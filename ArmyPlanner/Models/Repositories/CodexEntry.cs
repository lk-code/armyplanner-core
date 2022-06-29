using Newtonsoft.Json;
using System;

namespace ArmyPlanner.Models.Repositories
{
    /// <summary>
    /// Contains the Repository-Data of a Codex
    /// </summary>
    public class CodexEntry
    {
        #region properties


        #endregion

        #region json properties

        /// <summary>
        /// The title of the Codex.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// The description of the Codex.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The description of the Codex.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Url to the Index of the Game.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        #endregion

        #region constructors
        
        

        #endregion

        #region logic

        public override string ToString()
        {
            return $"{nameof(CodexEntry)} - {this.Title}";
        }

        #endregion
    }
}