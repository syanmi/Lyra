namespace Lyra.Middleware
{
    public interface IMiddleware
    {
        Task InvokeAsync(LyraContext context, Func<Task> next);
    }
}
