using WordWise.Application.Messaging.Query;
using WordWise.Core.Language.Repository;
using WordWise.Framework;

namespace WordWise.Application.App.Language.Queries.GetLanguages;

public sealed record LanguageQueryResult(Guid Id, string Title);
public sealed record GetLanguagesQuery : ICortexQuery<IReadOnlyList<LanguageQueryResult>>;

internal sealed class GetLanguagesQueryHandler(ILanguageRepository _languageRepository) : ICortexQueryHandler<GetLanguagesQuery, IReadOnlyList<LanguageQueryResult>>
{
    public async Task<Result<IReadOnlyList<LanguageQueryResult>>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
    {
        var items = await _languageRepository.GetAllAsync<LanguageQueryResult>(cancellationToken).ToListAsync(cancellationToken);

        return items.AsReadOnly();
    }
}