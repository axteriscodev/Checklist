using ConstructionSiteLibrary.Components.Utilities;
using Shared.Templates;

namespace ConstructionSiteLibrary.Components.Templates.Wizard;

public partial class TemplateDescriptionSelectionStep
{
    List<TemplateDescriptionModel> TemplatesDescriptions = [];

    TemplateDescriptionModel? CurrentSelection;

    /// <summary>
    /// booleano che indica se la pagina sta eseguendo il caricamento iniziale
    /// </summary>
    private bool initialLoading;

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
        TemplatesDescriptions = await TemplatesRepository.GetTemplatesDescriptions();
    }
}
