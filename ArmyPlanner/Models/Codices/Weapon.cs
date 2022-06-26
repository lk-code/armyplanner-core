using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArmyPlanner.Models.Codices
{
    public class Weapon
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("weapon-type")]
        public string WeaponType { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("dices")]
        public string Dices { get; set; }

        [JsonProperty("strength")]
        public string Strength { get; set; }

        [JsonProperty("armour-penetration")]
        public string ArmourPenetration { get; set; }

        [JsonProperty("damage")]
        public string Damage { get; set; }

        [JsonProperty("abilities")]
        public List<string> Abilities { get; set; }

        [JsonProperty("profiles")]
        public List<string> Profiles { get; set; }

        [JsonProperty("is-profile")]
        public bool? IsProfile { get; set; }

        public Weapon()
        {
            this.Abilities = new List<string>();
            this.Profiles = new List<string>();
        }
    }
}
