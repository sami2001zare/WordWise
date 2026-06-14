using WordWise.Application.Authentication;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;
using System.Security.Cryptography;

namespace WordWise.Application.App.User.Student.ChangePassword;

internal sealed class CustomerChangePasswordCommandHandler(
    IStudentRepository _studentRepository,
    ICredentialRepository _credentialRepository,
    IUnitOfWork _unitOfWork,
    IPasswordHasher passwordHasher
    ) : ICortexCommandHandler<CustomerChangePasswordCommand, bool>
{
    public async Task<Result<bool>> Handle(CustomerChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var customer = await _studentRepository.GetByPhoneAsync(request.Phone, cancellationToken);

        if (customer == null)
        {
            return Result.Failure<bool>(new Error("", ""));
        }

        var credential = await _credentialRepository.GetByUserIdAsync(customer.Id, cancellationToken);

        if (credential == null)
        {
            return Result.Failure<bool>(new Error("", ""));
        }

        try
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            credential.SetPasswod(passwordHasher.Hash(request.NewPassword, salt));
            credential.SetSalt(Convert.ToBase64String(salt));

            _unitOfWork.Update(credential);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return Result.Failure<bool>(new Error("", ""));
        }
    }
}