namespace WordWise.Application.Authentication;

public interface IMessageService
{
    Task SendVerificationMessageAsync(string phone, string otp, CancellationToken ct = default);
}