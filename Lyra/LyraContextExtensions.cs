namespace Lyra
{
    public static class LyraContextExtensions
    {
        public static Task InvokeResult(this ILyraContext ctx, ILyraResult result) => result.ExecuteAsync(ctx);
    }
}
