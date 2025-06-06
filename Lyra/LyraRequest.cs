using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Lyra
{
    internal class LyraRequest(HttpListenerRequest request, JsonSerializerContext? json = null) : ILyraRequest
    {
        private readonly HttpListenerRequest _request = request ?? throw new ArgumentNullException(nameof(request), "HttpListenerRequest cannot be null.");

        public string Method => _request.HttpMethod;
        public string Path => _request.Url?.AbsolutePath ?? string.Empty;
        public Dictionary<string, string> RouteValues { get; } = new ();
        public string? Query(string key) => _request.QueryString[key];

        public async Task<string> ReadBodyAsync(CancellationToken token = default)
        {
            using var reader = new StreamReader(_request.InputStream, _request.ContentEncoding);
            return await reader.ReadToEndAsync(token);
        }

        public async Task<byte[]> ReadBodyBytesAsync(CancellationToken token = default)
        {
            using var stream = new MemoryStream();
            await _request.InputStream.CopyToAsync(stream, token);
            return stream.ToArray();
        }

        public async Task<T?> ReadBodyAsync<T>(CancellationToken token = default)
        {
            if(json == null) throw new InvalidOperationException("JsonSerializerContext is not set. Use UseJsonContext method to set it.");

            var info = json.GetTypeInfo(typeof(T)) as JsonTypeInfo<T>;
            if (info == null) throw new InvalidOperationException($"JsonSerializerContext does not contain type info for {typeof(T)}.");

            return await JsonSerializer.DeserializeAsync(_request.InputStream, info, token);
        }
    }
}
