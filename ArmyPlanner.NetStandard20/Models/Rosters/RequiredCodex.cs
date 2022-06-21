using Newtonsoft.Json;

namespace ArmyPlanner.Models.Rosters
{
    public class RequiredCodex
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}