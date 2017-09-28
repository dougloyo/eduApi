using System.Web.Http;
using EduApi.Web.App_Start;
using EduApi.Web.App_Start.ExtensionMethods;
using Owin;
using SimpleInjector.Integration.WebApi;

namespace EduApi.Web
{
    /// <summary>
    /// The OWIN startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The Configuration method for the app.
        /// </summary>
        /// <param name="app">The I AppBuilder</param>
        public virtual void Configuration(IAppBuilder app)
        {
            // Enable IoC
            var ioCConfig = new InversionOfControlConfig();
            var container = ioCConfig.GetInitializedContainer();

            app.UseOwinContextInjector(container);

            //Configure Authorization Server
            var authorizationServerConfig = container.GetInstance<JwtAuthorizationServerConfig>();
            authorizationServerConfig.Configure(app);

            //Configure Bearer Token Authentication 
            var tokenBearerAuthenticationConfig = container.GetInstance<JwtBearerTokenAuthenticationConfig>();
            tokenBearerAuthenticationConfig.Config(app);

            // Enable WebApi with IoC
            var httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };                        

            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);

            // Enable Swagger
            SwaggerConfig.Register(httpConfiguration);
        }
    }
}