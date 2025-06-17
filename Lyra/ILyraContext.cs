using Lyra.Serialization;

namespace Lyra
{
    public interface ILyraContext
    {
        public IJsonMultiContextSerializer Json { get; }
        public ILyraRequest Request { get; }
        public ILyraResponse Response { get; }
    }
}
