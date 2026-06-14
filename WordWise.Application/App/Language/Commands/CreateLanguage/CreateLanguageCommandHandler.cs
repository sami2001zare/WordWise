using WordWise.Application.App.Language.Specification;
using WordWise.Application.Clock;
using WordWise.Application.Messaging.Command;
using WordWise.Core.Language.Repository;
using WordWise.Core.Lexikon;
using WordWise.Core.Lexikon.Repository;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.Language.Commands.CreateLanguage;

internal sealed class CreateLanguageCommandHandler(
    ILanguageRepository _languageRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<CreateLanguageCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
    {
        bool exists = await _languageRepository.ExistsAsync(new CreateLanguageSpecification(request), cancellationToken);

        if (exists)
        {
            return Result.Failure<Guid>(new Error("Exists", "Language Exsist"));
        }

        var lang = Core.Language.Language.Create(Guid.CreateVersion7(), request.Title, request.NativeTitle, request.Abbr, _dateTimeProvider.UtcNow);

        await _languageRepository.AddAsync(lang, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return lang.Id;
    }
}

public sealed record CreateLexikonSingleCommand(string Word, string POS, DifficultyLevel? DifficultyLevel) : ICortexCommand<Guid>;

internal sealed class CreateLexikonSingleCommandHandler(
    ILexikonRepository<EnglishLexikon> _lexikonRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<CreateLexikonSingleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateLexikonSingleCommand request, CancellationToken cancellationToken)
    {
        EnglishLexikon? lexikon = await _lexikonRepository.GetBySpecificationAsync(new EnglishLexikonSpecification(request), cancellationToken);

        if (lexikon is not null)
        {
            return Result.Failure<Guid>(new Error("Exists", "Language Exsist"));
        }

        lexikon = EnglishLexikon.Create(Guid.CreateVersion7(), request.Word, request.POS, _dateTimeProvider.UtcNow, Guid.Parse("English LexikonPack Guid"));

        await _lexikonRepository.AddAsync(lexikon, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return lexikon.Id;
    }
}


public sealed class EnglishLexikonSpecification(CreateLexikonSingleCommand lexi) : Specification<Lexikon>
{
    public override bool IsSatisfiedBy(Lexikon entity)
    {
        return entity.PartOfSpeech == lexi.POS && entity.Word == lexi.Word;
    }
}


public sealed record SingleLexikon(string Word, string POS, DifficultyLevel? DifficultyLevel);
public sealed record CreateLexikonMultipleCommmand(IEnumerable<SingleLexikon> Lexis) : ICortexCommand<IReadOnlyList<Guid>>;
internal sealed class CreateLexikonMultipleCommmandHandler(
    ILexikonRepository<EnglishLexikon> _lexikonRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<CreateLexikonMultipleCommmand, IReadOnlyList<Guid>>
{
    public async Task<Result<IReadOnlyList<Guid>>> Handle(CreateLexikonMultipleCommmand request, CancellationToken cancellationToken)
    {
        //EnglishLexikon? lexikon = await ;

        //if (lexikon is not null)
        //{
        //    return Result.Failure<IReadOnlyList<Guid>>(new Error("Exists", "Language Exsist"));
        //}

        

        List<EnglishLexikon> englishLexikons = [];

        //_lexikonRepository.GetBySpecificationAsync(new EnglishLexikonSpecification(request), cancellationToken)

        foreach (var item in request.Lexis)
        {
            englishLexikons.Add(EnglishLexikon.Create(Guid.CreateVersion7(), item.Word, item.POS, _dateTimeProvider.UtcNow, Guid.Parse("English LexikonPack Guid")));
        }

        await _lexikonRepository.AddRangeAsync(englishLexikons, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return englishLexikons.Select(i => i.Id).ToList();
    }
}
