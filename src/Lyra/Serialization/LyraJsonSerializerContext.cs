using Lyra.Result;
using System.Text.Json.Serialization;

namespace Lyra.Serialization
{
    [JsonSerializable(typeof(DetailResponse))]
    internal partial class LyraJsonSerializerContext : JsonSerializerContext
    {
    }
}
