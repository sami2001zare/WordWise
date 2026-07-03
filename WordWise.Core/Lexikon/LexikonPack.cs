using WordWise.Framework;

namespace WordWise.Core.Lexikon;


public sealed class LexikonPack : Entity
{
    private LexikonPack(Guid id, string title, Guid languageId)
    {
        Id = id;
        Title = title;
        LanguageId = languageId;
    }

    private LexikonPack(Guid id, string title, Guid languageId, DateTime createDateTime) : this(id, title, languageId)
    {
        CreateDateTime = createDateTime;
    }

    protected LexikonPack() { }

    public string Title { get; private set; }

    public Guid LanguageId { get; private set; }
    public Language.Language Language { get; set; }

    public List<Lexikon> Lexikons { get; private set; } = [];

    public static LexikonPack Create(Guid id, string title, Guid languageId)
    {
        LexikonPack lexikonPack = new(id, title, languageId);

        return lexikonPack;
    }

    public void SetTitle(string title)
    {
        Title = title;
    }

    public static LexikonPack Create(Guid id, string title, Guid languageId, DateTime createDateTime)
    {
        LexikonPack lexikonPack = new(id, title, languageId, createDateTime);

        return lexikonPack;
    }
}