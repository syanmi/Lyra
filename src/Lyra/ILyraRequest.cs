namespace Lyra
{
    public interface ILyraRequest
    {
        string Method { get; }
        string Path { get; }
        Dictionary<string, string> RouteValues { get; }

        string? Query(string key);
        Task<string> ReadBodyAsync(CancellationToken token = default);
        Task<byte[]> ReadBodyBytesAsync(CancellationToken token = default);
        Task<T?> ReadBodyAsync<T>(CancellationToken token = default);
    }
}
