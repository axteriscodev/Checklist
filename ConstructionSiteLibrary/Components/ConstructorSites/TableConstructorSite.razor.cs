using ConstructionSiteLibrary.Components.Utilities;
using ConstructionSiteLibrary.Repositories;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using Shared.Documents;

namespace ConstructionSiteLibrary.Components.ConstructorSites;

public partial class TableConstructorSite
{

    private List<ConstructorSiteModel> constructorSites = [];

    private bool initialLoading;

    /// <summary>
    /// Booleano che è impostata durante una ricerca
    /// </summary>
    private bool isLoading = false;


    /// <summary>
    /// Intero che ci dice quanti sono gli elementi
    /// </summary>
    private int count;

    /// <summary>
    /// Intero che ci dice quanti elementi possono stare in una pagina
    /// </summary>
    private int pageSize = 8;

    /// <summary>
    /// Stringa indica la pagina e gli elementi
    /// </summary>
    private string pagingSummaryFormat = "Pagina {0} di {1} (Totale {2} cantieri)";

    /// <summary>
    /// Riferimento al componente tabella
    /// </summary>
    private RadzenDataGrid<ConstructorSiteModel>? grid;

    [Parameter]
    public string Param { get; set; } = "";

    ScreenComponent screenComponent;

    protected override async Task OnInitializedAsync()
    {
        initialLoading = true;
        await base.OnInitializedAsync();
        await LoadData();
        initialLoading = false;
    }

    private async Task LoadData()
    {
        constructorSites = await ConstructorSitesRepository.GetConstructorSites();
        count = constructorSites.Count;
    }

    private async Task ReloadTable()
    {
        DialogService.Close();
        await LoadData();
        await grid!.Reload();
    }

}
