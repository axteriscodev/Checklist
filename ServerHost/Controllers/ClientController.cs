using AXT_WebComunication.WebResponse;
using Microsoft.AspNetCore.Mvc;
using ServerHost.Services;
using Shared;
using TDatabase.Queries;

namespace ServerHost.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ClientController : DefaultController
{
    [LogAction]
    [HttpPost]
    public AXT_WebResponse ClientsList()
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var list = ClientDbHelper.Select(db);
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
    [HttpPost]
    public async Task<AXT_WebResponse> SaveClient(ClientModel newClient)
    {
        var response = new AXT_WebResponse();
        var stopwatch = StartTime();
        ConfigureLog("", 0);

        try
        {
            var db = GetDbConnection();
            var list = await ClientDbHelper.Insert(db, newClient);
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
