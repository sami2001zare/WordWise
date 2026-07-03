using WordWise.Application.Caching;
using WordWise.Application.Messaging.Query;

namespace WordWise.Application.App.LexikonItem.Commands.Queries.Search;

public sealed record LexikonItemResult(Guid Id, string Word, string PartOfSpeech, string? AudioUrl, string? ImageUrl);

// R-45: Search by Target Word or Translation
public sealed record SearchLexikonQuery(string SearchTerm) : ICortexQuery<IReadOnlyList<LexikonItemResult>>, ICachedQuery
{
    public string CacheKey => $"lexikon-search-{SearchTerm.ToLowerInvariant()}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(15);
}
