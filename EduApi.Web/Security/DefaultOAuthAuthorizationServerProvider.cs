using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace EduApi.Web.Security
{
    /// <summary>
    /// The Default imlementation of the OAuth Authorizaton Server Provider
    /// </summary>
    public class DefaultOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider, IOAuthAuthorizationServerProvider
    {
        private readonly IClaimsIdentityProvider _claimsIdentityProvider;

        public DefaultOAuthAuthorizationServerProvider(IClaimsIdentityProvider claimsIdentityProvider)
        {
            _claimsIdentityProvider = claimsIdentityProvider;
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // TODO: Go to database to verify.
            if (context.UserName != context.Password)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                return Task.FromResult(0);
            }

            // If still here we have validated the credentials. Lets proceed to issue claims.
            var userId = "55";

            var identity = _claimsIdentityProvider.GetClaimsIdentity(userId);
            context.Validated(identity);
            return Task.FromResult(0);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // TODO: Need Database Validation of Audience and ClientId based on the following article:
            // http://bitoftech.net/2014/10/27/json-web-token-asp-net-web-api-2-jwt-owin-authorization-server/

            //if (context.ClientId == null)
            //{
            //    context.SetError("invalid_clientId", "client_Id is not set");
            //    return Task.FromResult(0);
            //}

            context.Validated();

            return Task.FromResult(0);
        }
    }
}