using System.Text.Json.Serialization;

namespace Shared.Documents;

public class NoteModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("Text")]
    public string Text { get; set; } = "";

    [JsonPropertyName("companyListIds")]
    public List<int> CompanyListIds { get; set; } = [];

    [JsonPropertyName("attachments")]
    public List<AttachmentModel> Attachments { get; set; } = [];
}
