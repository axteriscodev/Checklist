using System.Text.Json.Serialization;

namespace Shared;

public class DocumentChoiceModel : TemplateChoicesModel
{
    
    [JsonPropertyName("reportedCompanyIds")]
    public List<int> ReportedCompanyIds { get; set; } = [];

    [JsonPropertyName("color")]
    public string Color { get; set; } = "";
}
