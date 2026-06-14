using WordWise.Application.Authentication;
using WordWise.Application.Generator;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;
using System.Security.Cryptography;

namespace WordWise.Application.App.User.Student.ForgotPassword;

internal sealed class ManagerForgotPasswordCommandHandler(
    IStudentRepository _studentRepository,
    ICredentialRepository _credentialRepository,
    IUnitOfWork _unitOfWork,
    IIdGenerator idGenerator,
    IPasswordHasher passwordHasher,
    ITextMessageService textMessageService
    ) : ICortexCommandHandler<CustomerForgotPasswordCommand, string>
{
    public async Task<Result<string>> Handle(CustomerForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var customer = await _studentRepository.GetByPhoneAsync(request.Phone, cancellationToken);

        if (customer == null)
        {
            return Result.Failure<string>(new Error("", ""));
        }

        var credential = await _credentialRepository.GetByUserIdAsync(customer.Id, cancellationToken);

        if (credential == null)
        {
            return Result.Failure<string>(new Error("", ""));
        }

        try
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            string randomPass = await idGenerator.GenerateRandomPassword();
            credential.SetPasswod(passwordHasher.Hash(randomPass, salt));
            credential.SetSalt(Convert.ToBase64String(salt));

            _unitOfWork.Update(credential);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await textMessageService.SendForgotPasswordAsync(request.Phone.Value, randomPass, cancellationToken);

            return Result.Success(randomPass);
        }
        catch (Exception)
        {
            return Result.Failure<string>(new Error("", ""));
        }
    }
}
