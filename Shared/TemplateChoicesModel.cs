using System.Text.Json.Serialization;

namespace Shared;

public class TemplateChoicesModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; } = "";

    [JsonPropertyName("reportable")]
    public bool Reportable { get; set; }
}
