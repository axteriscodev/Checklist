using System.Text.Json.Serialization;

namespace Shared;

public class SignatureModel
{
    [JsonPropertyName("idAzienda")]
    public int IdAzienda { get; set; }

    [JsonPropertyName("reportedQuestionsId")]
    public List<int> ReporteQuestionsIds { get; set; } = [];
}
