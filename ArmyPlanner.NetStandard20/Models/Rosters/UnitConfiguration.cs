using Newtonsoft.Json;
using System;

namespace ArmyPlanner.Models.Rosters
{
    public class UnitConfiguration
    {
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("unit-key")]
        public string UnitKey { get; set; }

        [JsonProperty("codex-key")]
        public string CodexKey { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}