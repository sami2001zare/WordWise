using WordWise.Framework;

namespace WordWise.Core.Language;

public sealed class Language : Entity
{
    private Language(Guid id, string title, string nativeTitle, string abbrivation)
    {
        Id = id;
        Title = title;
        NativeTitle = nativeTitle;
        Abbrivation = abbrivation;
    }

    private Language(Guid id, string title, string nativeTitle, string abbrivation, DateTime creationDatetime) : this(id, title, nativeTitle, abbrivation)
    {
        CreateDateTime = creationDatetime;
    }

    protected Language() { }

    public string Title { get; set; } = null!;
    public string NativeTitle { get; set; } = null!;
    public string Abbrivation { get; set; } = null!;


    public static Language Create(Guid id, string title, string nativeTitle, string abbrivation)
    {
        Language lang = new(id, title, nativeTitle, abbrivation);

        // lang.RaiseDomainEvent();

        return lang;
    }

    public static Language Create(Guid id, string title, string nativeTitle, string abbrivation, DateTime createDatetime)
    {
        Language lang = new(id, title, nativeTitle, abbrivation, createDatetime);

        // lang.RaiseDomainEvent();

        return lang;
    }
}
