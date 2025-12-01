using System.Data;
using System.Text.Json.Serialization;
using Shared.Defaults;

namespace Shared.Templates;

public class TemplateModel
{
    [JsonPropertyName("idTemplate")]
    public int IdTemplate { get; set; }

    [JsonPropertyName("nameTemplate")]
    public string NameTemplate { get; set; } = "";

    [JsonPropertyName("titleTemplate")]
    public string TitleTemplate { get; set; } = "";

    [JsonPropertyName("note")]
    public string Note { get; set; } = "";

    [JsonPropertyName("hasClient")]
    public bool HasClient { get; set; } = false;

    [JsonPropertyName("hasCompanies")]
    public bool HasCompanies { get; set; } = false;

    [JsonPropertyName("hasMeteo")]
    public bool HasMeteo { get; set; } = false;

    [JsonPropertyName("hasSiteData")]
    public bool HasSiteData { get; set; } = false;

    [JsonPropertyName("creationDateTemplate")]
    public DateTime CreationDate { get; set; } = DateTime.Now;

    [JsonPropertyName("categories")]
    public List<TemplateCategoryModel> Categories { get; set; } = [];

    [JsonPropertyName("description")]
    public TemplateDescriptionModel Description { get; set; } = new();
}
