namespace WordWise.Core.Media.Repository;

public interface IMediaRepository<TMedia> where TMedia : MediaBase
{
    Task<TMedia?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IAsyncEnumerable<TMedia>> GetAllAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IAsyncEnumerable<T>> GetAllAsync<T>(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(TMedia language, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TMedia> languages, CancellationToken cancellationToken = default);
}

public interface IMediaRepository : IMediaRepository<MediaBase>;