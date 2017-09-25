using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.IdentityModel.Tokens;

namespace EduApi.Web.Providers.JWTSigningCredentialsProviders
{
    public class HMACSHA256SigningCredentialsProvider : ISigningCredentialsProvider
    {
        private IApplicationSettingsProvider _appSettings;
        public HMACSHA256SigningCredentialsProvider(IApplicationSettingsProvider appSettings)
        {
            _appSettings = appSettings;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var hmacKey = _appSettings.GetValue("JWT.HmacSecretKey");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(hmacKey));
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

    }
}