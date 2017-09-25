using System;
using System.Collections.Generic;
using System.Configuration;
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

    public class JWTTokenProvider : ISecurityTokenProvider
    {
        private readonly IApplicationSettingsProvider _appSettings;
        private readonly ISigningCredentialsProvider _signingCredentialsProvider;

        public JWTTokenProvider(IApplicationSettingsProvider appSettings, ISigningCredentialsProvider signingCredentialsProvider)
        {
            _appSettings = appSettings;
            _signingCredentialsProvider = signingCredentialsProvider;
        }

        public ClaimsPrincipal GetClaimsPrincipal(string tokenString)
        {
            var issuer = _appSettings.GetValue("JWT.Issuer");
            var appliesToAddress = _appSettings.GetValue("JWT.Audience");
            var signingCredentials = _signingCredentialsProvider.GetSigningCredentials();

            var validationParameters = new TokenValidationParameters
            {
                ValidIssuers = new [] { issuer },
                ValidAudience = appliesToAddress,
                IssuerSigningKey = signingCredentials.Key
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
                SigningCredentials = _signingCredentialsProvider.GetSigningCredentials(),
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
    }

}