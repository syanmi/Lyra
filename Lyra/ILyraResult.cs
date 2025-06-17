namespace Lyra
{
    public interface ILyraResult
    {
        public Task ExecuteAsync(ILyraContext ctx);
    }
}
