using Newtonsoft.Json;

namespace ArmyPlanner.Models.Codices
{
    public class CodexMeta
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}