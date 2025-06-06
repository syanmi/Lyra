namespace Lyra.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void Use(this LyraApp app, IMiddleware middleware)
        {
            app.Use(middleware.InvokeAsync);
        }
    }
}
