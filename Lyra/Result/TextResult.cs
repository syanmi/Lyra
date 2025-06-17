using Lyra.Http;
using System.Text;

namespace Lyra.Result
{
    public class TextResult(string value, int status) : ILyraResult
    {
        public async Task ExecuteAsync(ILyraContext ctx)
        {
            ctx.Response.StatusCode = status;
            ctx.Response.ContentType = HttpHeader.ContentType.Plain;
            var buffer = Encoding.UTF8.GetBytes(value);
            ctx.Response.ContentLength64 = buffer.Length;
            await ctx.Response.WriteContentAsync(buffer, 0, buffer.Length);
        }
    }
}
