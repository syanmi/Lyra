namespace Lyra.Middleware
{
    public delegate Task PipelineNode(ILyraContext context, Func<Task> next);
}
