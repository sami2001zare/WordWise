namespace WordWise.Core.Lexikon.Repository;

public interface ILexikonRepository : ILexikonRepository<Lexikon>;

public interface ILexikonRepository<TLexikon> where TLexikon : Lexikon
{
    Task<TLexikon?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<TLexikon> GetAllAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<TLexikon> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);
    Task AddAsync(TLexikon lexikon, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TLexikon> lexikons, CancellationToken cancellationToken = default);
}