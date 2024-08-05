using AXT_WebComunication.WebResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerHost.Services;
using Shared.ApiRouting;
using Shared.Templates;
using TDatabase.Queries;

namespace ServerHost.Controllers;

[ApiController]
public class TemplateController : DefaultController
{
    [LogAction]
    [Route(ApiRouting.TemplatesList)]
    [Authorize]
    [HttpPost()]
    public AXT_WebResponse TemplatesList([FromBody]int idTemplate)
    {
        var response = new AXT_WebResponse();
            var stopwatch = StartTime();
            ConfigureLog("", 0);

            try
            {
                var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var list = TemplateDbHelper.Select(db, idOrganizzation, idTemplate);
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
    [Route(ApiRouting.SaveTemplate)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> SaveTemplate(TemplateModel newTemplate)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var q = await TemplateDbHelper.Insert(db, idOrganizzation, newTemplate);
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
    [Route(ApiRouting.HideTemplates)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> HideTemplates(List<TemplateModel> templates)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var q = await TemplateDbHelper.Hide(db, templates);
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
    [Route(ApiRouting.TemplatesDescriptionsList)]
    [Authorize]
    [HttpPost]
    public AXT_WebResponse TemplateDescitpionsList([FromBody]int idTemplate)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var q = TemplateDescriptionDbHelper.Select(db, idTemplate);
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
    [Route(ApiRouting.SaveTemplatesDescriptions)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> SaveTemplateDescription(TemplateDescriptionModel newDesc)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var q = await TemplateDescriptionDbHelper.Insert(db, idOrganizzation, newDesc);
            response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), q);

        }
        catch (Exception ex)
        {
            response = ExceptionWebResponse(ex, "");
        }
        StopTime(stopwatch);
        return response;
    }
}
