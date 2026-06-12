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

    public string Title { get; set; }

    public Guid LanguageId { get; set; }
    public Language.Language Language { get; set; }

    public static LexikonPack Create(Guid id, string title, Guid languageId)
    {
        LexikonPack lexikonPack = new(id, title, languageId);

        return lexikonPack;
    }

    public static LexikonPack Create(Guid id, string title, Guid languageId, DateTime createDateTime)
    {
        LexikonPack lexikonPack = new(id, title, languageId, createDateTime);

        return lexikonPack;
    }
}