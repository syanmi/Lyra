namespace Lyra.Http
{
    internal class HttpHeader
    {
        public static class ContentType
        {
            public const string Json = "application/json";
            public const string Html = "text/html";
            public const string Plain = "text/plain";
            public const string FormUrlEncoded = "application/x-www-form-urlencoded";
            public const string OctetStream = "application/octet-stream";
        }

        public static class Accept
        {
            public const string Json = "application/json";
            public const string Any = "*/*";
        }

        public static class Connection
        {
            public const string KeepAlive = "keep-alive";
            public const string Close = "close";
        }

        public static class AuthorizationScheme
        {
            public const string Basic = "Basic";
            public const string Bearer = "Bearer";
        }
    }
}
