using ConstructionSiteLibrary.Repositories;
using Microsoft.AspNetCore.Components;
using Shared;

namespace ConstructionSiteLibrary.Components.FormCompilation;

public partial class FormCompilation
{

    private DocumentModel documentModel = new();

    /// <summary>
    /// booleano che indica se la pagina sta eseguendo il caricamento iniziale
    /// </summary>
    private bool initialLoading;

    private int CurrentSelection;

    IEnumerable<int> DocumentsList = [];
    List<VisualCategory> visualCategories = [];

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
        List<DocumentModel> docLists = await DocumentsRepository.GetDocuments();

        if (docLists.Count != 0)
        {
            DocumentsList = docLists.Select(x => x.Id);
            CurrentSelection = docLists.First().Id;

            documentModel = await DocumentsRepository.GetDocumentById(CurrentSelection);
            CreateVisualCategories();
        }
    }

    private async Task OnDocumentSelected()
    {
        documentModel = await DocumentsRepository.GetDocumentById(CurrentSelection);
        CreateVisualCategories();
    }

    private void CreateVisualCategories()
    {
        visualCategories = [];
        foreach (var cat in documentModel.Categories)
        {
            visualCategories.Add(new() { Category = cat });
        }
    }

    private async Task SaveForm()
    {
       await DocumentsRepository.UpdateDocuments([documentModel]);
    }

    #region Visualizzazione

    private static string CategoryText(CategoryModel cat)
    {
        return cat.Order + ". " + cat.Text;
    }

    private static string QuestionText(CategoryModel cat, string questionText, int order)
    {
        return cat.Order + "." + order + " " + questionText;
    }

    private static void ShowQuestions(VisualCategory cat)
    {
        cat.ShowQuestion = !cat.ShowQuestion;
    }

    private static string AccordionIcon(VisualCategory cat)
    {
        return cat.ShowQuestion ? "remove" : "add";
    }

    #endregion

    #region Classe interna 

    class VisualCategory
    {
        public bool ShowQuestion = true;
        public CategoryModel Category { get; set; } = new();
    }

    #endregion
}
