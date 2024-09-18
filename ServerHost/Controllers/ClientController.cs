using AXT_WebComunication.WebResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerHost.Services;
using Shared.ApiRouting;
using Shared.Documents;
using TDatabase.Queries;

namespace ServerHost.Controllers;

[ApiController]
public class ClientController : DefaultController
{
    [LogAction]
    [Route(ApiRouting.ClientsList)]
    [Authorize]
    [HttpPost]
    public AXT_WebResponse ClientsList()
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var list = ClientDbHelper.Select(db, idOrganizzation);
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
    [Route(ApiRouting.SaveClient)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> SaveClient(ClientModel newClient)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var idOrganizzation = GetUserOrganization();
            var list = await ClientDbHelper.Insert(db, newClient, idOrganizzation);
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
    [Route(ApiRouting.UpdateClients)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> UpdateClients(List<ClientModel> clients)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var list = await ClientDbHelper.Update(db, clients);
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
    [Route(ApiRouting.HideClients)]
    [Authorize]
    [HttpPost]
    public async Task<AXT_WebResponse> HideClients(List<ClientModel> clients)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var list = await ClientDbHelper.Hide(db, clients);
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
