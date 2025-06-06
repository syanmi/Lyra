using System.Text.RegularExpressions;

namespace Lyra
{
    internal class RouteEntry
    {
        public string Method { get; }
        public string Pattern { get; }  // `/user/{id}`
        public Regex Regex { get; }     // `^/user/(?<id>[^/]+)$`
        public RequestHandler Handler { get; }

        public RouteEntry(string method, string pattern, RequestHandler handler)
        {
            Method = method;
            Pattern = pattern;
            Handler = handler;
            Regex = BuildRegexFromPattern(pattern);
        }

        private Regex BuildRegexFromPattern(string pattern)
        {
            // 例: "/user/{id}" → "^/user/(?<id>[^/]+)$"
            var regexPattern = "^" + Regex.Replace(pattern, @"\{(\w+)\}", @"(?<$1>[^/]+)") + "$";
            return new Regex(regexPattern, RegexOptions.Compiled);
        }

        public async Task InvokeAsync(string path, LyraContext ctx)
        {
            var match = Regex.Match(path);
            var routeValues = match.Groups
                .Cast<Group>()
                .Where(g => g.Success && Regex.GroupNameFromNumber(g.Index) != g.Name) // named only
                .ToDictionary(g => g.Name, g => g.Value);

            foreach (var kv in routeValues)
                ctx.Request.RouteValues[kv.Key] = kv.Value;

            await Handler(ctx);
        }
    }
}
