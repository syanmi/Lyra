namespace Lyra.Middleware
{
    internal class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly PipelineNode _handler;

        public ExceptionHandlerMiddleware(ExceptionHandlerOptions options)
        {
            var handler = options.Handlers.ToArray().Compose();
            _handler = options.FallbackHandler.Append(handler);
        }

        public Task InvokeAsync(LyraContext context, Func<Task> next) => _handler.Invoke(context, next);
    }
}
