namespace WordWise.Core.Lexikon.Repository;

public interface ILexikonPackRepository
{
    Task<LexikonPack?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<LexikonPack?> GetBySpecificationAsync(Specification<LexikonPack> specification, CancellationToken cancellationToken = default);
    Task<bool> ExistsBySpecificationAsync(Specification<LexikonPack> specification, CancellationToken cancellationToken = default);
    Task<IEnumerable<LexikonPack>> GetAllAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<LexikonPack>> GetAllAsync<T>(CancellationToken cancellationToken = default);
    Task<IEnumerable<LexikonPack>> GetAllByLoadingGraphAsync(CancellationToken cancellationToken = default);
    Task AddAsync(LexikonPack lpack, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<LexikonPack> lpacks, CancellationToken cancellationToken = default);
}