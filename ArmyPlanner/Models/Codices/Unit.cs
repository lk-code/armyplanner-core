using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArmyPlanner.Models.Codices
{
    public class Unit
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("unit-type")]
        public string UnitType { get; set; }

        [JsonProperty("power")]
        public int Power { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("factions")]
        public List<string> Factions { get; set; }

        [JsonProperty("weapons")]
        public List<string> Weapons { get; set; }

        [JsonProperty("characteristics")]
        public List<Characteristics> Characteristics { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }

        public Unit()
        {
            this.Factions = new List<string>();
            this.Weapons = new List<string>();
            this.Characteristics = new List<Characteristics>();
            this.Options = new List<Option>();
        }
    }
}