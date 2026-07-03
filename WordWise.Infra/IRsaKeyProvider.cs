using Microsoft.IdentityModel.Tokens;

namespace WordWise.Infra;

public interface IRsaKeyProvider
{
    RsaSecurityKey GetPrivateKey();
    RsaSecurityKey GetPublicKey();
}