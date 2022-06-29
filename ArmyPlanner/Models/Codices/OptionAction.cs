using Newtonsoft.Json;

namespace ArmyPlanner.Models.Codices
{
    public class OptionAction
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("source-unit")]
        public string SourceUnit { get; set; }

        [JsonProperty("source-equipment")]
        public string SourceEquipment { get; set; }

        [JsonProperty("target-unit")]
        public string TargetUnit { get; set; }

        [JsonProperty("target-equipment")]
        public string TargetEquipment { get; set; }

        [JsonProperty("max-count")]
        public int? MaxCount { get; set; }
    }
}
