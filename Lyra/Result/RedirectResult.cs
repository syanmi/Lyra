namespace Lyra.Result
{
    public class RedirectResult : ILyraResult
    {
        private readonly string _url;
        private readonly int _statusCode;

        public RedirectResult(string url, bool permanent = false)
        {
            _url = url ?? throw new ArgumentNullException(nameof(url));
            _statusCode = permanent ? 301 : 302; // 301 Moved Permanently / 302 Found
        }

        public RedirectResult(string url, int statusCode)
        {
            if (statusCode is not (301 or 302 or 303 or 307 or 308))
                throw new ArgumentOutOfRangeException(nameof(statusCode), "Must be a valid redirect status code.");

            _url = url ?? throw new ArgumentNullException(nameof(url));
            _statusCode = statusCode;
        }

        public Task ExecuteAsync(ILyraContext ctx)
        {
            ctx.Response.StatusCode = _statusCode;
            ctx.Response.Headers["Location"] = _url;
            return Task.CompletedTask;
        }
    }
}
