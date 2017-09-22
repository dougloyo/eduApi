using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace EduApi.Web.Providers
{
    public interface ISigningCredentialsProvider
    {
        SigningCredentials GetSigningCredentials();
    }

    public class RSASigningCredentialsProvider : ISigningCredentialsProvider
    {
        public SigningCredentials GetSigningCredentials() {
            throw new NotImplementedException();
        }
    }

    public class HMACSHA256SigningCredentialsProvider : ISigningCredentialsProvider
    {
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        public SigningCredentials GetSigningCredentials()
        {
            throw new NotImplementedException();
        }
    }
}