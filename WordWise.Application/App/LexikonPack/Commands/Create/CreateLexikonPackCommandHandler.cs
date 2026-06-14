using WordWise.Application.App.LexikonPack.Specification;
using WordWise.Application.Clock;
using WordWise.Application.Messaging.Command;
using WordWise.Core.Language.Repository;
using WordWise.Core.Lexikon.Repository;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.LexikonPack.Commands.Create;

internal sealed class CreateLexikonPackCommandHandler(
    ILexikonPackRepository _lexikonPackRepository,
    ILanguageRepository _languageRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork
    ) : ICortexCommandHandler<CreateLexikonPackCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateLexikonPackCommand request, CancellationToken cancellationToken)
    {
        var lexikonpack = await _lexikonPackRepository.ExistsBySpecificationAsync(new LexikonPackSpecification(request), cancellationToken);

        if (lexikonpack)
        {
            return Result.Failure<Guid>(new Error("Exists", "Already An Item Like This Exists In The System"));
        }

        Core.Language.Language? language = await _languageRepository.GetBySpecificationAsync(new GetLanguageByTitleSpecification(request.Title), cancellationToken);

        if (language == null)
        {
            return Result.Failure<Guid>(new Error("Language", "No Language Exists"));
        }

         var lexikonPack = Core.Lexikon.LexikonPack.Create(Guid.CreateVersion7(), request.Title, language.Id, _dateTimeProvider.UtcNow);

        try
        {
            await _lexikonPackRepository.AddAsync(lexikonPack, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lexikonPack.Id;
        }
        catch (Exception)
        {
            return Result.Failure<Guid>(new Error("System Error", "Error Happened While Saving Data"));
        }
    }
}
