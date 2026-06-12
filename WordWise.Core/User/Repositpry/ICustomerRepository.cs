using WordWise.Core.User.ValueObjects;

namespace WordWise.Core.User.Repositpry;

public interface ICustomerRepository
{
    Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Student?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<Student?> GetByPhoneAsync(Phone phone, CancellationToken cancellationToken = default);
    Task<Student> GetCustomerGraphAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<IAsyncEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Student customer, CancellationToken cancellationToken = default);
    Task AddRangeAsync(ReadOnlyMemory<Student> customers, CancellationToken cancellationToken = default);
}
