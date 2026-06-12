namespace WordWise.Core.Media.Film;

public sealed class Film : MediaBase
{
    private Film(Guid id, string title, Guid languageId, DateTime createDateTime, Genere genere) : base(id, title, languageId, createDateTime)
    {
        Genere = genere;
    }

    public Genere Genere { get; private set; }

    public static Film Create(Guid id, string title, Guid languageId, DateTime createDateTime, Genere genere)
    {
        Film series = new(id, title, languageId, createDateTime, genere);

        return series;
    }
}
