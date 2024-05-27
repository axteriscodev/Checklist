using System.Text.Json.Serialization;

namespace Shared;

public class NoteModel
{
    [JsonPropertyName("Text")]
    public string Text { get; set; } = "";

    [JsonPropertyName("companyListIds")]
    public List<int> CompanyListIds { get; set; } = [];
}
