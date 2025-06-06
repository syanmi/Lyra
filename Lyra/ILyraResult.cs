namespace Lyra
{
    public interface ILyraResult
    {
        public Task ExecuteAsync(LyraContext ctx);
    }
}
