using ArmyPlanner.Exceptions;
using ArmyPlanner.Interfaces;
using ArmyPlanner.Models.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ArmyPlanner.Test.UnitTests.Services.Roster
{
    [TestClass]
    public class EnsureRosterAsyncTest
    {
        [TestMethod]
        public async Task TestWithMissingGame()
        {
            // prepare
            string missingGameKey = "/unit-test";
            IRosterService rosterService = Startup.ServiceProvider.GetService<IRosterService>()!;
            bool hasRequiredException = false;

            var roster = new Models.Rosters.Roster
            {
                Id = System.Guid.NewGuid(),
                Name = "TEST",
                Description = "UnitTest",
                RequiredGame = missingGameKey,
                RequiredCodizes = new List<Models.Rosters.RequiredCodex>
                {
                    new Models.Rosters.RequiredCodex
                    {
                        Key = "test",
                        Language = "test"
                    }
                }
            };

            try
            {
                // run
                await rosterService.EnsureRosterAsync(roster);
            }
            // test
            catch (MissingGameException exception)
            {
                hasRequiredException = true;

                exception.MissingGameKey.Should().Be(missingGameKey);
            }

            if (!hasRequiredException)
            {
                throw new InvalidOperationException(nameof(MissingGameException) + " is missing!");
            }
        }

        [TestMethod]
        public async Task TestWithExistingGame()
        {
            // prepare
            string missingGameKey = "/another-unit-test";
            IRosterService rosterService = Startup.ServiceProvider.GetService<IRosterService>()!;
            IStorageService storageService = Startup.ServiceProvider.GetService<IStorageService>()!;

            List<GameEntry> games = new List<GameEntry>
            {
                new GameEntry
                {
                    Title = "Unit-Test",
                    Path = missingGameKey
                }
            };
            string indexJsonData = JsonConvert.SerializeObject(games);
            await storageService.WriteDataAsync("armyplanner-index.json",
                indexJsonData,
                $"app_data{Path.DirectorySeparatorChar}data");

            var roster = new Models.Rosters.Roster
            {
                Id = System.Guid.NewGuid(),
                Name = "TEST",
                Description = "UnitTest",
                RequiredGame = missingGameKey,
                RequiredCodizes = new List<Models.Rosters.RequiredCodex>
                {
                    new Models.Rosters.RequiredCodex
                    {
                        Key = "test",
                        Language = "test"
                    }
                }
            };

            try
            {
                // run
                await rosterService.EnsureRosterAsync(roster);
            }
            // test
            catch (MissingGameException)
            {
                true.Should().Be(false);
                
                throw;
            }

            // If the test passes this point, the test is successful
            roster.RequiredGame.Should().Be(missingGameKey);
        }
    }
}