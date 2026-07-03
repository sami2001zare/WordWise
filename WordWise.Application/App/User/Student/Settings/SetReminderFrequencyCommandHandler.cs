using WordWise.Application.Messaging.Command;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.User.Student.Settings;

internal sealed class SetReminderFrequencyCommandHandler(
    IStudentRepository _studentRepository,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<SetReminderFrequencyCommand>
{
    public async Task<Result> Handle(SetReminderFrequencyCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null) return Result.Failure(new Error("Student.NotFound", "Student not found."));

        student.SetReminderFrequency(request.Frequency);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}