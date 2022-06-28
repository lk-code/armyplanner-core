using ArmyPlanner.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ArmyPlanner.Test.UnitTests.Services.Codex
{
    [TestClass]
    public class GetCodexAsyncTest
    {
        [TestMethod]
        public async Task TestWithCorrectGameFolderName()
        {
            // prepare
            ICodexService codexService = Startup.ServiceProvider.GetService<ICodexService>()!;
            //TestHttpService httpService = (TestHttpService)Startup.ServiceProvider.GetService<IHttpService>()!;

            //httpService.RegisterHttpContent("https://raw.githubusercontent.com/lk-code/armyplanner/main/w40k/codex1.json", JsonConvert.SerializeObject(new Models.Codices.Codex()));
            //httpService.RegisterHttpContent("https://raw.githubusercontent.com/lk-code/armyplanner/main/w40k/codex2.json", JsonConvert.SerializeObject(new Models.Codices.Codex()));

            // run
            await codexService.GetCodexAsync("/w40k", "tau-empire-2022.json");

            int i = 0;
        }
    }
}
