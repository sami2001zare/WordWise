using FluentAssertions;
using WordWise.Core.User.Events;
using WordWise.Core.User.Student;
using WordWise.Core.User.ValueObjects;

namespace WordWise.Test;

public class StudentTests
{
    [Fact]
    public void SaveVocabulary_ShouldRaiseLearningMilestoneReachedEvent_When100WordsSaved()
    {
        // Arrange
        var studentId = Guid.CreateVersion7();
        var student = Student.Register(studentId, new FirstName("John"), new LastName("Doe"), new Phone("+1234567890"));
        student.ClearDomainEvents(); // Clear registration events

        // Act
        for (int i = 0; i < 100; i++)
        {
            var vocab = SavedVocabulary.Create(Guid.NewGuid(), studentId, Guid.NewGuid(), DateTime.UtcNow);
            student.SaveVocabulary(vocab);
        }

        // Assert
        var events = student.GetDomainEvents();
        events.Should().ContainSingle(e => e is LearningMilestoneReachedEvent);
        var milestoneEvent = (LearningMilestoneReachedEvent)events.First(e => e is LearningMilestoneReachedEvent);
        milestoneEvent.VocabularyCount.Should().Be(100);
    }
}

