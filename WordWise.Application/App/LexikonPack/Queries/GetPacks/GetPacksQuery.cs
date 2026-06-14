using WordWise.Application.Messaging.Query;
using WordWise.Core.Lexikon.Repository;
using WordWise.Framework;

namespace WordWise.Application.App.LexikonPack.Queries.GetPacks;

public sealed record LexikonPackQueryResult(Guid Id, string Title, string Language);

public sealed record GetPacksQuery() : ICortexQuery<IReadOnlyList<LexikonPackQueryResult>>;

internal sealed class GetPacksQueryHandler(ILexikonPackRepository _languageRepository) : ICortexQueryHandler<GetPacksQuery, IReadOnlyList<LexikonPackQueryResult>>
{
    public async Task<Result<IReadOnlyList<LexikonPackQueryResult>>> Handle(GetPacksQuery request, CancellationToken cancellationToken)
    {
        var items = await _languageRepository.GetAllByLoadingGraphAsync(cancellationToken);

        return items.Select(i => new LexikonPackQueryResult(i.Id, i.Title, i.Language.Title)).ToList().AsReadOnly();
    }
}