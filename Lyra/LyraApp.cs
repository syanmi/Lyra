using Lyra.Middleware;
using System.Net;
using System.Text.Json.Serialization;
using Lyra.Serialization;

namespace Lyra
{
    public class LyraApp
    {
        private readonly PipelineBuilder _builder = new ();
        private readonly List<RouteEntry> _routes = [];
        private readonly List<SunAppEntry> _subEntries = [];
        private readonly JsonMultiContextSerializer _jsonSerializer = new (new LyraJsonSerializerContext());

        public void UseJsonContext(JsonSerializerContext context)
        {
            if (context is not LyraJsonSerializerContext)
            {
                _jsonSerializer.AddContext(context);
            }
        }

        public void Use(PipelineNode middleware)
            => _builder.Add(middleware);

        public void Route(string method, string path, RequestHandler handler)
            => _routes.Add(new RouteEntry(method, path, _builder.Build(handler)));

        public void Map(string prefix, Action<LyraApp> configure)
        {
            var subApp = new LyraApp();
            configure(subApp);

            var handler = _builder.Build(async ctx =>
            {
                var context = ctx as LyraContext ?? throw new InvalidOperationException("Context must be of type LyraContext.");
                await subApp.InvokeAsync(context.RelativePath, context);
            });

            foreach (var jsonContext in _jsonSerializer.Contexts)
            {
                subApp.UseJsonContext(jsonContext);
            }
            _subEntries.Add(new SunAppEntry(prefix.TrimEnd('/'), subApp, handler));
        }

        public async Task RunAsync(int port, CancellationToken token = default)
        {
            var listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:{port}/");
            listener.Start();
            Console.WriteLine($"[Lyra] Listening on http://localhost:{port}/");

            while (!token.IsCancellationRequested)
            {
                var ctx = await listener.GetContextAsync();
                _ = Task.Run(async () =>
                {
                    var lyraCtx = new LyraContext(ctx.Request, ctx.Response, _jsonSerializer);
                    await InvokeAsync(ctx.Request.Url?.AbsolutePath ?? string.Empty, lyraCtx);
                    ctx.Response.Close();
                }, token);
            }

            token.ThrowIfCancellationRequested();
        }

        private async Task InvokeAsync(string path, LyraContext ctx)
        {
            var entry = _subEntries.FirstOrDefault(x => x.Regex.IsMatch(path));
            if (entry != null)
            {
                await entry.InvokeAsync(path, ctx);
                return;
            }

            var request = ctx.Request;
            var route = _routes.FirstOrDefault(r => r.Method == request.Method && r.Regex.IsMatch(path));
            if (route == null)
            {
                var result = LyraResult.NotFound();
                await ctx.InvokeResult(result);
                return;
            }

            try
            {
                await route.InvokeAsync(path, ctx);
            }
            catch (Exception)
            {
                var result = LyraResult.InternalServerError();
                await ctx.InvokeResult(result);
            }
        }
    }
}
