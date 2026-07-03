using WordWise.Application.Caching;
using WordWise.Application.Messaging.Query;

namespace WordWise.Application.App.User.Student.ExportVocabulary;

public sealed record ExportVocabularyQuery(Guid StudentId) : ICortexQuery<byte[]>, ICachedQuery
{
    // Export can be heavy, cache for 1 hour to prevent spamming the export button
    public string CacheKey => $"export-vocab-{StudentId}";
    public TimeSpan? Expiration => TimeSpan.FromHours(1);
}
