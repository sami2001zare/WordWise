using WordWise.Application.Messaging.Command;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.User.Student.Settings;

internal sealed class ToggleRemindersCommandHandler(
    IStudentRepository _studentRepository,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<ToggleRemindersCommand>
{
    public async Task<Result> Handle(ToggleRemindersCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null) return Result.Failure(new Error("Student.NotFound", "Student not found."));

        student.ToggleReminders(request.Enabled);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
