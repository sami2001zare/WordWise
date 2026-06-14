using WordWise.Framework;

namespace WordWise.Core.Lexikon;

public class Lexikon : Entity
{
    //protected Lexikon(Guid id, string word, string pos, string meaning, DateTime creationDatetime, Guid lexikonPackId)
    //{
    //    Id = id;
    //    Word = word;
    //    PartOfSpeech = pos;
    //    CreateDateTime = creationDatetime;
    //    Meaning = meaning;
    //    LexikonPackId = lexikonPackId;
    //}

    private protected Lexikon(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId)
    {
        Id = id;
        Word = word;
        PartOfSpeech = pos;
        CreateDateTime = creationDatetime;
        LexikonPackId = lexikonPackId;
    }

    private protected Lexikon(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId, DifficultyLevel? difficultyLevel) : this(id, word, pos, creationDatetime, lexikonPackId)
    {
        DifficultyLevel = difficultyLevel;
    }

    protected Lexikon() { }

    public string Word { get; protected set; } = null!;

    public string PartOfSpeech { get; protected set; } = null!;

    //public string? Meaning { get; protected set; }

    public DifficultyLevel? DifficultyLevel { get; private set; }

    public Guid LexikonPackId { get; private set; }
    public LexikonPack LexikonPack { get; set; } = null!;

    //public static Lexikon Create(Guid id, string word, string pos, string meaning, DateTime creationDatetime, Guid lexikonPackId)
    //{
    //    Lexikon lexikon = new(id, word, pos, meaning, creationDatetime, lexikonPackId);

    //    return lexikon;
    //}

    public static Lexikon Create(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId)
    {
        Lexikon lexikon = new(id, word, pos, creationDatetime, lexikonPackId);

        return lexikon;
    }

    public static Lexikon Create(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId, DifficultyLevel? difficultyLevel)
    {
        Lexikon lexikon = new(id, word, pos, creationDatetime, lexikonPackId, difficultyLevel);

        return lexikon;
    }
}


public class Translation : Entity
{
    private Translation(Guid id, string translation, DateTime creationDatetime)
    {
        Id = id;
        Content = translation;
        CreateDateTime = creationDatetime;
    }

    protected Translation() { }


    public string Content { get; private set; } = null!;


    public static Translation Create(Guid id, string translation, DateTime creationDatetime)
    {
        Translation trans = new(id, translation, creationDatetime);

        return trans;
    }
}


public sealed class MeaningDictionary : Entity
{
    private MeaningDictionary(Guid id, Guid lexikonId, Guid translationId, DateTime creationDatetime)
    {
        Id = id;
        LexikonId = lexikonId;
        TranslationId = translationId;
        CreateDateTime = creationDatetime;
    }

    protected MeaningDictionary() { }


    public Guid? LexikonId { get; private set; }
    public Lexikon Lexikon { get; set; } = null!;

    public Guid? TranslationId { get; private set; }
    public Translation Translation { get; set; } = null!;


    public static MeaningDictionary Create(Guid id, Guid lexikonId, Guid translationId, DateTime creationDatetime)
    {
        MeaningDictionary meaningDictionary = new(id, lexikonId, translationId, creationDatetime);

        return meaningDictionary;
    }
}