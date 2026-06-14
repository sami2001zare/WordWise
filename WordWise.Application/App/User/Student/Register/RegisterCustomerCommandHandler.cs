using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using WordWise.Application.Authentication;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.User.Student.Register;

internal sealed class RegisterCustomerCommandHandler(
    IStudentRepository _userRepository,
    ICredentialRepository _credentialRepository,
    IJsonWebTokenRepository _jwtRepository,
    IHttpContextAccessor httpContextAccessor,
    IJwtService _jwtService,
    IPasswordHasher _passwordHasher,
    IUnitOfWork _unitOfWork) : ICortexCommandHandler<RegisterCustomerCommand, AccessToken>
{
    public async Task<Result<AccessToken>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        //Validate email uniqueness
        //if (await _userRepository.GetByEmailAsync(request.Email, cancellationToken) is not null)
        //    return Result.Failure<Guid>(new Error("", ""));

        if (await _userRepository.GetByPhoneAsync(request.Phone, cancellationToken) is not null)
            return Result.Failure<AccessToken>(new Error("", ""));

        // Create user aggregate
        var user = Core.User.Student.Student.Register(Guid.CreateVersion7(), request.FirstName, request.LastName, request.Phone);

        byte[] salt = RandomNumberGenerator.GetBytes(16);

        string password = _passwordHasher.Hash(request.Password, salt);

        Credential credential = Credential.Create(password, Convert.ToBase64String(salt), user.Id);

        AccessToken token = await _jwtService.GetAccessTokenWithMetadataAsync(user, cancellationToken);

        JsonWebToken jwt = JsonWebToken.Create(token.Token, token.Expiration, "Login", httpContextAccessor.HttpContext.Request.Headers.UserAgent, "IP Address", user.Id);

        await _jwtRepository.AddAsync(jwt, cancellationToken);

        await _userRepository.AddAsync(user, cancellationToken);
        await _credentialRepository.AddAsync(credential, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return token;
    }
}
