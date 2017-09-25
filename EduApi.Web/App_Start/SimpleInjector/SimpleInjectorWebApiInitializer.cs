using System;
using System.Configuration;
using System.Web.Http;
using EduApi.Web.Data;
using EduApi.Web.Providers;
using EduApi.Web.Providers.JWTSigningCredentialsProviders;
using EduApi.Web.Services;
using EduApi.Web.SimpleInjector;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace EduApi.Web.SimpleInjector
{
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize(HttpConfiguration httpConfiguration)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            InitializeContainer(container);

            // Installs based on convention i.e. IUserService, UserService
            BaseContainer.Register(container, "Provider");
            BaseContainer.Register(container, "Service");

            container.RegisterWebApiControllers(httpConfiguration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        //<summary>Custom Installations not overridden by the base installation</summary>
        private static void InitializeContainer(Container container)
        {
            var useRsa = ConfigurationManager.AppSettings["JWT.UseRsa"];
            if(useRsa == "true") container.Register<ISigningCredentialsProvider, RSASigningCredentialsProvider>(Lifestyle.Scoped);
            else container.Register<ISigningCredentialsProvider, HMACSHA256SigningCredentialsProvider>(Lifestyle.Scoped);

            container.Register<DatabaseContext, DatabaseContext>(Lifestyle.Scoped);

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }

    }
}