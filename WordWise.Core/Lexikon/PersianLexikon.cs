namespace WordWise.Core.Lexikon;

public sealed class PersianLexikon : Lexikon
{
    private PersianLexikon(Guid id, string word, string pos, string meaning, DateTime creationDatetime) : base(id, word, pos, meaning, creationDatetime) 
    {

    }

    protected PersianLexikon()
    {
        
    }

    public static PersianLexikon Create(Guid id, string word, string pos, string meaning, DateTime creationDatetime)
    {
        PersianLexikon lexikon = new(id, word, pos, meaning, creationDatetime);

        return lexikon;
    }
}
