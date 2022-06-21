using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArmyPlanner.Models.Codices
{
    public class Characteristics
    {
        [JsonProperty("countMin")]
        public int CountMin { get; set; }

        [JsonProperty("countMax")]
        public int? CountMax { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("move")]
        public string Move { get; set; }

        [JsonProperty("weapon-skill")]
        public string WeaponSkill { get; set; }

        [JsonProperty("ballistic-skill")]
        public string BallisticSkill { get; set; }

        [JsonProperty("strength")]
        public string Strength { get; set; }

        [JsonProperty("toughness")]
        public string Toughness { get; set; }

        [JsonProperty("wounds")]
        public string Wounds { get; set; }

        [JsonProperty("attacks")]
        public string Attacks { get; set; }

        [JsonProperty("leadership")]
        public string Leadership { get; set; }

        [JsonProperty("save")]
        public string Save { get; set; }

        [JsonProperty("damage-profiles")]
        public List<Characteristics> DamageProfiles { get; set; } = new List<Characteristics>();
    }
}