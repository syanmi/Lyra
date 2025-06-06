namespace Lyra
{
    public static class LyraContextExtensions
    {
        public static Task InvokeResult(this LyraContext ctx, ILyraResult result) => result.ExecuteAsync(ctx);
    }
}
