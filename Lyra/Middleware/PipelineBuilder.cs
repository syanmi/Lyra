namespace Lyra.Middleware
{
    internal class PipelineBuilder
    {
        private readonly List<PipelineNode> _nodes = new();

        public void Add(PipelineNode node)
        {
            _nodes.Add(node);
        }

        public RequestHandler Build(RequestHandler terminal) => _nodes.ToArray().BuildHandler(terminal);
    }
}
