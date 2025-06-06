using Lyra.Middleware;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace Lyra
{
    public class LyraApp
    {
        private readonly PipelineBuilder _builder = new ();
        private readonly List<RouteEntry> _routes = new();
        private readonly List<SunAppEntry> _subEntries = new();
        private JsonSerializerContext? _json;

        public void UseJsonContext(JsonSerializerContext context) => _json = context;

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
                await subApp.InvokeAsync(ctx.RelativePath, ctx);
            });

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
                    var lyraCtx = new LyraContext(ctx.Request, ctx.Response, _json);
                    await InvokeAsync(ctx.Request.Url?.AbsolutePath ?? string.Empty, lyraCtx);
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

            var req = ctx.Request;
            var res = ctx.Response;
            var route = _routes.FirstOrDefault(r => r.Method == req.Method && r.Regex.IsMatch(path));
            if (route == null)
            {
                res.StatusCode = 404;
                await res.WriteContentAsync(Encoding.UTF8.GetBytes("Not Found"));
                res.Close();
                return;
            }

            try
            {
                await route.InvokeAsync(path, ctx);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            res.Close();
        }
    }
}
