using ArmyPlanner.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
            IStorageService storageService = Startup.ServiceProvider.GetService<IStorageService>()!;
            IRepositoryService repositoryService = Startup.ServiceProvider.GetService<IRepositoryService>()!;

            string codexFile = "tau-empire-2022.json";
            string gameKey = "/w40k";
            string gameStorageFolder = repositoryService.GetGameStorageFilePath(gameKey);
            ArmyPlanner.Models.Codices.Codex codex = new ArmyPlanner.Models.Codices.Codex
            {
                Meta = new ArmyPlanner.Models.Codices.MetaContainer
                {
                    Codex = new ArmyPlanner.Models.Codices.CodexMeta
                    {
                        Name = "A Codex :D",
                        Key = "codex"
                    },
                    Game = new ArmyPlanner.Models.Codices.GameMeta
                    {
                        Name = "Test-Game",
                        Key = "game"
                    }
                }
            };
            await storageService.WriteDataAsync(codexFile,
                JsonConvert.SerializeObject(codex),
                gameStorageFolder);

            // run
            ArmyPlanner.Models.Codices.Codex loadedCodex = await codexService.GetCodexAsync(gameKey,
                codexFile);

            // test
            loadedCodex.Meta.Codex.Name.Should().Be(codex.Meta.Codex.Name);
            loadedCodex.Meta.Codex.Key.Should().Be(codex.Meta.Codex.Key);
            loadedCodex.Meta.Game.Name.Should().Be(codex.Meta.Game.Name);
            loadedCodex.Meta.Game.Key.Should().Be(codex.Meta.Game.Key);
        }
    }
}
