namespace WordWise.Core.Lexikon;

public sealed class EnglishLexikon : Lexikon
{
    private EnglishLexikon(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId) : base(id, word, pos, creationDatetime, lexikonPackId)
    {

    }

    private EnglishLexikon(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId, DifficultyLevel? difficultyLevel) : base(id, word, pos, creationDatetime, lexikonPackId, difficultyLevel)
    {

    }

    protected EnglishLexikon() { }

    public new static EnglishLexikon Create(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId)
    {
        EnglishLexikon lexikon = new(id, word, pos, creationDatetime, lexikonPackId);

        return lexikon;
    }

    public new static EnglishLexikon Create(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId, DifficultyLevel? difficultyLevel)
    {
        EnglishLexikon lexikon = new(id, word, pos, creationDatetime, lexikonPackId, difficultyLevel);

        return lexikon;
    }
}
