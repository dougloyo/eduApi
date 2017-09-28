using System.IdentityModel.Tokens;

namespace EduApi.Web.Security.JWT
{
    public interface ISigningCredentialsProvider
    {
        SigningCredentials GetSigningCredentials();
    }
}