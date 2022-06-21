using Newtonsoft.Json;

namespace ArmyPlanner.Models.Codices
{
    public class Keyword
    {
        [JsonProperty("keyword")]
        public string Key { get; set; }

        [JsonProperty("translation")]
        public string Translation { get; set; }
    }
}