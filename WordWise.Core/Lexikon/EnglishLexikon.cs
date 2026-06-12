namespace WordWise.Core.Lexikon;

public sealed class EnglishLexikon : Lexikon
{
    private EnglishLexikon(Guid id, string word, string pos, string meaning, DateTime creationDatetime) : base(id, word, pos, meaning, creationDatetime) 
    {

    }


    public static EnglishLexikon Create(Guid id, string word, string pos, string meaning, DateTime creationDatetime)
    {
        EnglishLexikon lexikon = new(id, word, pos, meaning, creationDatetime);

        return lexikon;
    }
}
