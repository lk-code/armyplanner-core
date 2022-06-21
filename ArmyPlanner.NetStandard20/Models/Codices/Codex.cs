using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArmyPlanner.Models.Codices
{
    public class Codex
    {
        #region properties

        [JsonProperty("$schema")]
        public string Schema { get; private set; } = "https://raw.githubusercontent.com/lk-code/armyplanner/main/codex-schema-v1.json";

        [JsonIgnore]
        public Codex BasedOnCodex { get; set; } = null;

        [JsonProperty("meta")]
        public MetaContainer Meta { get; set; }

        [JsonProperty("languages")]
        public List<CodexLanguage> Languages { get; set; }

        [JsonProperty("keywords")]
        public List<Keyword> Keywords { get; set; }

        [JsonProperty("units")]
        public List<Unit> Units { get; set; }

        [JsonProperty("weapons")]
        public List<Weapon> Weapons { get; set; }

        [JsonProperty("wargear")]
        public List<Wargear> Wargear { get; set; }

        [JsonProperty("ui")]
        public List<UserInterface> UserInterfaceFields { get; set; }

        #endregion

        #region constructor

        public Codex()
        {
            this.Meta = new MetaContainer();
            this.Languages = new List<CodexLanguage>();
            this.Keywords = new List<Keyword>();
            this.Units = new List<Unit>();
            this.Weapons = new List<Weapon>();
            this.Wargear = new List<Wargear>();
            this.UserInterfaceFields = new List<UserInterface>();
        }

        #endregion

        #region logic

        public bool IsSameCodex(Codex updatedCodex)
        {
            if (this.Meta.Game.Key.ToLowerInvariant().Equals(updatedCodex.Meta.Game.Key.ToLowerInvariant())
                && this.Meta.Codex.Key.ToLowerInvariant().Equals(updatedCodex.Meta.Codex.Key.ToLowerInvariant()))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}