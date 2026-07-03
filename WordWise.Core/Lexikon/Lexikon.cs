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

    public DifficultyLevel? DifficultyLevel { get; internal set; }

    public string? AudioUrl { get; private set; }
    public string? ImageUrl { get; private set; }

    public Guid LexikonPackId { get; private set; }
    public LexikonPack LexikonPack { get; set; } = null!;

    private readonly List<ExampleSentence> _exampleSentences = [];
    public IReadOnlyCollection<ExampleSentence> ExampleSentences => _exampleSentences.AsReadOnly();

    private readonly List<Synonym> _synonyms = [];
    public IReadOnlyCollection<Synonym> Synonyms => _synonyms.AsReadOnly();

    private readonly List<Antonym> _antonyms = [];
    public IReadOnlyCollection<Antonym> Antonyms => _antonyms.AsReadOnly();

    public static Lexikon Create(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId)
    {
        return new Lexikon(id, word, pos, creationDatetime, lexikonPackId);
    }

    public static Result<Lexikon> CreateWithValidation(Guid id, string word, string pos, DateTime creationDatetime, Guid lexikonPackId, DifficultyLevel? difficultyLevel)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            return Result.Failure<Lexikon>(new Error("Lexikon.EmptyWord", "Word cannot be empty."));
        }

        if (string.IsNullOrWhiteSpace(pos))
        {
            return Result.Failure<Lexikon>(new Error("Lexikon.EmptyPartOfSpeech", "Part of speech cannot be empty."));
        }

        return Result.Success(new Lexikon(id, word, pos, creationDatetime, lexikonPackId, difficultyLevel));
    }

    public void SetMedia(string? audioUrl, string? imageUrl)
    {
        AudioUrl = audioUrl;
        ImageUrl = imageUrl;
    }

    public void AddExampleSentence(ExampleSentence sentence)
    {
        _exampleSentences.Add(sentence);
    }

    public void AddSynonym(Synonym synonym)
    {
        _synonyms.Add(synonym);
    }

    public void AddAntonym(Antonym antonym)
    {
        _antonyms.Add(antonym);
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


public class ExampleSentence : Entity
{
    private ExampleSentence(Guid id, Guid lexikonId, string text, string? translation, string? source)
    {
        Id = id;
        LexikonId = lexikonId;
        Text = text;
        Translation = translation;
        Source = source;
    }

    protected ExampleSentence() { }

    public Guid LexikonId { get; private set; }
    public string Text { get; private set; } = null!;
    public string? Translation { get; private set; }
    public string? Source { get; private set; }

    public static ExampleSentence Create(Guid id, Guid lexikonId, string text, string? translation = null, string? source = null)
    {
        return new ExampleSentence(id, lexikonId, text, translation, source);
    }
}

public class Synonym : Entity
{
    private Synonym(Guid id, Guid lexikonId, string word)
    {
        Id = id;
        LexikonId = lexikonId;
        Word = word;
    }

    protected Synonym() { }

    public Guid LexikonId { get; private set; }
    public string Word { get; private set; } = null!;

    public static Synonym Create(Guid id, Guid lexikonId, string word)
    {
        return new Synonym(id, lexikonId, word);
    }
}

public class Antonym : Entity
{
    private Antonym(Guid id, Guid lexikonId, string word)
    {
        Id = id;
        LexikonId = lexikonId;
        Word = word;
    }

    protected Antonym() { }

    public Guid LexikonId { get; private set; }
    public string Word { get; private set; } = null!;

    public static Antonym Create(Guid id, Guid lexikonId, string word)
    {
        return new Antonym(id, lexikonId, word);
    }
}