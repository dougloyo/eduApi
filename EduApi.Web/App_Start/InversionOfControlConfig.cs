using EduApi.Web.Data;
using EduApi.Web.Services;
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
            container.Register<IStudentsService, StudentsService>();
            container.Register<DatabaseContext, DatabaseContext>(Lifestyle.Scoped);
        }
    }
}