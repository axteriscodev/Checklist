using System.Text.Json.Serialization;

namespace Shared;

public class DocumentChoiceModel : IQuestion
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; } = "";

    [JsonPropertyName("reportable")]
    public bool Reportable { get; set; }

    [JsonPropertyName("reportedCompanyIds")]
    public List<int> ReportedCompanyIds { get; set; } = [];

    [JsonPropertyName("color")]
    public string Color { get; set; } = "";
}
