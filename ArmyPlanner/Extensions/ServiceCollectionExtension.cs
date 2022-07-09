using ArmyPlanner.Interfaces;
using ArmyPlanner.Services.Codex;
using ArmyPlanner.Services.Http;
using ArmyPlanner.Services.Repository;
using ArmyPlanner.Services.Roster;
using ArmyPlanner.Services.Storage;
using ArmyPlanner.Services.Translation;
using Microsoft.Extensions.DependencyInjection;

namespace ArmyPlanner.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddArmyPlanner(this IServiceCollection services)
        {
            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<IRepositoryService, RepositoryService>();
            services.AddSingleton<ITranslationService, TranslationService>();
            services.AddSingleton<ICodexService, CodexService>();
            services.AddSingleton<IRosterService, RosterService>();

#if NET5_0_OR_GREATER
            services.AddSingleton<IStorageService, NetStorageService>();
#endif

            return services;
        }
    }
}