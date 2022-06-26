using ArmyPlanner.Extensions;
using ArmyPlanner.Test.Base.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ArmyPlanner.Test
{
    internal static class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static async Task Init()
        {
            List<Type> serviceTypes = new List<Type>();

            IServiceCollection serviceCollection = new ServiceCollection()
            .AddArmyPlanner()
            .AddTestBase()
            ;

            foreach (ServiceDescriptor service in serviceCollection)
            {
                serviceTypes.Add(service.ServiceType);
            }

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            ServiceProvider = serviceProvider;
        }
    }
}
