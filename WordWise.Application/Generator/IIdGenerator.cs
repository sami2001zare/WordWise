namespace WordWise.Application.Generator;

public interface IIdGenerator
{
    Task<string> GenerateSerial();
    Task<string> GenerateRandomPassword();
}
