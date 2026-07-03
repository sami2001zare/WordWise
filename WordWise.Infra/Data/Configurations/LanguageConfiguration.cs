using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordWise.Core.Language;
using WordWise.Core.Lexikon;
using WordWise.Core.Media;
using WordWise.Core.Media.Book;
using WordWise.Core.Media.Film;
using WordWise.Core.Media.Series;
using WordWise.Core.User;
using WordWise.Core.User.Student;
using WordWise.Core.User.ValueObjects;

namespace WordWise.Infra.Data.Configurations;

internal sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("Languages");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Title).HasMaxLength(100).IsRequired();
        builder.Property(l => l.NativeTitle).HasMaxLength(100).IsRequired();
        builder.Property(l => l.Abbrivation).HasMaxLength(10).IsRequired();

        //builder.HasMany(l => l.LexikonPacks)
        //    .WithOne(p => p.Language)
        //    .HasForeignKey(p => p.LanguageId)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}


internal sealed class LexikonConfiguration : IEntityTypeConfiguration<Lexikon>
{
    public void Configure(EntityTypeBuilder<Lexikon> builder)
    {
        builder.ToTable("Lexikons");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Word)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(l => l.PartOfSpeech)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(l => l.DifficultyLevel)
            //.HasConversion(d => d == null ? null : (int?)d.Value, v => v.HasValue ? DifficultyLevel.FromValue(v.Value) : null);
            .HasConversion<string?>();

        builder.Property(l => l.AudioUrl).HasMaxLength(500);
        builder.Property(l => l.ImageUrl).HasMaxLength(500);

        builder.HasOne(l => l.LexikonPack)
            .WithMany(p => p.Lexikons)
            .HasForeignKey(l => l.LexikonPackId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.ExampleSentences)
            .WithOne()
            .HasForeignKey(e => e.LexikonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.Synonyms)
            .WithOne()
            .HasForeignKey(s => s.LexikonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.Antonyms)
            .WithOne()
            .HasForeignKey(a => a.LexikonId)
            .OnDelete(DeleteBehavior.Cascade);

        var navExampleSentences = builder.Metadata.FindNavigation(nameof(Lexikon.ExampleSentences));
        navExampleSentences?.SetPropertyAccessMode(PropertyAccessMode.Field);

        var navSynonyms = builder.Metadata.FindNavigation(nameof(Lexikon.Synonyms));
        navSynonyms?.SetPropertyAccessMode(PropertyAccessMode.Field);

        var navAntonyms = builder.Metadata.FindNavigation(nameof(Lexikon.Antonyms));
        navAntonyms?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}


internal sealed class LexikonPackConfiguration : IEntityTypeConfiguration<LexikonPack>
{
    public void Configure(EntityTypeBuilder<LexikonPack> builder)
    {
        builder.ToTable("LexikonPacks");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).HasMaxLength(200).IsRequired();

        var navLexikons = builder.Metadata.FindNavigation(nameof(LexikonPack.Lexikons));
        navLexikons?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}


internal sealed class MediaBaseConfiguration : IEntityTypeConfiguration<MediaBase>
{
    public void Configure(EntityTypeBuilder<MediaBase> builder)
    {
        builder.ToTable("Medias");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title).HasMaxLength(200).IsRequired();
        builder.Property(m => m.Thumbnail).HasMaxLength(500);
        builder.Property(m => m.ContentUrl).HasMaxLength(500);

        builder.HasOne(m => m.Language)
            .WithMany()
            .HasForeignKey(m => m.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Subtitles)
            .WithOne()
            .HasForeignKey(s => s.MediaBaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.UseTphMappingStrategy()
            .HasDiscriminator<string>("MediaType")
            .HasValue<Film>("Film")
            .HasValue<Series>("Series")
            .HasValue<Book>("Book");
    }
}

internal sealed class FilmConfiguration : IEntityTypeConfiguration<Film>
{
    public void Configure(EntityTypeBuilder<Film> builder)
    {
        builder.HasBaseType<MediaBase>();
        builder.Property(f => f.Genere)
            //.HasConversion(g => g.Name, v => Genere.FromName(v, true))
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}

internal sealed class SeriesConfiguration : IEntityTypeConfiguration<Series>
{
    public void Configure(EntityTypeBuilder<Series> builder)
    {
        builder.HasBaseType<MediaBase>();
        builder.Property(s => s.Episod).IsRequired();
        builder.Property(s => s.Season).IsRequired();
    }
}

internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasBaseType<MediaBase>();
        builder.Property(b => b.Category)
            .HasConversion<string>()
            .HasMaxLength(50);
        builder.Property(b => b.Author).HasMaxLength(100);
    }
}

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasConversion(fn => fn.Value, v => new FirstName(v))
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasConversion(ln => ln.Value, v => new LastName(v))
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.Phone)
            .HasConversion(p => p.Value, v => new Phone(v))
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(u => u.Credential)
            .WithOne()
            .HasForeignKey<Credential>("UserId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.WebTokens)
            .WithOne()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.UseTphMappingStrategy()
            .HasDiscriminator<string>("UserType")
            .HasValue<Student>("Student")
            .HasValue<Administrator>("Administrator");
    }
}


internal sealed class AdministratorConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.HasBaseType<User>();

        builder.ToTable("Administrators");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
                .HasConversion(p => p.Value, v => new Email(v))
                .HasMaxLength(20)
                .IsRequired();
    }
}


internal sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasBaseType<User>();

        builder.Property(s => s.Email)
            //.HasConversion(e => e == null ? null : e.Value, v => v == null ? null : new Email(v))
            .HasConversion<string?>()
            .HasMaxLength(150);

        builder.Property(s => s.ProficiencyLevel)
            //.HasConversion(p => p == null ? null : p.Name, v => v == null ? null : ProficiencyLevel.FromName(v, true))
            .HasConversion<string?>()
            .HasMaxLength(20);

        builder.Property(u => u.Email)
            .HasConversion(p => p.Value, v => new Email(v))
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(s => s.LearningGoal)
            //.HasConversion(g => g == null ? null : g.Name, v => v == null ? null : LearningGoal.FromName(v, true))
            .HasConversion<string?>()
            .HasMaxLength(50);

        builder.Property(s => s.LearningStyle)
            //.HasConversion(ls => ls == null ? null : ls.Name, v => v == null ? null : LearningStyle.FromName(v, true))
            .HasConversion<string?>()
            .HasMaxLength(50);

        builder.Property(s => s.ContentFocus)
            //.HasConversion(cf => cf == null ? null : cf.Name, v => v == null ? null : ContentFocus.FromName(v, true))
            .HasConversion<string?>()
            .HasMaxLength(50);

        var navigationTakenMedia = builder.Metadata.FindNavigation(nameof(Student.TakenMedias));
        navigationTakenMedia?.SetPropertyAccessMode(PropertyAccessMode.Field);

        var navigationUploaded = builder.Metadata.FindNavigation(nameof(Student.UploadedContents));
        navigationUploaded?.SetPropertyAccessMode(PropertyAccessMode.Field);

        var navigationSavedVocab = builder.Metadata.FindNavigation(nameof(Student.SavedVocabularies));
        navigationSavedVocab?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}