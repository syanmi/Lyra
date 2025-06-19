using System.Text.RegularExpressions;

namespace Lyra
{
    internal class SunAppEntry
    {
        public string Pattern { get; }  // `/user/{id}`
        public Regex Regex { get; }     // `^/user/(?<id>[^/]+)$`
        public RequestHandler Handler { get; }
        public LyraApp App { get; }

        public SunAppEntry(string pattern, LyraApp app, RequestHandler handler)
        {
            Pattern = pattern;
            Regex = BuildRegexFromPattern(pattern);
            Handler = handler;
            App = app;
        }

        private Regex BuildRegexFromPattern(string pattern)
        {
            var regexPattern = "^" + Regex.Replace(pattern, @"\{(\w+)\}", @"(?<$1>[^/]+)");
            return new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public async Task InvokeAsync(string path, LyraContext ctx)
        {
            var subMatch = Regex.Match(path);
            var pathParams = subMatch.Groups
                .Cast<Group>()
                .Where(g => g.Success && Regex.GroupNameFromNumber(g.Index) != g.Name) // named only
                .ToDictionary(g => g.Name, g => g.Value);
            foreach (var parameter in pathParams)
            {
                ctx.Request.RouteValues.Add(parameter.Key, parameter.Value);
            }

            // パスをサブアプリ用に再構築（prefixを除去）
            var prefixLength = subMatch.Length;
            var newPath = path.Substring(prefixLength);
            if (string.IsNullOrEmpty(newPath))
                newPath = "/";

            ctx.RelativePath = newPath;

            // ✅ サブアプリ内で処理を引き継ぐ
            await Handler(ctx);
        }
    }
}
