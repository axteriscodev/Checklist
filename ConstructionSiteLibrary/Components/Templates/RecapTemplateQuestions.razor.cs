using Microsoft.AspNetCore.Components;
using Shared.Templates;

namespace ConstructionSiteLibrary.Components.Templates;

public partial class RecapTemplateQuestions
{

    [Parameter]
    public TemplateModel? SelectedTemplate { get; set; }

    private List<CategoryQuestionsGroup> groups = [];

    private bool onloading = false;

    protected override async Task OnInitializedAsync()
    {
        onloading = true;
        await base.OnInitializedAsync();
        LoadData();
        onloading = false;
    }

    public void LoadData()
    {
        groups = [];

        if (SelectedTemplate is not null)
        {
            foreach (var category in SelectedTemplate!.Categories)
            {
                //OrderElements(category.Questions.Cast<DocumentQuestionModel>());
                groups.Add(new() { Id = category.Id, Text = category.Text, Order = category.Order, Questions = category.Questions });
            }
        }


    }


    // public void OnTemplateSelected()
    // {
    //     groups = [];

    //     foreach (var category in SelectedTemplate!.Categories)
    //     {
    //         //OrderElements(category.Questions.Cast<DocumentQuestionModel>());
    //         groups.Add(new() { Id = category.Id, Text = category.Text, Order = category.Order, Questions = category.Questions });
    //     }
    // }

    private static string CategoryText(CategoryQuestionsGroup group)
    {
        return group.Order + ". " + group.Text;
    }

    private static string QuestionText(CategoryQuestionsGroup group, string questionText, int order)
    {
        return group.Order + "." + order + " " + questionText;
    }

    private void ShowQuestions(CategoryQuestionsGroup group)
    {
        group.ShowQuestion = !group.ShowQuestion;
    }

    private string AccordionIcon(CategoryQuestionsGroup group)
    {
        return group.ShowQuestion ? "remove" : "add";
    }

}
