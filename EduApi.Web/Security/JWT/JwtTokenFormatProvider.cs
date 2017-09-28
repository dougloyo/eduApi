using System;
using System.IdentityModel.Tokens;
using EduApi.Web.Providers;
using Microsoft.Owin.Security;

namespace EduApi.Web.Security.JWT
{
    public class JwtTokenFormatProvider : ITokenFormatProvider
    {
        private readonly IApplicationSettingsProvider _appSettings;
        private readonly ISigningCredentialsProvider _signingCredentialsProvider;

        public JwtTokenFormatProvider(IApplicationSettingsProvider appSettings, ISigningCredentialsProvider signingCredentialsProvider)
        {
            _appSettings = appSettings;
            _signingCredentialsProvider = signingCredentialsProvider;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException("data");

            var issuer = _appSettings.GetValue("JWT.Issuer");
            var audience = _appSettings.GetValue("JWT.Audience");
            var validFrom = DateTime.UtcNow;
            var expires = validFrom.AddMinutes(Convert.ToInt32(_appSettings.GetValue("JWT.Expires")));

            var signingCredentials = _signingCredentialsProvider.GetSigningCredentials();

            var token = new JwtSecurityToken(issuer, audience, data.Identity.Claims, validFrom, expires, signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}