namespace WordWise.Core.Language.Repository;

public interface ILanguageRepository
{
    Task<Language?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IAsyncEnumerable<Language>> GetAllAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IAsyncEnumerable<T>> GetAllAsync<T>(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Language language, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Language> languages, CancellationToken cancellationToken = default);
}
