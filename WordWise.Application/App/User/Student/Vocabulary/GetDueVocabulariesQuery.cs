using WordWise.Application.Caching;
using WordWise.Application.Messaging.Query;

namespace WordWise.Application.App.User.Student.Vocabulary;

public sealed record DueVocabularyResult(Guid SavedVocabularyId, Guid LexikonId, string Word, string PartOfSpeech, string? AudioUrl, string? ImageUrl);

public sealed record GetDueVocabulariesQuery(Guid StudentId) : ICortexQuery<IReadOnlyList<DueVocabularyResult>>, ICachedQuery
{
    public string CacheKey => $"due-vocabularies-{StudentId}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
