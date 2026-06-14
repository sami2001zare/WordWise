using WordWise.Application.Authentication;
using WordWise.Application.Generator;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;
using System.Security.Cryptography;
using System.Text;

namespace WordWise.Application.App.User.Manager.ForgotPassword;

internal sealed class ManagerForgotPasswordCommandHandler(
    IAdministratorRepository _studentRepository,
    ICredentialRepository _credentialRepository,
    IUnitOfWork _unitOfWork,
    IIdGenerator idGenerator,
    IPasswordHasher passwordHasher,
    ITextMessageService textMessageService
    ) : ICortexCommandHandler<ManagerForgotPasswordCommand, string>
{
    public async Task<Result<string>> Handle(ManagerForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var manager = await _studentRepository.GetGraphAsync(request.Id, cancellationToken);

        if (manager == null)
        {
            return Result.Failure<string>(new Error("", ""));
        }

        var credential = await _credentialRepository.GetByUserIdAsync(manager.Id, cancellationToken);

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

            await textMessageService.SendForgotPasswordAsync(manager.Phone.Value, randomPass, cancellationToken);

            return Result.Success(randomPass);
        }
        catch (Exception)
        {
            return Result.Failure<string>(new Error("", ""));
        }
    }
}
