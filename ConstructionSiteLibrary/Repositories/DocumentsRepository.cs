using System.Text.Json;
using ConstructionSiteLibrary.Managers;
using ConstructionSiteLibrary.Services;
using ConstructionSiteLibrary.Model;
using Shared;

namespace ConstructionSiteLibrary.Repositories;

public class DocumentsRepository(HttpManager httpManager, IndexedDBService indexedDBService)
{
    List<DocumentModel> Documents = [];

    private readonly HttpManager _httpManager = httpManager;
    private readonly IndexedDBService _indexedDBService = indexedDBService;

    public async Task<List<DocumentModel>> GetDocuments()
    {
        if (Documents.Count == 0)
        {
            var response = await _httpManager.SendHttpRequest("Document/DocumentsList", 0);
            if (response.Code.Equals("0"))
            {

                Documents = JsonSerializer.Deserialize<List<DocumentModel>>(response.Content.ToString() ?? "") ?? [];
                _ = await _indexedDBService.Insert(IndexedDBTables.documents, Documents.Cast<object>().ToArray());
            }
            else if (response.Code.Equals("Ex8995BA25"))// problemi di connessione
            {
                var content = await _indexedDBService.ReadObjectStore(IndexedDBTables.documents);
                Documents = JsonSerializer.Deserialize<List<DocumentModel>>(content ?? "") ?? [];
            }
        }

        return Documents;
    }

    public async Task<DocumentModel> GetDocumentById(int idDocument = 0)
    {
        var document = new DocumentModel();
        var response = await _httpManager.SendHttpRequest("Document/DocumentsList", idDocument);
        if (response.Code.Equals("0"))
        {
            var documents = JsonSerializer.Deserialize<List<DocumentModel>>(response.Content.ToString() ?? "") ?? [];
            document = documents.FirstOrDefault() ?? new();
        } else if(response.Code.Equals("Ex8995BA25"))// problemi di connessione
        {
            var content = await _indexedDBService.Read(IndexedDBTables.documents, idDocument);
            document = JsonSerializer.Deserialize<DocumentModel>(content ?? "") ?? new();
        }    
        return document;
    }

    public async Task<bool> SaveDocument(DocumentModel document)
    {
        var response = await _httpManager.SendHttpRequest("Document/SaveDocument", document);

        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            Documents.Clear();
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateDocuments(List<DocumentModel> documents)
    {
        var response = await _httpManager.SendHttpRequest("Document/UpdateDocument", documents);
        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            Documents.Clear();
            return true;
        }

        return false;
    }

    public async Task<bool> HideDocuments(List<DocumentModel> documents)
    {
        var response = await _httpManager.SendHttpRequest("Document/HideQuestion", documents);
        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            Documents.Clear();
            return true;
        }

        return false;
    }
}
