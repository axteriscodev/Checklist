using ConstructionSiteLibrary.Components.Utilities;
using Radzen;


namespace ConstructionSiteLibrary.Components.Templates.Wizard;

public partial class TemplateNameStep
{
    /// <summary>
    /// booleano che indica se la pagina sta eseguendo il caricamento iniziale
    /// </summary>
    private bool initialLoading;

    private bool onSaving = false;

    /// <summary>
    /// il design degli elementi della form
    /// </summary>
    readonly Variant variant = Variant.Outlined;

    ScreenComponent screenComponent;

    string title = "";

    public void SaveTemplate()
    {

    }
}
