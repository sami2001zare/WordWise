using WordWise.Application.Authentication;
using WordWise.Application.Clock;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User;
using WordWise.Core.User.Repositpry;
using WordWise.Core.User.ValueObjects;
using WordWise.Framework;
using WordWise.Framework.Repository;
using System.Text.RegularExpressions;

namespace WordWise.Application.App.User.Student.LoginWithOTP;

internal sealed class LoginCustomerWithOTPCommandHandler(
    IStudentRepository _studentRepository,
    IOtpService _otpService,
    IDateTimeProvider _dateTimeProvider,
    IOneTimePasswordRepository _otpRepository,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<LoginCustomerWithOTPCommand>
{

    public const string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    //09123456789, +989123456789, 00989123456789, 9123456789
    public const string PersianPhoneRegex = @"^(\+98|0098|98|0)?9\d{9}$";

    public async Task<Result> Handle(LoginCustomerWithOTPCommand request, CancellationToken cancellationToken)
    {
        Core.User.Student.Student? customer = await _studentRepository.GetByPhoneAsync(new Phone(request.EmailOrPhone), cancellationToken);

        if (customer == null)
        {
            return Result.Failure(new Error("No Customer", "There Is No Customer Founded"));
        }

        DateTime dateTime = _dateTimeProvider.UtcNow;

        OneTimePassword otp = OneTimePassword.Create(Guid.CreateVersion7(),
            _otpService.Generate(),
            request.EmailOrPhone,
            dateTime,
            dateTime.AddMinutes(5));

        await _otpRepository.AddAsync(otp, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }


    public static string GetInputType(string input)
    {
        if (Regex.IsMatch(input, EmailRegex))
            return "Email";

        if (Regex.IsMatch(input, PersianPhoneRegex))
            return "Phone";

        return "Unknown";
    }
}
