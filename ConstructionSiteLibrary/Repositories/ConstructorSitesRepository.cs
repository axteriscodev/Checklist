using System.Diagnostics;
using System.Text.Json;
using ConstructionSiteLibrary.Managers;
using Shared.ApiRouting;
using Shared.Documents;

namespace ConstructionSiteLibrary.Repositories;

public class ConstructorSitesRepository(HttpManager httpManager)
{
    List<SiteModel> ConstructorSites = [];

    private HttpManager _httpManager = httpManager;

    public async Task<List<SiteModel>> GetConstructorSites()
    {
        var response = await _httpManager.SendHttpRequest(ApiRouting.ConstructorSitesList, "");
        if (response.Code.Equals("0"))
        {
            ConstructorSites = JsonSerializer.Deserialize<List<SiteModel>>(response.Content.ToString() ?? "") ?? [];
        }

        return ConstructorSites;
    }

    public async Task<SiteModel> GetConstructorSiteInfo(int idConstructorSite)
    {
        SiteModel site = new();
        var response = await _httpManager.SendHttpRequest(ApiRouting.ConstructorSiteInfo, idConstructorSite);
        if (response.Code.Equals("0"))
        {
            var list = JsonSerializer.Deserialize<List<SiteModel>>(response.Content.ToString() ?? "") ?? [];
            site = list.FirstOrDefault() ?? new();
        }

        return site;
    }

    public async Task<bool> SaveContructorSite(SiteModel constructorSite)
    {
         var response = await _httpManager.SendHttpRequest(ApiRouting.SaveConstructorSite, constructorSite);

        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            ConstructorSites.Clear();
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateContructorSites(List<SiteModel> constructorSites)
    {
        var response = await _httpManager.SendHttpRequest(ApiRouting.UpdateConstructorSites, constructorSites);
        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            ConstructorSites.Clear();
            return true;
        }

        return false;
    }

    public async Task<bool> HideConstructorSites(List<SiteModel> constructorSites)
    {
        var response = await _httpManager.SendHttpRequest(ApiRouting.HideConstructorSites, constructorSites);
        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            ConstructorSites.Clear();
            return true;
        }

        return false;
    }
}
