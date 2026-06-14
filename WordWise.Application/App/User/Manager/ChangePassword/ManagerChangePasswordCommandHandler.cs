using WordWise.Application.Authentication;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;
using System.Security.Cryptography;
using System.Text;

namespace WordWise.Application.App.User.Manager.ChangePassword;

internal sealed class ManagerChangePasswordCommandHandler(
    IAdministratorRepository _studentRepository,
    IUnitOfWork _unitOfWork,
    IPasswordHasher passwordHasher
    ) : ICortexCommandHandler<ManagerChangePasswordCommand, bool>
{
    public async Task<Result<bool>> Handle(ManagerChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var customer = await _studentRepository.GetGraphAsync(request.Id, cancellationToken);

        if (customer == null)
        {
            return Result.Failure<bool>(new Error("", ""));
        }

        try
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            customer.Credential!.SetPasswod(passwordHasher.Hash(request.NewPassword, salt));
            customer.Credential!.SetSalt(Convert.ToBase64String(salt));

            _unitOfWork.Update(customer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return Result.Failure<bool>(new Error("", ""));
        }
    }
}