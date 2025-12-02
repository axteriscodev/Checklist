using AXT_WebComunication.WebResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerHost.Services;
using Shared.ApiRouting;
using Shared.Documents;
using TDatabase.Queries;

namespace ServerHost.Controllers;

[ApiController]
public class ConstructorSiteController : DefaultController
{
    [LogAction]
    [Route(ApiRouting.ConstructorSitesList)]
    [Authorize]
    [HttpPost]
    public AXT_WebResponse ConstructorSitesList()
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var list = ConstructorSiteDbHelper.Select(db, idOrganizzation);
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
    [Route(ApiRouting.ConstructorSiteInfo)]
    [Authorize]
    [HttpPost]
    public AXT_WebResponse ConstructorSiteInfo([FromBody] int idConstructorSite)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var list = ConstructorSiteDbHelper.Select(db, idOrganizzation, idConstructorSite);
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
    [Route(ApiRouting.SaveConstructorSite)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> SaveConstructorSite(SiteModel newContructorSite)
    {
         var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var list = await ConstructorSiteDbHelper.Insert(db, newContructorSite, idOrganizzation);
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
    [Route(ApiRouting.UpdateConstructorSites)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> UpdateConstructorSites(List<SiteModel> constructorSites)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var list = await ConstructorSiteDbHelper.Update(db, constructorSites);
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
    [Route(ApiRouting.HideConstructorSites)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> HideConstructorSites(List<SiteModel> constructorSites)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var list = await ConstructorSiteDbHelper.Hide(db, constructorSites);
            response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), list);
        }
        catch (Exception ex)
        {
            response = ExceptionWebResponse(ex, "");
        }
        StopTime(stopwatch);
        return response;
    }
}
