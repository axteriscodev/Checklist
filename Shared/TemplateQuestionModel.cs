using System.Text.Json.Serialization;

namespace Shared;

public class TemplateQuestionModel : IQuestion
{
    [JsonPropertyName("id")]
    public int Id { get; set;}

    [JsonPropertyName("text")]
    public string Text { get; set;} = "";

    [JsonPropertyName("order")]
    public int Order { get; set;}

    [JsonPropertyName("choices")]
    public List<TemplateChoicesModel> Choices { get; set;} = [];
}
