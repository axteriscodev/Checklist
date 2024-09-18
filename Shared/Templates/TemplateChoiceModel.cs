using System.Text.Json.Serialization;

namespace Shared.Templates;

public class TemplateChoiceModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; } = "";

    [JsonPropertyName("tag")]
    public string Tag { get; set; } = "";

    [JsonPropertyName("reportable")]
    public bool Reportable { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; } = "";
}
