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
            IRepositoryService repositoryService = Startup.ServiceProvider.GetService<IRepositoryService>()!;

            string directory = RepositoryService.REPOSITORY_LOCALSTORAGE_FOLDER;
            Dictionary<string, string> testValues = new Dictionary<string, string>
            {
                { "/w40k", $"{directory}{Path.DirectorySeparatorChar}w40k" },
                { "w40k", $"{directory}{Path.DirectorySeparatorChar}w40k" },
                { "\\w40k", $"{directory}{Path.DirectorySeparatorChar}w40k" }
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