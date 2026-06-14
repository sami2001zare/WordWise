namespace WordWise.Application.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
