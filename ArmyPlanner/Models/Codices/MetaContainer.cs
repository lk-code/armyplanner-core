using Newtonsoft.Json;
using System;

namespace ArmyPlanner.Models.Codices
{
    public class MetaContainer
    {
        #region properties

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("based-codex")]
        public string BasedOnCodexKey { get; set; }

        [JsonProperty("game")]
        public GameMeta Game { get; set; }

        [JsonProperty("codex")]
        public CodexMeta Codex { get; set; }

        #endregion

        #region constructor

        public MetaContainer()
        {
            this.Game = new GameMeta();
            this.Codex = new CodexMeta();
        }

        #endregion
    }
}
