using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArmyPlanner.Models.Codices
{
    public class Option
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("target-unit")]
        public string TargetUnit { get; set; }

        [JsonProperty("target-equipment")]
        public string TargetEquipment { get; set; }

        [JsonProperty("max-count")]
        public int MaxCount { get; set; }

        [JsonProperty("additional-power")]
        public int AdditionalPower { get; set; }

        [JsonProperty("actions")]
        public List<OptionAction> Actions { get; set; }

        public Option()
        {
            this.Actions = new List<OptionAction>();
        }
    }
}