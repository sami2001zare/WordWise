using WordWise.Core.Media;
using WordWise.Framework;

namespace WordWise.Core.User.Student;


public sealed class TakenMedia : Entity
{
    private TakenMedia(Guid id, Guid studentId, Guid mediaBaseId, DateTime createDateTime)
    {
        Id = id;
        StudentId = studentId;
        MediaBaseId = mediaBaseId;
        CreateDateTime = createDateTime;
    }

    protected TakenMedia()
    {

    }

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public Guid MediaBaseId { get; set; }
    public MediaBase MediaBase { get; set; } = null!;

    public TimeSpan CurrentPosition { get; private set; } = TimeSpan.Zero;
    public bool IsCompleted { get; private set; } = false;
    public DateTime LastAccessedAt { get; private set; }

    public static TakenMedia Create(Guid id, Guid studentId, Guid mediaBaseId, DateTime createDateTime)
    {
        TakenMedia takenMedia = new(id, studentId, mediaBaseId, createDateTime)
        {
            LastAccessedAt = createDateTime
        };

        return takenMedia;
    }

    public void UpdateProgress(TimeSpan currentPosition, bool isCompleted, DateTime accessedAt)
    {
        CurrentPosition = currentPosition;
        IsCompleted = isCompleted;
        LastAccessedAt = accessedAt;
    }
}