using WordWise.Application.Authentication;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User;
using WordWise.Core.User.Repositpry;
using WordWise.Framework;
using WordWise.Framework.Repository;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace WordWise.Application.App.User.Manager.Login;

internal sealed class LoginManagerCommandHandler(
    IAdministratorRepository _studentRepository,
    IJsonWebTokenRepository jwtRepository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork _unitOfWork,
    IJwtService _jwtService,
    IPasswordHasher passwordHasher) : ICortexCommandHandler<LoginManagerCommand, AccessToken>
{
    public async Task<Result<AccessToken>> Handle(LoginManagerCommand request, CancellationToken cancellationToken)
    {

        Administrator? customer = await _studentRepository.GetGraphAsync(request.Phone, cancellationToken);

        if (customer.Credential!.Hash != passwordHasher.Hash(request.Password, Convert.FromBase64String(customer.Credential!.Salt)))
        {
            return Result.Failure<AccessToken>(new Error("", ""));
        }

        AccessToken token = await _jwtService.GetAccessTokenWithMetadataAsync(customer, cancellationToken);

        JsonWebToken jwt = JsonWebToken.Create(token.Token, token.Expiration, "Login", httpContextAccessor.HttpContext.Request.Headers.UserAgent, "IP Address", customer.Id);

        await jwtRepository.AddAsync(jwt, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return token;
    }
}
