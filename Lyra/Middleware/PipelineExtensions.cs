namespace Lyra.Middleware
{
    internal static class PipelineExtensions
    {
        public static RequestHandler BuildHandler(this PipelineNode node, RequestHandler terminal)
        {
            return (context) => node(context, () => terminal(context));
        }

        public static RequestHandler BuildHandler(this PipelineNode[] nodes, RequestHandler terminal)
        {
            var built = terminal;
            foreach (var node in nodes.AsEnumerable().Reverse())
            {
                var func = (RequestHandler next) => (RequestHandler)((ctx) => node(ctx, () => next(ctx)));
                built = func(built);
            }
            return built;
        }

        public static PipelineNode Compose(this PipelineNode[] pipelines)
        {
            PipelineNode built = (_, next) => next();
            
            foreach (var pipe in pipelines)
            {
                var currentPipe = pipe;
                var nextPipe = built;
                built = (context, next) => currentPipe(context, () => nextPipe(context, next));
            }

            return built;
        }

        public static PipelineNode Append(this PipelineNode source, PipelineNode target)
        {
            return (context, next) => source(context, () => target(context, next));
        }
    }
}
