using System.Text;
using System.Text.Json;
using Lyra.Http;

namespace Lyra.Result
{
    public class JsonResult<T>(T value, int status) : ILyraResult
    {
        public async Task ExecuteAsync(ILyraContext ctx)
        {
            ctx.Response.StatusCode = status;
            ctx.Response.ContentType = HttpHeader.ContentType.Json;

            var typeInfo = ctx.Json.GetTypeInfo<T>();
            var json = (typeInfo != null) ? 
                JsonSerializer.Serialize(value, typeInfo) : 
                JsonSerializer.Serialize(value);

            var buffer = Encoding.UTF8.GetBytes(json);
            ctx.Response.ContentLength64 = buffer.Length;
            await ctx.Response.WriteContentAsync(buffer, 0, buffer.Length);
        }
    }
}
