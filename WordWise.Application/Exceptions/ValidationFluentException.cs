namespace WordWise.Application.Exceptions;

public sealed class ValidationFluentException(IEnumerable<ValidationError> errors) : ApplicationException
{
    public IEnumerable<ValidationError> Errors { get; } = errors;
}
