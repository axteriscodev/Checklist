using System.Text.Json.Serialization;

namespace Shared.Documents;

public class AttachmentModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; } = "";
}
