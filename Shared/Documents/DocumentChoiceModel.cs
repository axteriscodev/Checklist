using System.Text.Json.Serialization;
using Shared.Templates;

namespace Shared.Documents;

public class DocumentChoiceModel : TemplateChoiceModel
{

    [JsonPropertyName("reportedCompanyIds")]
    public List<int> ReportedCompanyIds { get; set; } = [];


}
