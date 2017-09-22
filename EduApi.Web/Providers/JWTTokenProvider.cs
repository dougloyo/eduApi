using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;


namespace EduApi.Web.Providers
{
    public interface ISecurityTokenProvider
    {
        ClaimsPrincipal GetClaimsPrincipal(string tokenString);
        string GetToken(IEnumerable<Claim> claims, int expirationMinutes = 1);
    }

    public class TokenAuthOptions
    {
        public string Audience { get; set; }
        public List<string> Issuers { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }


    public class JWTTokenProvider : ISecurityTokenProvider
    {
        private readonly IApplicationSettingsProvider _appSettings;
        private readonly IRSAProvider _rsaProvider;
        private readonly TokenAuthOptions _tokenAuthOptions;

        public JWTTokenProvider(TokenAuthOptions tokenAuthOptions, IApplicationSettingsProvider appSettings, IRSAProvider rsaProvider)
        {
            _tokenAuthOptions = tokenAuthOptions;
            _appSettings = appSettings;
            _rsaProvider = rsaProvider;
        }

        public ClaimsPrincipal GetClaimsPrincipal(string tokenString)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidIssuers = _tokenAuthOptions.Issuers,
                ValidAudience = _tokenAuthOptions.Audience,
                IssuerSigningKey = _tokenAuthOptions.SigningCredentials.Key
            };

            var handler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = handler.ValidateToken(tokenString, validationParameters, out securityToken);

            return principal;
        }

        public string GetToken(IEnumerable<Claim> claims, int expirationMinutes = 1)
        {
            var identity = new ClaimsIdentity(claims);

            var appliesToAddress = _appSettings.GetValue("JWT.Audience");
            var issuer = _appSettings.GetValue("JWT.Issuer");
            var expires = DateTime.Now.AddMinutes(expirationMinutes);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = appliesToAddress,
                SigningCredentials = GetSigningCredentials(),
                Subject = identity,
                Expires = expires,
            };

            var handler = new JwtSecurityTokenHandler()
            {
                TokenLifetimeInMinutes = expirationMinutes,
                SetDefaultTimesOnTokenCreation = false
            };
            var securityToken = handler.CreateToken(securityTokenDescriptor);

            return handler.WriteToken(securityToken);
        }

        private SigningCredentials GetSigningCredentials()
        {
            // Load the key we are going to use.
            var key = GetRsaSecurityKey();
            return new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
        }

        private RsaSecurityKey GetRsaSecurityKey()
        {
            Directory.GetCurrentDirectory();
            RSAParameters keyParameters = _rsaProvider.GetKeyParameters("rsaKey.json");

            const int dwKeySize = 2048;
            var provider = new RSACryptoServiceProvider(dwKeySize);
            provider.ImportParameters(keyParameters);

            var key = new RsaSecurityKey(provider);
            return key;
        }
    }

}