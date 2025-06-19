namespace Lyra.Middleware
{
    public interface IMiddleware
    {
        Task InvokeAsync(ILyraContext context, Func<Task> next);
    }
}
