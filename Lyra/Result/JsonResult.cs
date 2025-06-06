using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Lyra.Http;

namespace Lyra.Result
{
    public class JsonResult<T>(T value, int status) : ILyraResult
    {
        private readonly T _value = value;
        private readonly int _status = status;

        public async Task ExecuteAsync(LyraContext ctx)
        {
            ctx.Response.StatusCode = _status;
            ctx.Response.ContentType = HttpHeader.ContentType.Json;

            var typeInfo = ctx.Json?.GetTypeInfo(typeof(T));
            var json = (typeInfo != null) ? 
                JsonSerializer.Serialize(_value, (JsonTypeInfo<T>)typeInfo) : 
                JsonSerializer.Serialize(_value);

            var buffer = Encoding.UTF8.GetBytes(json);
            ctx.Response.ContentLength64 = buffer.Length;
            await ctx.Response.WriteContentAsync(buffer, 0, buffer.Length);
        }
    }
}
