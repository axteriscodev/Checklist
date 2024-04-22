using System.Reflection.Metadata;

namespace Shared;

public class DocumentFactory
{
    public QuestionRepository QuestionRepository { get; set; } = new QuestionRepository();

    public List<Question> SelectedQuestion = [];

    public Document createDocument()
    {
        return new Document();
    }
}
