using ArmyPlanner.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ArmyPlanner.Test.UnitTests.Services.Codex
{
    [TestClass]
    public class GetCodexTranslationsAsyncTest
    {
        [TestMethod]
        public async Task TestWithW40KTauEmpire2022AndGermanLanguage()
        {
            // prepare
            ICodexService codexService = Startup.ServiceProvider.GetService<ICodexService>()!;
            IStorageService storageService = Startup.ServiceProvider.GetService<IStorageService>()!;

            string languageCode = "de";
            string gameKey = "w40k";
            ArmyPlanner.Models.Codices.Codex codex = new ArmyPlanner.Models.Codices.Codex
            {
                Meta = new ArmyPlanner.Models.Codices.MetaContainer
                {
                    Game = new ArmyPlanner.Models.Codices.GameMeta
                    {
                        Key = gameKey
                    },
                    Codex = new ArmyPlanner.Models.Codices.CodexMeta
                    {
                        Key = "tau-empire-2022"
                    }
                }
            };
            ArmyPlanner.Models.Codices.CodexLanguage codexLanguage = new ArmyPlanner.Models.Codices.CodexLanguage
            {
                LanguageCode = languageCode
            };


            await storageService.WriteDataAsync($"tau-empire-2022.{languageCode}.json",
                JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    { "game_key", "a Game" },
                    { "codex_key", "a Codex" }
                }),
                $"app_data{Path.DirectorySeparatorChar}data{Path.DirectorySeparatorChar}{gameKey}");

            // run
            Dictionary<string, string> translations = await codexService.GetCodexTranslationsAsync(codex, codexLanguage);

            // test
            translations.Should().NotBeEmpty();
        }
    }
}
