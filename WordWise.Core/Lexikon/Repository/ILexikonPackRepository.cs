namespace WordWise.Core.Lexikon.Repository;

public interface ILexikonPackRepository
{
    Task<LexikonPack?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IAsyncEnumerable<LexikonPack>> GetAllAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IAsyncEnumerable<LexikonPack>> GetAllAsync<T>(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(LexikonPack lpack, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<LexikonPack> lpacks, CancellationToken cancellationToken = default);
}
