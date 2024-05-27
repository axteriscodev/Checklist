using System.Text.Json.Serialization;

namespace Shared;

public class DocumentModel
{
    [JsonPropertyName("id")]
    public int Id { get; set;}

    [JsonPropertyName("title")]
    public string Title { get; set;} = "";

    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set;} = DateTime.Now;

    [JsonPropertyName("compilationDate")]
    public DateTime CompilationDate { get; set;} = DateTime.Now;

    [JsonPropertyName("lastEditDate")]
    public DateTime LastEditDate { get; set;} = DateTime.Now;

    [JsonPropertyName("readonly")]
    public bool ReadOnly { get; set;}

    [JsonPropertyName("ChangedOffline")]
    public int ChangedOffline { get; set;}

    [JsonPropertyName("client")]
    public ClientModel? Client { get; set;}

    [JsonPropertyName("constructorSite")]
    public ConstructorSiteModel? ConstructorSite { get; set;}

    [JsonPropertyName("categories")]
    public List<CategoryModel> Categories { get; set;} = [];

    [JsonPropertyName("companies")]
    public List<CompanyModel> Companies { get; set;} = [];

    [JsonPropertyName("signatures")]
    public List<SignatureModel> Signature { get; set;} = [];

    [JsonPropertyName("attachments")]
    public List<AttachmentModel> Attachments { get; set;} = [];

    [JsonPropertyName("notes")]
    public List<NoteModel> Notes { get; set;} = [];
}
