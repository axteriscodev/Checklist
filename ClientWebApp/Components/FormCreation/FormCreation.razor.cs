using ClientWebApp.Repositories;
using Microsoft.AspNetCore.Components;
using Shared;

namespace ClientWebApp.Components.FormCreation;

public partial class FormCreation
{

    private List<CategoryModel> categories = [];

    private IEnumerable<int> selected = [];

    /// <summary>
    /// booleano che indica se la pagina sta eseguendo il caricamento iniziale
    /// </summary>
    private bool initialLoading;

    /// <summary>
    /// Booleano che è impostata durante una ricerca
    /// </summary>
    private bool isLoading = false;


    /// <summary>
    /// Intero che ci dice quanti sono gli elementi
    /// </summary>
    private int count;


    [Parameter]
    public string Param { get; set; } = "";

    protected override async Task OnInitializedAsync()
    {
        initialLoading = true;
        await base.OnInitializedAsync();
        await LoadData();
        initialLoading = false;
    }

    private async Task LoadData()
    {
        categories = await CategoriesRepository.GetCategories();
        count = categories.Count;
    }

    private async Task ReloadTable()
    {
        //DialogService.Close();
        await LoadData();
        //await grid!.Reload();
    }

}
