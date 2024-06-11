using ConstructionSiteLibrary.Components.Utilities;

using Shared.Templates;

namespace ConstructionSiteLibrary.Components.Templates.Wizard;

public partial class TemplateSelectionStep
{
    List<TemplateModel> Templates = [];

    TemplateModel? CurrentSelection;

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
        Templates = await TemplatesRepository.GetTemplates();
    }

}
