using WordWise.Application.Clock;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.User.Student.Review;

internal sealed class ReviewVocabularyCommandHandler(
    IStudentRepository _studentRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<ReviewVocabularyCommand>
{
    public async Task<Result> Handle(ReviewVocabularyCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null) return Result.Failure(new Error("Student.NotFound", "Student not found."));

        var savedVocab = student.SavedVocabularies.FirstOrDefault(v => v.LexikonId == request.LexikonId);
        if (savedVocab is null) return Result.Failure(new Error("Vocabulary.NotFound", "Saved vocabulary not found."));

        savedVocab.MarkReviewed(_dateTimeProvider.UtcNow, request.IsRemembered);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
