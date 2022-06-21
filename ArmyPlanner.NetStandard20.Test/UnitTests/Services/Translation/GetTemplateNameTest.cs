using ArmyPlanner.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ArmyPlanner.NetStandard20.Test.UnitTests.Services.Translation
{
    [TestClass]
    public class GetTemplateNameTest
    {
        [TestMethod]
        public void TestOnlyTemplate()
        {
            // prepare
            string template = "{{test_template}}";
            string expection = "test_template";
            List<string> parameters = new List<string> { "test_template" };
            ITranslationService codexTranslationService = Startup.ServiceProvider.GetService<ITranslationService>()!;

            // run
            Dictionary<string, List<string>> result = codexTranslationService.GetTemplateData(template);

            // test
            Assert.AreEqual(1, result.Count);
            foreach (KeyValuePair<string, List<string>> keyValuePair in result)
            {
                Assert.AreEqual(expection, keyValuePair.Key);
                Assert.AreEqual(parameters.Count, keyValuePair.Value.Count);

                for (int i = 0; i < parameters.Count; i++)
                {
                    string expectedParameter = parameters[i];
                    string parameter = keyValuePair.Value[i];
                    Assert.AreEqual(expectedParameter, parameter);
                }
            }
        }

        [TestMethod]
        public void TestWithAdditionalParameter()
        {
            // prepare
            string template = "{{test_template,5,3}}";
            string expection = "test_template";
            List<string> parameters = new List<string> { "test_template,5,3", "5", "3" };
            ITranslationService codexTranslationService = Startup.ServiceProvider.GetService<ITranslationService>()!;

            // run
            Dictionary<string, List<string>> result = codexTranslationService.GetTemplateData(template);

            // test
            Assert.AreEqual(1, result.Count);
            foreach (KeyValuePair<string, List<string>> keyValuePair in result)
            {
                Assert.AreEqual(expection, keyValuePair.Key);
                Assert.AreEqual(parameters.Count, keyValuePair.Value.Count);

                for (int i = 0; i < parameters.Count; i++)
                {
                    string expectedParameter = parameters[i];
                    string parameter = keyValuePair.Value[i];
                    Assert.AreEqual(expectedParameter, parameter);
                }
            }
        }
    }
}