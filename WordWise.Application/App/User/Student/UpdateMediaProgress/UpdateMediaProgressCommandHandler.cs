using WordWise.Application.Clock;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.User.Student.UpdateMediaProgress;

internal sealed class UpdateMediaProgressCommandHandler(
    IStudentRepository _studentRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<UpdateMediaProgressCommand>
{
    public async Task<Result> Handle(UpdateMediaProgressCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null) return Result.Failure(new Error("Student.NotFound", "Student not found."));

        var takenMedia = student.TakenMedias.FirstOrDefault(m => m.Id == request.TakenMediaId);
        if (takenMedia is null) return Result.Failure(new Error("TakenMedia.NotFound", "Media tracking record not found."));

        takenMedia.UpdateProgress(request.CurrentPosition, request.IsCompleted, _dateTimeProvider.UtcNow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
