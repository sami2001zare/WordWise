using WordWise.Framework;

namespace WordWise.Core.Media.Subtitle;

public sealed class SubtitleTrack : Entity
{
    private SubtitleTrack(Guid id, Guid mediaBaseId, Guid languageId, string label)
    {
        Id = id;
        MediaBaseId = mediaBaseId;
        LanguageId = languageId;
        Label = label;
    }

    protected SubtitleTrack() { }

    public Guid MediaBaseId { get; private set; }
    public Guid LanguageId { get; private set; }
    public string Label { get; private set; } = null!;

    public List<SubtitleLine> Lines { get; private set; } = [];

    public static SubtitleTrack Create(Guid id, Guid mediaBaseId, Guid languageId, string label)
    {
        return new SubtitleTrack(id, mediaBaseId, languageId, label);
    }

    public void AddLine(SubtitleLine line)
    {
        Lines.Add(line);
    }
}