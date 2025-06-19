using System.Net;

namespace Lyra
{
    internal class LyraResponse(HttpListenerResponse response) : ILyraResponse
    {
        private readonly HttpListenerResponse _response = response ?? throw new ArgumentNullException(nameof(response), "HttpListenerResponse cannot be null.");

        public int StatusCode
        {
            get => _response.StatusCode;
            set => _response.StatusCode = value;
        }

        public string? ContentType
        {
            get => _response.ContentType;
            set => _response.ContentType = value;
        }

        public WebHeaderCollection Headers
        {
            get => _response.Headers;
            set => _response.Headers = value;
        }

        public long ContentLength64
        {
            get => _response.ContentLength64;
            set => _response.ContentLength64 = value;
        }

        public ValueTask WriteContentAsync(ReadOnlyMemory<byte> buffer)
            => _response.OutputStream.WriteAsync(buffer);
        public Task WriteContentAsync(byte[] buffer, int offset, int count) 
            => _response.OutputStream.WriteAsync(buffer, offset, buffer.Length);

        public void Close() => _response.Close();
    }
}
