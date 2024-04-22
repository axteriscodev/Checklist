using System.Text.Json.Serialization;

namespace Shared;

public class Question
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = "";

    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; } = [];

    [JsonPropertyName("currentChoice")]
    public Choice CurrentChoice { get; set; } = new Choice();

    [JsonPropertyName("note")]
    public string Note { get; set; } = "";

    [JsonPropertyName("printable")]
    public bool Printable { get; set; } = true;
    
    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; } = false;

}
