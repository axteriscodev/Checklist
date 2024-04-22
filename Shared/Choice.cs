using System.Text.Json.Serialization;

namespace Shared;

public class Choice
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("tag")]
    public string Tag { get; set; } = "";

    [JsonPropertyName("value")]
    public string Value { get; set; } = "";
}
