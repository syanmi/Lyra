namespace Lyra.Middleware
{
    internal class AnonymousMiddleware : IMiddleware
    {
        private readonly Func<LyraContext, Func<Task>, Task> _handler;

        public AnonymousMiddleware(Func<LyraContext, Func<Task>, Task> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler), "Handler cannot be null.");
        }

        public AnonymousMiddleware(Action<LyraContext, Func<Task>> handler)
        {
            _handler = (context, next) =>
            {
                handler(context, next);
                return Task.CompletedTask;
            };
        }

        public Task InvokeAsync(LyraContext context, Func<Task> next) => _handler.Invoke(context, next);
    }
}
