using WordWise.Application.Clock;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User.Repositpry;
using WordWise.Core.User.Student;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.User.Student.SaveVocabulary;

internal sealed class SaveVocabularyCommandHandler(
    IStudentRepository _studentRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<SaveVocabularyCommand, Guid>
{
    public async Task<Result<Guid>> Handle(SaveVocabularyCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null) return Result.Failure<Guid>(new Error("Student.NotFound", "Student not found."));

        var savedVocabId = Guid.CreateVersion7();
        var savedVocab = SavedVocabulary.Create(savedVocabId, student.Id, request.LexikonId, _dateTimeProvider.UtcNow);

        student.SaveVocabulary(savedVocab);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(savedVocabId);
    }
}
