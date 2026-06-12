using WordWise.Core.Media;
using WordWise.Framework;

namespace WordWise.Core.User;

public sealed class TakenMedia : Entity
{
    private TakenMedia(Guid id, Guid studentId, Guid mediaBaseId, DateTime createDateTime)
    {
        Id = id;
        StudentId = studentId;
        MediaBaseId = mediaBaseId;
        CreateDateTime = createDateTime;
    }

    public Guid StudentId { get; set; }
    public Student Student { get; set; }

    public Guid MediaBaseId { get; set; }
    public MediaBase MediaBase { get; set; }

    public static TakenMedia Create(Guid id, Guid studentId, Guid mediaBaseId, DateTime createDateTime)
    {
        TakenMedia takenMedia = new(id, studentId, mediaBaseId, createDateTime);

        return takenMedia;
    }
}
