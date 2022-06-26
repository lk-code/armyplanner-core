using Newtonsoft.Json;

namespace ArmyPlanner.Models.Repositories
{
    public class GameIndex
    {
        /// <summary>
        /// Url to the Index of the Game.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}