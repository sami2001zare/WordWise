using WordWise.Application.Clock;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User.Repositpry;
using WordWise.Core.User.Student;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.User.Student.StartMedia;

internal sealed class StartMediaCommandHandler(
    IStudentRepository _studentRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<StartMediaCommand, Guid>
{
    public async Task<Result<Guid>> Handle(StartMediaCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null) return Result.Failure<Guid>(new Error("Student.NotFound", "Student not found."));

        // Check if already taken
        var existingMedia = student.TakenMedias.FirstOrDefault(m => m.MediaBaseId == request.MediaBaseId);
        if (existingMedia != null)
        {
            return Result.Success(existingMedia.Id);
        }

        var takenMediaId = Guid.CreateVersion7();
        var takenMedia = TakenMedia.Create(takenMediaId, student.Id, request.MediaBaseId, _dateTimeProvider.UtcNow);

        student.RecordTakenMedia(takenMedia);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(takenMediaId);
    }
}
