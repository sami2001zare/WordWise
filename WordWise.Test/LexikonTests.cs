using FluentAssertions;
using WordWise.Core.Lexikon;

namespace WordWise.Test;

public class LexikonTests
{
    [Fact]
    public void CreateWithValidation_ShouldFail_WhenWordIsEmpty()
    {
        // Act
        var result = Lexikon.CreateWithValidation(Guid.NewGuid(), "", "Noun", DateTime.UtcNow, Guid.NewGuid(), null);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("Lexikon.EmptyWord");
    }

    [Fact]
    public void CreateWithValidation_ShouldSucceed_WhenValid()
    {
        // Act
        var result = Lexikon.CreateWithValidation(Guid.NewGuid(), "Hello", "Noun", DateTime.UtcNow, Guid.NewGuid(), null);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Word.Should().Be("Hello");
    }
}

