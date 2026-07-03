using Microsoft.EntityFrameworkCore;
using System.Net;
using WordWise.Core.Language;
using WordWise.Core.Language.Repository;
using WordWise.Core.Lexikon;
using WordWise.Core.Lexikon.Repository;
using WordWise.Core.User;
using WordWise.Core.User.Repositpry;
using WordWise.Core.User.Student;
using WordWise.Core.User.ValueObjects;
using WordWise.Infra.Data;


namespace WordWise.Infra.Repositories;

internal sealed class StudentRepository(WordWiseDbContext dbContext) : IStudentRepository
{
    public async Task<Student?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await dbContext.Students.FirstOrDefaultAsync(s => s.Email != null && s.Email.Value == email.Value, cancellationToken);
    }

    public async Task<Student?> GetByPhoneAsync(Phone phone, CancellationToken cancellationToken = default)
    {
        return await dbContext.Students.FirstOrDefaultAsync(s => s.Phone.Value == phone.Value, cancellationToken);
    }

    public async Task<Student> GetCustomerGraphAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        // Using explicit paths and split queries for performance on large graphs
        return await dbContext.Students
            .Include(s => s.SavedVocabularies)
            .Include(s => s.TakenMedias)
            .Include(s => s.UploadedContents)
            .AsSplitQuery()
            .FirstAsync(s => s.Id == customerId, cancellationToken);
    }

    public Task AddRangeAsync(ReadOnlyMemory<Student> customers, CancellationToken cancellationToken = default)
    {
        return dbContext.Students.AddRangeAsync(customers.ToArray(), cancellationToken);
    }

    public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Students.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Student customer, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(customer, cancellationToken);
    }

    public async Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Students.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

}


internal sealed class OneTimePasswordRepository(WordWiseDbContext dbContext) : IOneTimePasswordRepository
{
    public async Task<OneTimePassword?> GetLatestByPhoneAsync(Phone phone, CancellationToken cancellationToken = default)
    {
        return await dbContext.OneTimePasswords
            .Where(o => o.EmailOrPhone == phone.Value)
            .OrderByDescending(o => o.CreateDateTime)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OneTimePassword?> GetLatestByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await dbContext.OneTimePasswords
            .Where(o => o.EmailOrPhone == email.Value)
            .OrderByDescending(o => o.CreateDateTime)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<OneTimePassword>> GetLatestsByPhoneAsync(Phone phone, CancellationToken cancellationToken = default)
    {
         return await dbContext.OneTimePasswords.Where(p => p.EmailOrPhone == phone.Value).OrderByDescending(i => i.EmailOrPhone).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(OneTimePassword otp, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(otp, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<OneTimePassword> otps, CancellationToken cancellationToken = default)
    {
        await dbContext.AddRangeAsync(otps, cancellationToken);
    }
}


internal sealed class LexikonRepository(WordWiseDbContext dbContext) : ILexikonRepository
{
    public async Task<Lexikon?> GetBySpecificationAsync(Specification<Lexikon> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.Lexikons
            .Include(l => l.ExampleSentences)
            .Include(l => l.Synonyms)
            .Include(l => l.Antonyms)
            .FirstOrDefaultAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async Task<bool> ExistBySpecificationAsync(Specification<Lexikon> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.Lexikons.AnyAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async IAsyncEnumerable<Lexikon> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        var items = await dbContext.Lexikons
            .Where(l => l.LexikonPackId == ownerId)
            .Include(l => l.ExampleSentences)
            .Include(l => l.Synonyms)
            .Include(l => l.Antonyms)
            .ToListAsync(cancellationToken);

        foreach (var item in items) yield return item;
    }

    public Task AddRangeAsync(IEnumerable<Lexikon> lexikons, CancellationToken cancellationToken = default)
    {
        return dbContext.Lexikons.AddRangeAsync(lexikons, cancellationToken);
    }

    async IAsyncEnumerable<Lexikon> ILexikonRepository<Lexikon>.GetAllAsync(CancellationToken cancellationToken)
    {
        var items = await dbContext.Lexikons.ToListAsync(cancellationToken);
        foreach (var item in items) yield return item;
    }

    public async Task AddAsync(Lexikon lexikon, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(lexikon, cancellationToken);
    }

    public async Task<Lexikon?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Lexikons.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}







internal sealed class PersianLexikonRepository(WordWiseDbContext dbContext) : ILexikonRepository<PersianLexikon>
{
    public async Task<PersianLexikon?> GetBySpecificationAsync(Specification<Lexikon> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.PersianLexikons
            .Include(l => l.ExampleSentences)
            .Include(l => l.Synonyms)
            .Include(l => l.Antonyms)
            .FirstOrDefaultAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async Task<bool> ExistBySpecificationAsync(Specification<Lexikon> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.PersianLexikons.AnyAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async IAsyncEnumerable<PersianLexikon> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        var items = await dbContext.PersianLexikons
            .Where(l => l.LexikonPackId == ownerId)
            .Include(l => l.ExampleSentences)
            .Include(l => l.Synonyms)
            .Include(l => l.Antonyms)
            .ToListAsync(cancellationToken);

        foreach (var item in items) yield return item;
    }

    public Task AddRangeAsync(IEnumerable<PersianLexikon> lexikons, CancellationToken cancellationToken = default)
    {
        return dbContext.PersianLexikons.AddRangeAsync(lexikons, cancellationToken);
    }

    async IAsyncEnumerable<PersianLexikon> ILexikonRepository<PersianLexikon>.GetAllAsync(CancellationToken cancellationToken)
    {
        var items = await dbContext.PersianLexikons.ToListAsync(cancellationToken);
        foreach (var item in items) yield return item;
    }

    public async Task AddAsync(PersianLexikon lexikon, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(lexikon, cancellationToken);
    }

    public async Task<PersianLexikon?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.PersianLexikons.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}





internal sealed class EnglishLexikonRepository(WordWiseDbContext dbContext) : ILexikonRepository<EnglishLexikon>
{
    public async Task<EnglishLexikon?> GetBySpecificationAsync(Specification<Lexikon> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.EnglishLexikons
            .Include(l => l.ExampleSentences)
            .Include(l => l.Synonyms)
            .Include(l => l.Antonyms)
            .FirstOrDefaultAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async Task<bool> ExistBySpecificationAsync(Specification<Lexikon> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.EnglishLexikons.AnyAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async IAsyncEnumerable<EnglishLexikon> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        var items = await dbContext.EnglishLexikons
            .Where(l => l.LexikonPackId == ownerId)
            .Include(l => l.ExampleSentences)
            .Include(l => l.Synonyms)
            .Include(l => l.Antonyms)
            .ToListAsync(cancellationToken);

        foreach (var item in items) yield return item;
    }

    public Task AddRangeAsync(IEnumerable<EnglishLexikon> lexikons, CancellationToken cancellationToken = default)
    {
        return dbContext.EnglishLexikons.AddRangeAsync(lexikons, cancellationToken);
    }

    async IAsyncEnumerable<EnglishLexikon> ILexikonRepository<EnglishLexikon>.GetAllAsync(CancellationToken cancellationToken)
    {
        var items = await dbContext.EnglishLexikons.ToListAsync(cancellationToken);
        foreach (var item in items) yield return item;
    }

    public async Task AddAsync(EnglishLexikon lexikon, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(lexikon, cancellationToken);
    }

    public async Task<EnglishLexikon?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.EnglishLexikons.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}






internal sealed class LexikonPackRepository(WordWiseDbContext dbContext) : ILexikonPackRepository
{
    public async Task<LexikonPack?> GetBySpecificationAsync(Specification<LexikonPack> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.LexikonPacks.FirstOrDefaultAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async Task<bool> ExistBySpecificationAsync(Specification<LexikonPack> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.LexikonPacks.AnyAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async Task<List<LexikonPack>> GetAllByLoadingGraphAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.LexikonPacks
            .Include(p => p.Language)
            .Include(p => p.Lexikons)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public Task AddRangeAsync(IEnumerable<LexikonPack> packs, CancellationToken cancellationToken = default)
    {
        return dbContext.LexikonPacks.AddRangeAsync(packs, cancellationToken);
    }


    public Task<bool> ExistsBySpecificationAsync(Specification<LexikonPack> specification, CancellationToken cancellationToken = default)
    {
        return dbContext.LexikonPacks.AnyAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async Task<IEnumerable<LexikonPack>> GetAllAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.LexikonPacks.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<LexikonPack>> GetAllAsync<T>(CancellationToken cancellationToken = default)
    {
        return await dbContext.LexikonPacks
            .Include(i => i.Lexikons)
            .ToListAsync(cancellationToken);
    }

    async Task<IEnumerable<LexikonPack>> ILexikonPackRepository.GetAllByLoadingGraphAsync(CancellationToken cancellationToken)
    {
        return await dbContext.LexikonPacks
            .Include(i => i.Lexikons)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LexikonPack lpack, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(lpack, cancellationToken);
    }

    public async Task<LexikonPack?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.LexikonPacks.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}


internal sealed class LanguageRepository(WordWiseDbContext dbContext) : ILanguageRepository
{
    public async IAsyncEnumerable<Language> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await dbContext.Languages.ToListAsync(cancellationToken);
        foreach (var item in items) yield return item;
    }

    public async IAsyncEnumerable<TResult> GetAllAsync<TResult>(CancellationToken cancellationToken = default)
    {
        var items = await dbContext.Languages.ToListAsync(cancellationToken);
        foreach (var item in items) yield return (TResult)(object)item;
    }

    public Task AddRangeAsync(IEnumerable<Language> languages, CancellationToken cancellationToken = default)
    {
        return dbContext.Languages.AddRangeAsync(languages, cancellationToken);
    }

    public async Task<Language?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Languages.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<Language?> GetBySpecificationAsync(Specification<Language> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.Languages.FirstOrDefaultAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async Task<bool> ExistsAsync(Specification<Language> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.Languages.AnyAsync(i => specification.IsSatisfiedBy(i), cancellationToken);
    }

    public async Task<IEnumerable<Language>> GetAllAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Languages.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Language language, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(language, cancellationToken);
    }
}


internal sealed class CredentialRepository(WordWiseDbContext dbContext) : ICredentialRepository
{
    public async Task AddAsync(Credential credential, CancellationToken cancellationToken = default)
    {
        await dbContext.Credentials.AddAsync(credential, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<Credential> credentials, CancellationToken cancellationToken = default)
    {
        await dbContext.Credentials.AddRangeAsync(credentials, cancellationToken);
    }

    public async Task<IEnumerable<Credential>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Credentials.ToListAsync(cancellationToken);
    }

    public async Task<Credential?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Credentials.FirstOrDefaultAsync(i =>  i.Id == id, cancellationToken);
    }

    public async Task<Credential?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Credentials.FirstOrDefaultAsync(i => i.UserId == id, cancellationToken);
    }
}





internal sealed class AdministratorRepository(WordWiseDbContext dbContext) : IAdministratorRepository
{
    public async Task AddAsync(Administrator administrator, CancellationToken cancellationToken = default)
    {
        await dbContext.Administrators.AddAsync(administrator, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<Administrator> administrators, CancellationToken cancellationToken = default)
    {
        await dbContext.Administrators.AddRangeAsync(administrators, cancellationToken);
    }

    public Task<IEnumerable<Administrator>> GetAllAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Administrator?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Administrator> GetGraphAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Administrator> GetGraphAsync(Phone phone, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}