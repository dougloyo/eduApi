using System;
using System.Web.Http;
using EduApi.Web.Data;
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

            ServicesContainer.Register(container);

            InitializeContainer(container);

            container.RegisterWebApiControllers(httpConfiguration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<DatabaseContext, DatabaseContext>(Lifestyle.Scoped);

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }

    }
}