using WordWise.Application.Clock;
using WordWise.Application.Messaging.Command;
using WordWise.Core.Lexikon;
using WordWise.Core.Lexikon.Repository;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.LexikonItem.Commands.Create;

internal sealed class CreateLexikonCommandHandler(
    ILexikonRepository _lexikonRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<CreateLexikonCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateLexikonCommand request, CancellationToken cancellationToken)
    {
        //    var difficulty = request.DifficultyLevelValue.HasValue
        //&& Enum.IsDefined(typeof(DifficultyLevel), request.DifficultyLevelValue.Value)
        //? (DifficultyLevel)request.DifficultyLevelValue.Value
        //: null;

        DifficultyLevel? difficulty = request.DifficultyLevelValue.HasValue
    ? (DifficultyLevel)request.DifficultyLevelValue.Value
    : null;

        var id = Guid.CreateVersion7();
        var lexikon = Lexikon.Create(
            id,
            request.Word,
            request.PartOfSpeech,
            _dateTimeProvider.UtcNow,
            request.LexikonPackId);

        //lexikon.DifficultyLevel = difficulty;

        lexikon.SetMedia(request.AudioUrl, request.ImageUrl);

        foreach (var sentence in request.ExampleSentences)
        {
            lexikon.AddExampleSentence(ExampleSentence.Create(Guid.CreateVersion7(), id, sentence.Text, sentence.Translation, sentence.Source));
        }

        foreach (var synonym in request.Synonyms)
        {
            lexikon.AddSynonym(Synonym.Create(Guid.CreateVersion7(), id, synonym));
        }

        foreach (var antonym in request.Antonyms)
        {
            lexikon.AddAntonym(Antonym.Create(Guid.CreateVersion7(), id, antonym));
        }

        await _lexikonRepository.AddAsync(lexikon, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(lexikon.Id);
    }
}