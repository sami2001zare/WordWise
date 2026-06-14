using WordWise.Core.User.Events;
using WordWise.Framework;

namespace WordWise.Core.User.Student;

public sealed class UploadedContent : Entity
{
    private UploadedContent(Guid id, Guid studentId, string fileName, string fileLocation, DateTime createDateTime)
    {
        Id = id;
        StudentId = studentId;
        FileName = fileName;
        FileLocation = fileLocation;
        CreateDateTime = createDateTime;
    }

    protected UploadedContent()
    {

    }

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public string FileName { get; private set; } = null!;
    public string FileLocation { get; private set; } = null!;

    public List<UserLexikon> UserLexikon { get; private set; } = [];


    public static UploadedContent Create(Guid id, Guid studentId, string fileName, string fileLocation, DateTime createDateTime)
    {
        UploadedContent uploadedContent = new(id, studentId, fileName, fileLocation, createDateTime);

        uploadedContent.RaiseDomainEvent(new ContentUploadedEvent(id, fileLocation));

        return uploadedContent;
    }
}


public sealed class UserLexikon : Entity
{
    public Guid LexikonId { get; private set; }
    public Lexikon.Lexikon Lexikon { get; set; } = null!;


    public Guid UploadedContentId { get; private set; }
    public UploadedContent UploadedContent { get; set; } = null!;
}