using ArmyPlanner.Interfaces;
using ArmyPlanner.Services.Repository;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace ArmyPlanner.Test.UnitTests.Services.Repository
{
    [TestClass]
    public class GetGameStorageFilePathTest
    {
        [TestMethod]
        public async Task TestWithCorrectGameFolderName()
        {
            // prepare
            string repositoryBasePath = "app_data";
            IRepositoryService repositoryService = Startup.ServiceProvider.GetService<IRepositoryService>()!;
            repositoryService.SetBasePathForData(repositoryBasePath);

            string directory = RepositoryService.REPOSITORY_LOCALSTORAGE_FOLDER;
            Dictionary<string, string> testValues = new Dictionary<string, string>
            {
                { "/w40k", $"{repositoryBasePath}{Path.DirectorySeparatorChar}{directory}{Path.DirectorySeparatorChar}w40k" },
                { "anothergame", $"{repositoryBasePath}{Path.DirectorySeparatorChar}{directory}{Path.DirectorySeparatorChar}anothergame" },
                { "\\w40k", $"{repositoryBasePath}{Path.DirectorySeparatorChar}{directory}{Path.DirectorySeparatorChar}w40k" },
                { "/w4-0k", $"{repositoryBasePath}{Path.DirectorySeparatorChar}{directory}{Path.DirectorySeparatorChar}w4-0k" },
                { "another-game", $"{repositoryBasePath}{Path.DirectorySeparatorChar}{directory}{Path.DirectorySeparatorChar}another-game" },
                { "\\w4-0k", $"{repositoryBasePath}{Path.DirectorySeparatorChar}{directory}{Path.DirectorySeparatorChar}w4-0k" }
            };

            foreach (KeyValuePair<string, string> testValue in testValues)
            {
                // run
                string result = repositoryService.GetGameStorageFilePath(testValue.Key);

                // test
                result.Should().Be(testValue.Value);
            }
        }
    }
}