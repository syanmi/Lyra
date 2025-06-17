namespace Lyra.Result
{
    public class EmptyResult(int status) : ILyraResult
    {
        public Task ExecuteAsync(ILyraContext ctx)
        {
            ctx.Response.StatusCode = status;
            return Task.CompletedTask;
        }
    }
}
