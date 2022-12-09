using IdentityModel.Client;

namespace AspCoreMVC.Services
{
    public interface ITokenService
    {
         Task<TokenResponse> GetToken(string scope);
    }
}
