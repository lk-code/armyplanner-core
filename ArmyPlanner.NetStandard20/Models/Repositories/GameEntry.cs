using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArmyPlanner.Models.Repositories
{
    /// <summary>
    /// Contains the Repository-Data of a Game.
    /// </summary>
    public class GameEntry
    {
        /// <summary>
        /// The title of the Game.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// The description of the Game.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Url to the Index of the Game.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
        /// <summary>
        /// The list of all codizies for this game. Loaded by the RepositoryService.
        /// </summary>
        [JsonProperty("codices")]
        public List<CodexEntry> Codices { get; set; } = new List<CodexEntry>();

        public override string ToString()
        {
            return $"{nameof(GameEntry)} - {this.Title} - {this.Codices.Count} codices loaded";
        }
    }
}