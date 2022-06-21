using ArmyPlanner.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ArmyPlanner.NetStandard20.Test.UnitTests.Services.Translation
{
    [TestClass]
    public class TranslateTest
    {
        [TestMethod]
        public void TestOnlyTemplateAndTanslations()
        {
            // prepare
            string template = "{{test_template,5,3}}";
            string translation = "Dies ist ein Test mit {0} Einträgen und {1} Feldern.";
            Dictionary<string, string> translations = new Dictionary<string, string>
            {
                { "test", "blub" },
                { "test_template", translation },
                { "name", "Ein Name für den Test" }
            };
            string expection = "Dies ist ein Test mit 5 Einträgen und 3 Feldern.";
            ITranslationService codexTranslationService = Startup.ServiceProvider.GetService<ITranslationService>()!;

            // run
            string result = codexTranslationService.Translate(template, translations);

            // test
            Assert.AreEqual(expection, result);
        }

        [TestMethod]
        public void TestWithMultipleEntries()
        {
            // prepare
            string template = "Ein Test mit {{test_template,5,3,9}} und {{testnew_template}}.";
            Dictionary<string, string> translations = new Dictionary<string, string>
            {
                { "test_template", "Text mit {0} Zeichen und {1} bis {2} Feldern." },
                { "testnew_template", "Gelaber" }
            };
            string expection = "Ein Test mit Text mit 5 Zeichen und 3 bis 9 Feldern. und Gelaber.";
            ITranslationService codexTranslationService = Startup.ServiceProvider.GetService<ITranslationService>()!;

            // run
            string result = codexTranslationService.Translate(template, translations);

            // test
            Assert.AreEqual(expection, result);
        }
    }
}