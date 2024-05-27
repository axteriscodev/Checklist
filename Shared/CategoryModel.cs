using System.Text.Json.Serialization;

namespace Shared;

public class CategoryModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = "";

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("questions")]
    public List<IQuestion> Questions { get; set; } = [];
}
