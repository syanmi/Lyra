using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Lyra.Serialization
{
    public class JsonMultiContextSerializer : IJsonMultiContextSerializer
    {
        private readonly List<JsonSerializerContext> _contexts = new();

        public JsonSerializerContext[] Contexts => _contexts.ToArray();

        public JsonMultiContextSerializer(JsonSerializerContext context)
        {
            _contexts.Add(context);
        }

        public void AddContext(JsonSerializerContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            _contexts.Add(ctx);
        }

        public JsonTypeInfo<T>? GetTypeInfo<T>()
        {
            foreach (var context in _contexts)
            {
                if (context.GetTypeInfo(typeof(T)) is JsonTypeInfo<T> info)
                {
                    return info;
                }
            }
            return null;
        }

        public string Serialize<T>(T value)
        {
            var typeInfo = GetTypeInfo<T>();
            return typeInfo != null
                ? JsonSerializer.Serialize(value, typeInfo)
                : JsonSerializer.Serialize(value);
        }

        public ValueTask<T?> DeserializeAsync<T>(Stream data)
        {
            var typeInfo = GetTypeInfo<T>();
            return typeInfo != null
                ? JsonSerializer.DeserializeAsync(data, typeInfo)
                : JsonSerializer.DeserializeAsync<T>(data);
        }
    }
}
