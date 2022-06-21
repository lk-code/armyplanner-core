using Newtonsoft.Json;

namespace ArmyPlanner.Models.Codices
{
    public class Wargear
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("effect")]
        public string Effect { get; set; }
    }
}