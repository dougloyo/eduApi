using System.Linq;
using System.Reflection;
using EduApi.Web.Data;
using EduApi.Web.Providers;
using EduApi.Web.Security;
using EduApi.Web.Security.JWT;
using Microsoft.Owin.Security.OAuth;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace EduApi.Web.App_Start
{
    /// <summary>
    /// Inversion of Control Configuration class.
    /// </summary>
    public class InversionOfControlConfig
    {
        /// <summary>
        /// Creates a configured Simple Injector IoC Container.
        /// </summary>
        public Container GetInitializedContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            RegisterDependencies(container);

            return container;
        }

        
        private static void RegisterDependencies(Container container)
        {
            // Register services by convention:
            // I.E.: container.Register<IPeopleService, PeopleService>();
            RegisterServicesByConvention(container);

            // Register Database dependencies.
            container.Register<DatabaseContext, DatabaseContext>(Lifestyle.Scoped);

            // Register Providers.
            container.Register<IApplicationSettingsProvider, ApplicationSettingsProvider>();

            container.Register<JwtAuthorizationServerConfig>();
            container.Register<JwtBearerTokenAuthenticationConfig>();

            container.Register<IClaimsIdentityProvider, DefaultClaimsIdentityProvider>();
            container.Register<IClaimsProvider, DefaultClaimsProvider>();
            container.Register<ISigningCredentialsProvider, HMACSHA256SigningCredentialsProvider>();
            container.Register<ITokenFormatProvider, JwtTokenFormatProvider>();
            container.Register<IOAuthAuthorizationServerProvider, DefaultOAuthAuthorizationServerProvider>();
        }

        private static void RegisterServicesByConvention(Container container)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            var servicesToRegister = (
                from interfaceType in types.Where(t => t.Name.StartsWith("I") && t.Name.EndsWith("Service"))
                from serviceType in types.Where(t => t.Name == interfaceType.Name.Substring(1) && t.Namespace == interfaceType.Namespace)
                select new
                {
                    InterfaceType = interfaceType,
                    ServiceType = serviceType
                }
            );

            foreach (var pair in servicesToRegister)
                container.Register(pair.InterfaceType, pair.ServiceType);
        }
    }
}