namespace Lyra.Middleware
{
    internal class AnonymousMiddleware : IMiddleware
    {
        private readonly Func<ILyraContext, Func<Task>, Task> _handler;

        public AnonymousMiddleware(Func<ILyraContext, Func<Task>, Task> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler), "Handler cannot be null.");
        }

        public AnonymousMiddleware(Action<ILyraContext, Func<Task>> handler)
        {
            _handler = (context, next) =>
            {
                handler(context, next);
                return Task.CompletedTask;
            };
        }

        public Task InvokeAsync(ILyraContext context, Func<Task> next) => _handler.Invoke(context, next);
    }
}
