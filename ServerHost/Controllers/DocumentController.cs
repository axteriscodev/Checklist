using AXT_WebComunication.WebResponse;
using Microsoft.AspNetCore.Mvc;
using ServerHost.Services;
using Shared.Documents;
using TDatabase.Queries;
using Shared.ApiRouting;
using Microsoft.AspNetCore.Authorization;

namespace ServerHost.Controllers;

[ApiController]
public class DocumentController : DefaultController
{
    [LogAction]
    [Route(ApiRouting.DocumentsList)]
    [Authorize]
    [HttpPost()]
    public AXT_WebResponse DocumentsList([FromBody]int idDocument)
    {
        var response = new AXT_WebResponse();
            var stopwatch = StartTime();
            ConfigureLog("", 0);

            try
            {
                var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var list = DocumentDbHelper.Select(db, idOrganizzation, idDocument);
                response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), list);

            }
            catch (Exception ex)
            {
                response = ExceptionWebResponse(ex, "");
            }
            StopTime(stopwatch);
            return response;
    }

    [LogAction]
    [Route(ApiRouting.SiteDocumentsList)]
    [Authorize]
    [HttpPost()]
    public AXT_WebResponse SiteDocumentsList([FromBody] int idSite)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var list = DocumentDbHelper.SelectFromSite(db, idSite);
            response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), list);

        }
        catch (Exception ex)
        {
            response = ExceptionWebResponse(ex, "");
        }
        StopTime(stopwatch);
        return response;
    }

    [LogAction]
    [Route(ApiRouting.SaveDocument)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> SaveDocument(DocumentModel newDocument)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var q = await DocumentDbHelper.Insert(db, newDocument, idOrganizzation);
            response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), q);

        }
        catch (Exception ex)
        {
            response = ExceptionWebResponse(ex, "");
        }
        StopTime(stopwatch);
        return response;
    }

    [LogAction]
    [Route(ApiRouting.UpdateDocument)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> UpdateDocument(List<DocumentModel> documents)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var q = await DocumentDbHelper.Update(db, documents, idOrganizzation);
            response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), q);

        }
        catch (Exception ex)
        {
            response = ExceptionWebResponse(ex, "");
        }
        StopTime(stopwatch);
        return response;
    }

    [LogAction]
    [Route(ApiRouting.HideDocuments)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> HideDocuments(List<DocumentModel> documents)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var q = await DocumentDbHelper.Hide(db, documents);
            response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), q);

        }
        catch (Exception ex)
        {
            response = ExceptionWebResponse(ex, "");
        }
        StopTime(stopwatch);
        return response;
    }

    [LogAction]
    [Route(ApiRouting.MeteoConditionsList)]
    [Authorize]
    [HttpPost]
    public AXT_WebResponse MeteoConditionsList()
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("" , 0);

        try
        {
            var db = GetDbConnection();
            var q = DocumentDbHelper.SelectMeteo(db);
            response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), q);
        }
        catch(Exception ex)
        {
            response = ExceptionWebResponse(ex, "");
        }

        StopTime(stopwatch);
        return response;
    }

}
