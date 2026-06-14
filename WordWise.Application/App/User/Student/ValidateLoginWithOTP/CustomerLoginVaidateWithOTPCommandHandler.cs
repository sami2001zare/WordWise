using WordWise.Application.Authentication;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;
using Microsoft.AspNetCore.Http;

namespace WordWise.Application.App.User.Student.ValidateLoginWithOTP;

internal sealed class CustomerLoginVaidateWithOTPCommandHandler(
    IOneTimePasswordRepository _otpRepository,
    IStudentRepository _studentRepository,
    IJsonWebTokenRepository jwtRepository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork _unitOfWork,
    IJwtService _jwtService
    ) : ICortexCommandHandler<CustomerLoginVaidateWithOTPCommand, string>
{
    public async Task<Result<string>> Handle(CustomerLoginVaidateWithOTPCommand request, CancellationToken cancellationToken)
    {
        OneTimePassword? oneTime = await _otpRepository.GetLatestByPhoneAsync(request.Phone, cancellationToken);

        if (oneTime is null)
        {
            return Result.Failure<string>(new Error("", ""));
        }

        else
        {
            var customer = await _studentRepository.GetByPhoneAsync(request.Phone, cancellationToken);

            if (customer is null)
            {
                return Result.Failure<string>(new Error("", ""));
            }

            else
            {
                customer.VerifyPhone();
                _unitOfWork.Update(customer);

                AccessToken token = await _jwtService.GetAccessTokenWithMetadataAsync(customer, cancellationToken);

                JsonWebToken jwt = JsonWebToken.Create(token.Token, token.Expiration, "Login", httpContextAccessor.HttpContext.Request.Headers.UserAgent, "IP Address", customer.Id);

                await jwtRepository.AddAsync(jwt, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return token.Token;
            }
        }
    }
}
