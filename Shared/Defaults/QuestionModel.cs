using System.Text.Json.Serialization;

namespace Shared.Defaults;

public class QuestionModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = "";

    [JsonPropertyName("idCategory")]
    public int IdCategory { get; set; }
}
