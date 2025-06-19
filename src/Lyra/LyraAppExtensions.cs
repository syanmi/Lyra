using Lyra.Http;

namespace Lyra
{
    public static class LyraAppExtensions
    {
        public static void Map(this LyraApp app, string prefix, Action<LyraApp> configure)
        {
            var subApp = new LyraApp();
            configure(subApp);
            app.Map(prefix, subApp);
        }


        public static void Route(this LyraApp app, string method, string path, Func<ILyraContext, Task<ILyraResult>> handler) 
            => app.Route(method, path, async (context) => 
            {
                var result = await handler(context);
                await context.InvokeResult(result);
            });

        public static void Route(this LyraApp app, string method, string path, Func<ILyraRequest, Task<ILyraResult>> handler)
            => app.Route(method, path, (context) => handler(context.Request));

        public static void Route<T>(this LyraApp app, string method, string path, Func<ILyraContext, T, Task<ILyraResult>> handler)
            => app.Route(method, path, async (ctx) =>
            {
                var body = await ctx.Request.ReadBodyAsync<T>();
                if(body == null) throw new ArgumentNullException(nameof(body), "Request body cannot be null.");

                return await handler(ctx, body);
            });

        public static void Route(this LyraApp app, string method, string path, Func<ILyraContext, ILyraResult> handler)
            => app.Route(method, path, (Func<ILyraContext, Task<ILyraResult>>)((context) => Task.FromResult(handler(context))));
        public static void Route(this LyraApp app, string method, string path, Func<ILyraRequest, ILyraResult> handler)
            => app.Route(method, path, (context) => Task.FromResult(handler(context)));
        public static void Route<T>(this LyraApp app, string method, string path, Func<ILyraContext, T, ILyraResult> handler)
            => app.Route<T>(method, path, (context, data) => Task.FromResult(handler(context, data)));


        public static void Get(this LyraApp app, string path, RequestHandler handler)
            => app.Route(HttpMethodConstant.Get, path, handler);
        public static void Get(this LyraApp app, string path, Func<ILyraContext, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Get, path, handler);
        public static void Get(this LyraApp app, string method, string path, Func<ILyraRequest, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Get, path, handler);
        public static void Get<T>(this LyraApp app, string path, Func<ILyraContext, T, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Get, path, handler);
        public static void Get(this LyraApp app, string path, Func<ILyraContext, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Get, path, handler);
        public static void Get(this LyraApp app, string method, string path, Func<ILyraRequest, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Get, path, handler);
        public static void Get<T>(this LyraApp app, string path, Func<ILyraContext, T, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Get, path, handler);


        public static void Post(this LyraApp app, string path, RequestHandler handler)
            => app.Route(HttpMethodConstant.Post, path, handler);
        public static void Post(this LyraApp app, string path, Func<ILyraContext, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Post, path, handler);
        public static void Post(this LyraApp app, string method, string path, Func<ILyraRequest, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Post, path, handler);
        public static void Post<T>(this LyraApp app, string path, Func<ILyraContext, T, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Post, path, handler);
        public static void Post(this LyraApp app, string path, Func<ILyraContext, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Post, path, handler);
        public static void Post(this LyraApp app, string method, string path, Func<ILyraRequest, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Post, path, handler);
        public static void Post<T>(this LyraApp app, string path, Func<ILyraContext, T, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Post, path, handler);


        public static void Put(this LyraApp app, string path, RequestHandler handler)
            => app.Route(HttpMethodConstant.Put, path, handler);
        public static void Put(this LyraApp app, string path, Func<ILyraContext, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Put, path, handler);
        public static void Put(this LyraApp app, string method, string path, Func<ILyraRequest, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Put, path, handler);
        public static void Put<T>(this LyraApp app, string path, Func<ILyraContext, T, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Put, path, handler);
        public static void Put(this LyraApp app, string path, Func<ILyraContext, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Put, path, handler);
        public static void Put(this LyraApp app, string method, string path, Func<ILyraRequest, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Put, path, handler);
        public static void Put<T>(this LyraApp app, string path, Func<ILyraContext, T, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Put, path, handler);


        public static void Delete(this LyraApp app, string path, RequestHandler handler)
            => app.Route(HttpMethodConstant.Delete, path, handler);
        public static void Delete(this LyraApp app, string path, Func<ILyraContext, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Delete, path, handler);
        public static void Delete(this LyraApp app, string method, string path, Func<ILyraRequest, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Delete, path, handler);
        public static void Delete<T>(this LyraApp app, string path, Func<ILyraContext, T, Task<ILyraResult>> handler)
            => app.Route(HttpMethodConstant.Delete, path, handler);
        public static void Delete(this LyraApp app, string path, Func<ILyraContext, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Delete, path, handler);
        public static void Delete(this LyraApp app, string method, string path, Func<ILyraRequest, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Delete, path, handler);
        public static void Delete<T>(this LyraApp app, string path, Func<ILyraContext, T, ILyraResult> handler)
            => app.Route(HttpMethodConstant.Delete, path, handler);
    }
}
