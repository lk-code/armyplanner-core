using ArmyPlanner.Interfaces;
using ArmyPlanner.Test.Base.Services.Http;
using ArmyPlanner.Test.Base.Services.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace ArmyPlanner.Test.Base.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTestBase(this IServiceCollection services)
        {
            services.AddSingleton<IStorageService, TestStorageService>();
            services.AddSingleton<IHttpService, TestHttpService>();

            return services;
        }
    }
}