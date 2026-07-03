using WordWise.Core.User;

namespace WordWise.Core.User.Repositpry;

public interface ICredentialRepository
{
    Task<Credential?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Credential>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Credential?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Credential credential, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Credential> credentials, CancellationToken cancellationToken = default);
}