using ArmyPlanner.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace ArmyPlanner.Test
{
    [TestClass]
    public class Initialize
    {
        [AssemblyInitialize]
        public static async Task AssemblyInitializeAsync(TestContext context)
        {
            Debug.WriteLine("Assembly Initialize");

            await Startup.Init();

            #region uncommented
            /*
            // DateTimeService
            DateTimeService dateTimeService = Startup.ServiceProvider.GetService<IDateTimeService>() as DateTimeService;
            dateTimeService.FakeDateTime = System.DateTime.Now; // set as default

            // ConfigService
            IConfigService configService = Startup.ServiceProvider.GetService<IConfigService>();
            string json = @"{
                             ""Config"": {
                                       ""Server"": {
                                                    ""Url"": ""#""
                                       }
                             }
            }";
            configService.Initialize(JObject.Parse(json));
            /* */
            #endregion

            InitializeAppDataPaths();

            Debug.WriteLine("Assembly Initialized");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Debug.WriteLine("Assembly Cleanup");

            Debug.WriteLine("Assembly Cleanup finished");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static void InitializeAppDataPaths()
        {
            // CodexService
            ICodexService codexService = Startup.ServiceProvider.GetService<ICodexService>()!;
            codexService.SetBasePathForCodexData("app_data");

            // RepositoryService
            IRepositoryService repositoryService = Startup.ServiceProvider.GetService<IRepositoryService>()!;
            repositoryService.SetBasePathForData("app_data");

            // RosterService
            IRosterService rosterService = Startup.ServiceProvider.GetService<IRosterService>()!;
            rosterService.SetBasePathForRosterData("app_docs");
        }
    }
}