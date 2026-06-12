using WordWise.Core.Media;
using WordWise.Framework;

namespace WordWise.Core.Lexikon;

public class Lexikon : Entity
{
    protected Lexikon(Guid id, string word, string pos, string meaning, DateTime creationDatetime)
    {
        Id = id;
        Word = word;
        PartOfSpeech = pos;
        CreateDateTime = creationDatetime;
        Meaning = meaning;
    }

    protected Lexikon() { }

    public string Word { get; protected set; }

    public string PartOfSpeech { get; protected set; }

    public string Meaning { get; protected set; }

    public DifficultyLevel DifficultyLevel { get; private set; }

    public static Lexikon Create(Guid id, string word, string pos, string meaning, DateTime creationDatetime)
    {
        Lexikon lexikon = new(id, word, pos, meaning, creationDatetime);

        return lexikon;
    }
}

