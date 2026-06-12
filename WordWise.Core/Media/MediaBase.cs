using WordWise.Framework;

namespace WordWise.Core.Media;

public class MediaBase : Entity
{
    private protected MediaBase(Guid id, string title, Guid languageId, DateTime createDateTime)
    {
        Id = id;
        Title = title;
        LanguageId = languageId;
        CreateDateTime = createDateTime;
    }

    protected MediaBase()
    {

    }

    public string Title { get; private set; }

    public string? Thumbnail { get; private set; } = null;

    public Guid LanguageId { get; private set; }
    public Language.Language Language { get; private set; } = null!;

    public void SetThumbnail(string thumbnail)
    {
        Thumbnail = thumbnail;
    }

    public static MediaBase Create(Guid id, string title, Guid languageId, DateTime createDateTime)
    {
        MediaBase mediaBase = new(id, title, languageId, createDateTime);

        return mediaBase;
    }
}
