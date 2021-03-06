using ArmyPlanner.Interfaces;
using ArmyPlanner.Test.Base.Models;
using ArmyPlanner.Test.Base.Services.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ArmyPlanner.Test.UnitTests.Services.Repository
{
    [TestClass]
    public class SubscribeGameAsyncTest
    {
        [TestMethod]
        public async Task TestWithCorrectGameFolderName()
        {
            // prepare
            IRepositoryService repositoryService = Startup.ServiceProvider.GetService<IRepositoryService>()!;
            TestHttpService httpService = (TestHttpService)Startup.ServiceProvider.GetService<IHttpService>()!;

            httpService.RegisterHttpContent("https://raw.githubusercontent.com/lk-code/armyplanner/main/w40k/codex1.json", JsonConvert.SerializeObject(new ArmyPlanner.Models.Codices.Codex()));
            httpService.RegisterHttpContent("https://raw.githubusercontent.com/lk-code/armyplanner/main/w40k/codex2.json", JsonConvert.SerializeObject(new ArmyPlanner.Models.Codices.Codex()));

            // run
            await repositoryService.SubscribeGameAsync(new DownloadableGame(new ArmyPlanner.Models.Repositories.GameEntry
            {
                Path = "/w40k",
                Codices = new List<ArmyPlanner.Models.Repositories.CodexEntry>
                {
                    new ArmyPlanner.Models.Repositories.CodexEntry
                    {
                        Path = "codex1.json",
                        Title = "codex1"
                    },
                    new ArmyPlanner.Models.Repositories.CodexEntry
                    {
                        Path = "codex2.json",
                        Title = "codex2"
                    },
                }
            }));
        }
    }
}
