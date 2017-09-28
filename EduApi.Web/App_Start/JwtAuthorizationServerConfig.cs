using System;
using EduApi.Web.Providers;
using EduApi.Web.Security;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace EduApi.Web.App_Start
{
    public class JwtAuthorizationServerConfig
    {
        private readonly IApplicationSettingsProvider _applicationSettingsProvider;
        private readonly ITokenFormatProvider _tokenFormatProvider;
        private readonly OAuthAuthorizationServerProvider _oAuthAuthorizationServerProvider;

        public JwtAuthorizationServerConfig(IApplicationSettingsProvider applicationSettingsProvider, 
                                            ITokenFormatProvider tokenFormatProvider,
                                            OAuthAuthorizationServerProvider oAuthAuthorizationServerProvider)
        {
            _applicationSettingsProvider = applicationSettingsProvider;
            _tokenFormatProvider = tokenFormatProvider;
            _oAuthAuthorizationServerProvider = oAuthAuthorizationServerProvider;
        }

        public void Configure(IAppBuilder app)
        {
            var expiresInMinutes = Convert.ToInt32(_applicationSettingsProvider.GetValue("JWT.Expires"));

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/jwt/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(expiresInMinutes),
                AccessTokenFormat = _tokenFormatProvider,
                Provider = _oAuthAuthorizationServerProvider,
#if DEBUG
                AllowInsecureHttp = true
#endif

            });
        }
    }
}