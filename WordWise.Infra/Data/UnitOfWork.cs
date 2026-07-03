using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WordWise.Core.Language;
using WordWise.Core.Lexikon;
using WordWise.Core.User;
using WordWise.Core.User.Student;
using WordWise.Framework.Repository;

namespace WordWise.Infra.Data;

internal sealed class UnitOfWork(WordWiseDbContext dbContext) : IUnitOfWork
{
    public EntityEntry Remove(object entity)
    {
        return dbContext.Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    public EntityEntry Update(object entity)
    {
        return dbContext.Update(entity);
    }
}


public class WordWiseDbContext : DbContext
{
    public WordWiseDbContext(DbContextOptions<WordWiseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<LexikonPack> LexikonPacks { get; set; } = null!;
    public DbSet<WordWise.Core.Lexikon.Lexikon> Lexikons { get; set; } = null!;
    public DbSet<ExampleSentence> ExampleSentences { get; set; } = null!;
    public DbSet<Synonym> Synonyms { get; set; } = null!;
    public DbSet<Antonym> Antonyms { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Administrator> Administrators { get; set; } = null!;
    public DbSet<SavedVocabulary> SavedVocabularies { get; set; } = null!;
    
    public DbSet<OneTimePassword> OneTimePasswords { get; set; } = null!;

    // Add additional DbSets for other aggregates like Media (Book, Film, etc.)

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(WordWiseDbContext).Assembly);
    }
}