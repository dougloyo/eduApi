using Microsoft.IdentityModel.Tokens;


namespace EduApi.Web.Providers
{
    public interface ISigningCredentialsProvider
    {
        SigningCredentials GetSigningCredentials();
    }
}