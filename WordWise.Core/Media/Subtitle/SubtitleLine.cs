using WordWise.Framework;

namespace WordWise.Core.Media.Subtitle;

public sealed class SubtitleLine : Entity
{
    private SubtitleLine(Guid id, Guid subtitleId, TimeSpan startTime, TimeSpan endTime, string text)
    {
        Id = id;
        SubtitleId = subtitleId;
        StartTime = startTime;
        EndTime = endTime;
        Text = text;
    }

    protected SubtitleLine() { }

    public Guid SubtitleId { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public string Text { get; private set; } = null!;

    public static SubtitleLine Create(Guid id, Guid subtitleId, TimeSpan startTime, TimeSpan endTime, string text)
    {
        return new SubtitleLine(id, subtitleId, startTime, endTime, text);
    }
}
