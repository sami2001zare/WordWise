using WordWise.Core.User.ValueObjects;

namespace WordWise.Core.User.Repositpry;

public interface IStudentRepository
{
    Task<Student.Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Student.Student?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<Student.Student?> GetByPhoneAsync(Phone phone, CancellationToken cancellationToken = default);
    Task<Student.Student> GetCustomerGraphAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<IAsyncEnumerable<Student.Student>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Student.Student customer, CancellationToken cancellationToken = default);
    Task AddRangeAsync(ReadOnlyMemory<Student.Student> customers, CancellationToken cancellationToken = default);
}
