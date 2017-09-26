using Owin;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace EduApi.Web.App_Start.ExtensionMethods
{
    public static class AppBuilderExtensions
    {
        public static void UseOwinContextInjector(this IAppBuilder app, Container container)
        {
            // Create an OWIN middleware to create an execution context scope
            app.Use(async (context, next) =>
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    await next();
                }
            });
        }
    }
}