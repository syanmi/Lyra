using System.Net;

namespace Lyra
{
    public interface ILyraResponse
    {
        public int StatusCode { get; set; }
        public string? ContentType { get; set; }
        public WebHeaderCollection Headers { get; set; }
        public long ContentLength64 { get; set; }

        public ValueTask WriteContentAsync(ReadOnlyMemory<byte> buffer);
        public Task WriteContentAsync(byte[] buffer, int offset, int count);
        public void Close();
    }
}
