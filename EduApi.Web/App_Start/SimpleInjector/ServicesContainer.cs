using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace EduApi.Web.SimpleInjector
{
    public class ServicesContainer
    {
        /// <summary>Registers service dependancies</summary>
        public static void Register (Container container)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            var servicesToRegister = (
                from interfaceType in types.Where(t => t.Name.StartsWith("I") && t.Name.EndsWith("Service"))
                from serviceType in
                types.Where(t => t.Name == interfaceType.Name.Substring(1) && t.Namespace == interfaceType.Namespace)
                select new
                {
                    InterfaceType = interfaceType,
                    ServiceType = serviceType
                }
            );
            foreach (var pair in servicesToRegister)
            {
                container.Register(pair.InterfaceType, pair.ServiceType);
            }
        }
    }
}