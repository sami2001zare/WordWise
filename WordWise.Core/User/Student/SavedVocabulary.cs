using WordWise.Framework;

namespace WordWise.Core.User.Student;

public sealed class SavedVocabulary : Entity
{
    private SavedVocabulary(Guid id, Guid studentId, Guid lexikonId, DateTime savedAt)
    {
        Id = id;
        StudentId = studentId;
        LexikonId = lexikonId;
        SavedAt = savedAt;
        NextReviewDate = savedAt.AddDays(1); // Default initial spaced repetition gap
        ReviewCount = 0;
    }

    protected SavedVocabulary() { }

    public Guid StudentId { get; private set; }
    public Guid LexikonId { get; private set; }
    public DateTime SavedAt { get; private set; }

    // Spaced repetition fields
    public DateTime NextReviewDate { get; private set; }
    public int ReviewCount { get; private set; }

    public static SavedVocabulary Create(Guid id, Guid studentId, Guid lexikonId, DateTime savedAt)
    {
        return new SavedVocabulary(id, studentId, lexikonId, savedAt);
    }

    public void MarkReviewed(DateTime reviewedAt, bool isRemembered)
    {
        ReviewCount++;

        // Simple spaced repetition logic
        int daysToAdd = isRemembered ? (int)Math.Pow(2, ReviewCount) : 1;
        NextReviewDate = reviewedAt.AddDays(daysToAdd);
    }
}