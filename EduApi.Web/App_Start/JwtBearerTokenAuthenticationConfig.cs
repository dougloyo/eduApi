using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Web;
using EduApi.Web.Providers;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace EduApi.Web.App_Start
{
    public class JwtBearerTokenAuthenticationConfig
    {
        private readonly IApplicationSettingsProvider _applicationSettingsProvider;

        public JwtBearerTokenAuthenticationConfig(IApplicationSettingsProvider applicationSettingsProvider)
        {
            _applicationSettingsProvider = applicationSettingsProvider;
        }

        public void Config(IAppBuilder app)
        {
            var issuer = _applicationSettingsProvider.GetValue("JWT.Issuer");
            var audience = _applicationSettingsProvider.GetValue("JWT.Audience");
            var key = Encoding.UTF8.GetBytes(_applicationSettingsProvider.GetValue("JWT.HmacSecretKey"));

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AllowedAudiences = new[] { audience },
                IssuerSecurityTokenProviders = new[] { new SymmetricKeyIssuerSecurityTokenProvider(issuer, key) },
                //TokenValidationParameters = new TokenValidationParameters
                //{
                    
                //}
            });

        }
    }
}