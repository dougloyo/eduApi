using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Microsoft.IdentityModel.Tokens;

namespace EduApi.Web.Providers.JWTSigningCredentialsProviders
{
    public class RSASigningCredentialsProvider : ISigningCredentialsProvider
    {
        private IApplicationSettingsProvider _appSettings;
        private readonly IRSAProvider _rsaProvider;
        public RSASigningCredentialsProvider(IApplicationSettingsProvider appSettings, IRSAProvider rsaProvider)
        {
            _appSettings = appSettings;
            _rsaProvider = rsaProvider;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = GetSecurityKey();
            return new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
        }
        private RsaSecurityKey GetSecurityKey()
        {
            Directory.GetCurrentDirectory();
            var rsaJsonFileName = _appSettings.GetValue("JWT.RsaJsonFileName");
            var keyParameters = _rsaProvider.GetKeyParameters(rsaJsonFileName);

            const int dwKeySize = 2048;
            var provider = new RSACryptoServiceProvider(dwKeySize);
            provider.ImportParameters(keyParameters);

            var key = new RsaSecurityKey(provider);
            return key;
        }
    }

}