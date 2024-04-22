using System.Text.Json.Serialization;

namespace Shared;

public class MacroCategory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = "";

    [JsonPropertyName("subCategories")]
    public List<SubCategory> SubCategories { get; set; } = [];
}
