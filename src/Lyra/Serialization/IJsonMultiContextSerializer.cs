using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Lyra.Serialization
{
    public interface IJsonMultiContextSerializer
    {
        public JsonSerializerContext[] Contexts { get; }

        JsonTypeInfo<T>? GetTypeInfo<T>();
        string Serialize<T>(T value);
        ValueTask<T?> DeserializeAsync<T>(Stream data);
    }
}
