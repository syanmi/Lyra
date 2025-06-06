namespace Lyra.Middleware
{
    public class ExceptionHandlerOptions
    {
        private readonly List<PipelineNode> _handlers = new ();
        private PipelineNode _fallback = DefaultUnhandledHandler;

        public PipelineNode[] Handlers => _handlers.ToArray();
        public PipelineNode FallbackHandler => _fallback;

        public void Handle<TException>(Func<LyraContext, Exception, Task> handler) where TException : Exception
        {
            _handlers.Add(async (ctx, next) => 
            {
                try
                {
                    await next();
                }
                catch (TException e)
                {
                    await handler(ctx, e);
                }
            });
        }

        public void OnUnhandled(Func<LyraContext, Exception, Task> handler)
        {
            _fallback = async (ctx, next) => 
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    await handler(ctx, ex);
                }
            };
        }

        private static async Task DefaultUnhandledHandler(LyraContext context, Func<Task> next)
        {
            try
            {
                await next();
            }
            catch (Exception)
            {
                var result = LyraResult.InternalServerError("Internal Server Error");
                await context.InvokeResult(result);
            }
        }
    }
}
