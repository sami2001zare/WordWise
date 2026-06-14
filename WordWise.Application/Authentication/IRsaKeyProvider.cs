using Microsoft.IdentityModel.Tokens;

namespace WordWise.Application.Authentication;

public interface IRsaKeyProvider
{
    RsaSecurityKey GetPrivateKey();
    RsaSecurityKey GetPublicKey();
}