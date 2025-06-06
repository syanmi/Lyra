using System.Net;
using System.Text.Json.Serialization;

namespace Lyra
{
    public class LyraContext(HttpListenerRequest request, HttpListenerResponse response, JsonSerializerContext? json)
    {
        public string RelativePath { get; set; } = request.Url?.AbsolutePath ?? "";
        public JsonSerializerContext? Json => json;
        public ILyraRequest Request { get; } = new LyraRequest(request, json);
        public ILyraResponse Response { get; } = new LyraResponse(response);
    }
}
