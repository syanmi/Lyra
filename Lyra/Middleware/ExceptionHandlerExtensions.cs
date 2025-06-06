namespace Lyra.Middleware
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseExceptionHandler(this LyraApp app, Action<ExceptionHandlerOptions>? configure = null)
        {
            var options = new ExceptionHandlerOptions();
            configure?.Invoke(options);

            var middleware = new ExceptionHandlerMiddleware(options);
            app.Use(middleware);
        }
    }
}
