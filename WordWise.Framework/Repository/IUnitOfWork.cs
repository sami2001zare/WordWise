using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WordWise.Framework.Repository;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry Update(object entity);
    EntityEntry Remove(object entity);
}
