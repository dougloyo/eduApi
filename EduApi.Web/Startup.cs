using System.Web.Http;
using EduApi.Web;
using EduApi.Web.Providers;
using EduApi.Web.SimpleInjector;
using Microsoft.Owin;
using Owin;
using SimpleInjector.Integration.WebApi;

[assembly: OwinStartup(typeof(Startup))]
namespace EduApi.Web
{    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(appBuilder, httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);

            SimpleInjectorWebApiInitializer.Initialize(httpConfiguration);
            SwaggerConfig.Register(httpConfiguration);
        }
    }
}