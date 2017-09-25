using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace EduApi.Web.SimpleInjector
{
    public class BaseContainer
    {
        /// <summary>registers dependancies based on naming conventions</summary>
        public static void Register (Container container, string endsWith)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            var typesToRegister = (
                from serviceType in types.Where(t => t.Name.StartsWith("I") && t.Name.EndsWith(endsWith))
                from implementationType in
                types.Where(t => t.Name == serviceType.Name.Substring(1) && t.Namespace == serviceType.Namespace)
                select new
                {
                    ServiceType = serviceType,
                    ImplementationType = implementationType
                }
            );
            foreach (var pair in typesToRegister)
            {
                // Checks if the service has already been registered. Allows for custom installations
                if(container.GetCurrentRegistrations().All(x => x.ServiceType != pair.ServiceType))
                    container.Register(pair.ServiceType, pair.ImplementationType, Lifestyle.Scoped);
            }
        }
    }
}