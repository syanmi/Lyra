namespace Lyra.Result
{
    public class EmptyResult(int status) : ILyraResult
    {
        private readonly int _status = status;

        public Task ExecuteAsync(LyraContext ctx)
        {
            ctx.Response.StatusCode = _status;
            return Task.CompletedTask;
        }
    }
}
