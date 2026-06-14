namespace WordWise.Core.Lexikon;

public sealed class PersianLexikon : Lexikon
{
    private PersianLexikon(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId) : base(id, word, pos, creationDatetime, lexikonPackId)
    {

    }

    private PersianLexikon(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId, DifficultyLevel? difficultyLevel) : base(id, word, pos, creationDatetime, lexikonPackId, difficultyLevel)
    {

    }

    protected PersianLexikon() { }


    public new static PersianLexikon Create(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId)
    {
        PersianLexikon lexikon = new(id, word, pos, creationDatetime, lexikonPackId);

        return lexikon;
    }

    public new static PersianLexikon Create(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId, DifficultyLevel? difficultyLevel)
    {
        PersianLexikon lexikon = new(id, word, pos, creationDatetime, lexikonPackId, difficultyLevel);

        return lexikon;
    }
}
