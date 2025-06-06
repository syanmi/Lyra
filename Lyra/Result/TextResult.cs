using Lyra.Http;
using System.Text;

namespace Lyra.Result
{
    public class TextResult(string value, int status) : ILyraResult
    {
        private readonly string _value = value;
        private readonly int _status = status;

        public async Task ExecuteAsync(LyraContext ctx)
        {
            ctx.Response.StatusCode = _status;
            ctx.Response.ContentType = HttpHeader.ContentType.Plain;
            var buffer = Encoding.UTF8.GetBytes(_value);
            ctx.Response.ContentLength64 = buffer.Length;
            await ctx.Response.WriteContentAsync(buffer, 0, buffer.Length);
        }
    }
}
