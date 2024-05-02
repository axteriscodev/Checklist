using AXT_WebComunication.WebResponse;
using ClientWebApp.Managers;
using Microsoft.AspNetCore.Components;
using Radzen;
using Shared;

namespace ClientWebApp.Components.Questions;

public partial class FormQuestions
{
    [Parameter]
    public EventCallback OnSaveComplete { get; set; }
    [Parameter]
    public bool CreationMode { get; set; }
    [Parameter]
    public QuestionModel? Question { get; set; }

    /// <summary>
    /// Classe utilizzata per incapsulare le informazioni relative alla scelta dell'utente
    /// </summary>
    private FormQuestionData form = new();
    /// <summary>
    /// il design degli elementi della form
    /// </summary>
    readonly Variant variant = Variant.Outlined;
    /// <summary>
    /// 
    /// </summary>
    private bool onSaving = false;

    /// <summary>
    /// Metodo invocato quando il componente è pronto per essere avviato
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        Setup();
    }

    /// <summary>
    /// Metodo che inizializza le impostazioni iniziali del componente sia in caso di creazione 
    /// che in modifica
    /// </summary>
    private void Setup()
    {
        if (!CreationMode && Question is not null)
        {
            form = new FormQuestionData()
            {
                Id = Question.Id,
                Text = Question.Text,
                IdCategory = Question.IdCategory,
                IdSubject = Question.IdSubject,
                Choices = Question.Choices,
                CurrentChoice = Question.CurrentChoice,
                Note = Question.Note,
                Printable = Question.Printable,
                Hidden = Question.Hidden,
            };
        }
    }

    private async Task Save()
    {
        onSaving = true;
        AXT_WebResponse response;
        if (CreationMode)
        {
            response = await HttpManager.SendHttpRequest("Question/SaveQuestion", new QuestionModel
            {
                Text = form.Text, 
                IdCategory = form.IdCategory,
                IdSubject = form.IdSubject,
                Choices = form.Choices,
                CurrentChoice = form.CurrentChoice,
                Note = form.Note ?? "",
                Printable = form.Printable,
                Hidden = form.Hidden,
            });
        }
        else
        {
            List<QuestionModel> list = [];
            list.Add(new()
            {
                Id = form.Id,
                Text = form.Text,
                IdCategory = form.IdCategory,
                IdSubject = form.IdSubject,
                Choices = form.Choices,
                CurrentChoice = form.CurrentChoice,
                Note = form.Note ?? "",
                Printable = form.Printable,
                Hidden = form.Hidden,
            });
            response = await HttpManager.SendHttpRequest("Question/UpdateQuestion", list);
        }

        if (response.Code.Equals("0"))
        {
            await OnSaveComplete!.InvokeAsync();
        }
        onSaving = false;
    }



    private class FormQuestionData
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int IdCategory { get; set; }
        public int? IdSubject { get; set; }
        public List<ChoiceModel> Choices { get; set; }
        public ChoiceModel CurrentChoice { get; set; }
        public string? Note { get; set; }
        public bool Printable { get; set; }
        public bool Hidden { get; set; }
    }
}
