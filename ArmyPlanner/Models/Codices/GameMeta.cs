using Newtonsoft.Json;

namespace ArmyPlanner.Models.Codices
{
    public class GameMeta
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}