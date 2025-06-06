namespace Lyra.Middleware
{
    public delegate Task PipelineNode(LyraContext context, Func<Task> next);
}
