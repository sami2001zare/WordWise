namespace WordWise.Core.Media.Series;

public sealed class Series : MediaBase
{
    private Series(Guid id, string title, Guid languageId, DateTime createDateTime, byte season, byte episod) : base(id, title, languageId, createDateTime)
    {
        Season = season;
        Episod = episod;
    }

    public byte Season { get; private set; }
    public byte Episod { get; private set; }

    public static Series Create(Guid id, string title, Guid languageId, DateTime createDateTime, byte season, byte episod)
    {
        Series series = new(id, title, languageId, createDateTime, season, episod);

        return series;
    }
}