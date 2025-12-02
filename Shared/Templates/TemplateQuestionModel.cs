using System.Text.Json.Serialization;
using Shared.Defaults;

namespace Shared.Templates;

public class TemplateQuestionModel : QuestionModel
{
    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("choices")]
    public List<TemplateChoiceModel> Choices { get; set; } = [];

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; } = string.Empty;

    [JsonPropertyName("topic")]
    public string Topic { get; set; } = string.Empty;

}
