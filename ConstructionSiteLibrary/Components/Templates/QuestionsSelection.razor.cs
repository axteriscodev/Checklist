using ConstructionSiteLibrary.Model;
using ConstructionSiteLibrary.Model.TemplateWizard;
using ConstructionSiteLibrary.Repositories;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using Shared.Documents;
using Shared.Templates;
using ConstructionSiteLibrary.Components.Utilities;
using Shared.Defaults;

namespace ConstructionSiteLibrary.Components.Templates;

public partial class QuestionsSelection
{


    [Parameter]
    required public TemplateModel CurrentTemplate { get; set; }

    private const int NOT_ORDER = 10000; 
    private List<TemplateCategoryModel> categories = [];
    private List<CategoryQuestionsGroup> groups = [];

    private bool ChangeCategoryOrder;



    /// <summary>
    /// booleano che indica se la pagina sta eseguendo il caricamento iniziale
    /// </summary>
    private bool initialLoading;

    /// <summary>
    /// Booleano che è impostata durante una ricerca
    /// </summary>
    private bool isLoading = false;

    private bool onSaving = false;

    /// <summary>
    /// Intero che ci dice quanti sono gli elementi
    /// </summary>
    private int count;

    /// <summary>
    /// Riferimento al componente tabella
    /// </summary>
    private RadzenDataGrid<TemplateCategoryModel>? grid;


    /// <summary>
    /// il design degli elementi della form
    /// </summary>
    readonly Variant variant = Variant.Outlined;

    ScreenComponent screenComponent;

    [Parameter]
    public string Param { get; set; } = "";

    private List<string> _topicList = [];
    private string? _selectedTopic = null;

    protected override async Task OnInitializedAsync()
    {
        initialLoading = true;
        await base.OnInitializedAsync();
        await LoadData();
        initialLoading = false;
    }

    protected override async Task OnParametersSetAsync()
    {
        await LoadData();
    }

    #region Caricamento e Salvataggio

    private async Task LoadData()
    {
        groups = [];
        categories = await CategoriesRepository.GetCategories();
        count = categories.Count;
        //SelectTopics();
        //var tempCategories = SelectCategoriesFromTopic();
        int categoryCount = 0;

        foreach (var category in categories)
        {
            List<int> templateSelectedId = [];

            if (CurrentTemplate.Categories.Count != 0)
            {
                var tempCat = CurrentTemplate.Categories.Where(c => c.Id == category.Id).FirstOrDefault();

                if (tempCat is not null)
                {
                    foreach (var question in tempCat.Questions)
                    {
                        templateSelectedId.Add(question.Id);
                    }
                }
            }
            else
            {
                foreach (var question in category.Questions)
                {
                    templateSelectedId.Add(question.Id);
                }
            }

            bool? groupState = null;

            if (templateSelectedId.Count == 0)
            {
                groupState = false;
            }
            else if (templateSelectedId.Count == category.Questions.Count)
            {
                groupState = true;
            }

            //se non ho già la categoria nel gruppo la aggiungo
            if (!groups.Where(x => x.Id == category.Id).Any())
            {
                OrderElements(category.Questions);
                var order = groupState == false ? NOT_ORDER : categoryCount++;
                groups.Add(new() { Id = category.Id, Order = categoryCount, Text = category.Text, State = groupState, Questions = category.Questions, SelectedQuestionIds = templateSelectedId });
            }
            ReorderActiveCategory();
        }
    }

    public TemplateStepArgs OnSave()
    {
        onSaving = true;
        List<TemplateCategoryModel> templateCategories = [];

        foreach (var group in groups)
        {
            if (group.SelectedQuestionIds.Any())
            {
                var category = new TemplateCategoryModel()
                {
                    Id = group.Id,
                    Text = group.Text,
                    Order = group.Order,
                };

                foreach (var selectedQuestionId in group.SelectedQuestionIds)
                {
                    var selectedQuestion = group.Questions.First(x => x.Id == selectedQuestionId);
                    category.Questions.Add(selectedQuestion);
                }
                templateCategories.Add(category);
            }
        }

        CurrentTemplate.Categories = templateCategories;

        TemplateStepArgs args = new()
        {
            Object = CurrentTemplate,
            Step = TemplateStep.Questions,
        };

        return args;
    }

    private async Task ReloadTable()
    {
        DialogService.Close();
        await LoadData();
        await grid!.Reload();
    }

    #endregion 

    #region Visualizzazione

    private static string CategoryText(CategoryQuestionsGroup group)
    {
        string order = group.Order == NOT_ORDER ? "X" : group.Order.ToString();
        return   $"{order}. {group.Text}";
    }

    private static string QuestionText(CategoryQuestionsGroup group, string questionText, int order)
    {
        string gOrder = group.Order == NOT_ORDER ? "x" : group.Order.ToString();
        return gOrder + "." + order + " " + questionText;
    }

    private void ShowQuestions(CategoryQuestionsGroup group)
    {
        group.ShowQuestion = !group.ShowQuestion;
    }

    private string AccordionIcon(CategoryQuestionsGroup group)
    {
        return group.ShowQuestion ? "remove" : "add";
    }

    #endregion

    #region Selezione categorie e domande 

    private bool ChangeCheckBoxCategory(bool? value, CategoryQuestionsGroup group)
    {
        value ??= true;
        if (value.Value)
        {
            foreach (var q in group.Questions)
            {
                group.SelectedQuestionIds.Add(q.Id);
            }
        }
        else
        {
            group.SelectedQuestionIds = [];
        }
        group.State = value.Value;
        ReorderActiveCategory();
        return false;
    }

    private bool ValoreCheckBoxQuestion(int id, CategoryQuestionsGroup group)
    {
        return group.SelectedQuestionIds.Contains(id);
    }

    private bool ChangeCheckBoxQuestion(bool value, int id, CategoryQuestionsGroup group)
    {
        if (value)
        {
            group.SelectedQuestionIds.Add(id);
            group.State = group.SelectedQuestionIds.Count == group.Questions.Count ? true : null;
        }
        else
        {
            group.SelectedQuestionIds.Remove(id);
            group.State = group.SelectedQuestionIds.Count > 0 ? null : false;
        }
        if(group.State == false)
        {
            ReorderActiveCategory();
        }
        return false;
    }

    #endregion

    #region gestione topic

    public void SelectTopics()
    {
        _topicList = categories?.Select(c => c.Topic ?? "Tutto").Distinct().ToList() ?? [];
    }

    public List<TemplateCategoryModel> SelectCategoriesFromTopic()
    {
        if (string.IsNullOrEmpty(_selectedTopic))
        {
            return categories;
        }

        return categories.Where(c => c.Topic == _selectedTopic).ToList();
    }

    private void ChangeSelectedTopic(string args)
    {
        _selectedTopic = args;
        StateHasChanged();
    }

    #endregion

    #region Ordinamento domande 


    private void ReorderActiveCategory()
    {
        var count = 1;
        foreach(var g in groups)
        {
            if(!g.State.HasValue || g.State.Value)
            {
                g.Order = count;
                count++;
            }
            else
            {
                g.Order = NOT_ORDER;
            }        
        }

        //groups = groups.OrderBy(x => x.Order).ToList();
        StateHasChanged();
    }

    private void OrderCategories(ChangeObjectIndex indici)
    {

        var items = groups;
        var itemToMove = items[indici.OldIndex];
        items.RemoveAt(indici.OldIndex);

        if (indici.NewIndex < items.Count)
        {
            items.Insert(indici.NewIndex, itemToMove);
        }
        else
        {
            items.Add(itemToMove);
        }

        for (int i = 0; i < groups.Count; i++)
        {
            groups[i].Order = i + 1;
        }
    }

    private void OrderList(ChangeObjectIndex indice)
    {
        var items = groups.Where(x => x.Id == indice.GroupNumber).SingleOrDefault().Questions;
        var itemToMove = items[indice.OldIndex];
        items.RemoveAt(indice.OldIndex);

        if (indice.NewIndex < items.Count)
        {
            items.Insert(indice.NewIndex, itemToMove);
        }
        else
        {
            items.Add(itemToMove);
        }

        OrderElements(items);
        StateHasChanged();
    }

    private static void OrderElements(List<TemplateQuestionModel> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            lista[i].Order = i + 1;
        }
    }


    #endregion

    #region Ordinamento Categorie

    public void ToggleOrderCategory(bool isActive)
    {
        ChangeCategoryOrder = isActive;
        StateHasChanged();
    }

    public void Reorder(CategoryQuestionsGroup catGroup, bool forward)
    {
       var index = groups.IndexOf(groups.FirstOrDefault(x=>x.Id == catGroup.Id) ?? new());
        if(forward && index < groups.Count - 1)
        {
            groups.RemoveAt(index);
            groups.Insert(index + 1, catGroup);
            ReorderActiveCategory();
        }
        else if(!forward && index > 0)
        {
            groups.RemoveAt(index);
            groups.Insert(index-1, catGroup);
            ReorderActiveCategory();
        }
    }

    /// <summary>
    /// Controllo per disabilitare i bottoni nel primo (bottone indietro) e ultimo elemento (bottone avanti)
    /// </summary>
    /// <param name="catGroup"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    public bool DisableButton(CategoryQuestionsGroup catGroup, bool forward)
    {
        var disable = false;
        var index = groups.IndexOf(groups.FirstOrDefault(x => x.Id == catGroup.Id) ?? new());
        if(forward && index == groups.Count - 1)
        {
            disable = true;
        }
        if(!forward && index == 0)
        {
            disable = true;
        }
        return disable;
    }

    #endregion
}
