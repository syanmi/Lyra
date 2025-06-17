using System.Net;
using Lyra.Serialization;

namespace Lyra
{
    public class LyraContext(HttpListenerRequest request, HttpListenerResponse response, IJsonMultiContextSerializer json) : ILyraContext
    {
        public string RelativePath { get; set; } = request.Url?.AbsolutePath ?? "";
        public IJsonMultiContextSerializer Json => json;
        public ILyraRequest Request { get; } = new LyraRequest(request, json);
        public ILyraResponse Response { get; } = new LyraResponse(response);
    }
}
