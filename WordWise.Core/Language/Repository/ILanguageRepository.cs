namespace WordWise.Core.Language.Repository;

public interface ILanguageRepository
{
    Task<Language?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Language?> GetBySpecificationAsync(Specification<Language> specification, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Specification<Language> specification, CancellationToken cancellationToken = default);
    Task<IEnumerable<Language>> GetAllAsync(Guid id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<T> GetAllAsync<T>(CancellationToken cancellationToken = default);
    Task AddAsync(Language language, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Language> languages, CancellationToken cancellationToken = default);
}
