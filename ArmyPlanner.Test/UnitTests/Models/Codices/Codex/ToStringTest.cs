using FluentAssertions;

namespace ArmyPlanner.Test.UnitTests.Models.Codices.Codex
{
    [TestClass]
    public class ToStringTest
    {
        [TestMethod]
        public void TestWithDefaultGameAndCodexKey()
        {
            // prepare
            string codexKey = "codex-key";
            string gameKey = "big-game.key";
            string expectedResult = $"game: {gameKey};codex: {codexKey};";
            ArmyPlanner.Models.Codices.Codex codex = new ArmyPlanner.Models.Codices.Codex
            {
                Meta = new ArmyPlanner.Models.Codices.MetaContainer
                {
                    Codex = new ArmyPlanner.Models.Codices.CodexMeta
                    {
                        Key = codexKey
                    },
                    Game = new ArmyPlanner.Models.Codices.GameMeta
                    {
                        Key = gameKey
                    }
                }
            };

            // run
            string codexToString = codex.ToString();

            // test
            codexToString.Should().NotBeNullOrEmpty();
            codexToString.Should().NotBeNullOrWhiteSpace();
            codexToString.Should().Be(expectedResult);
        }

        [TestMethod]
        public void TestWithNull()
        {
            // prepare
            string codexKey = "codex-key";
            string gameKey = "big-game.key";
            string expectedResult = string.Empty;
            ArmyPlanner.Models.Codices.Codex codex = new ArmyPlanner.Models.Codices.Codex();

            // run
            string codexToString = codex.ToString();

            // test
            codexToString.Should().Be(expectedResult);
        }
    }
}
