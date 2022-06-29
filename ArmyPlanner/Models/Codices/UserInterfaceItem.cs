using Newtonsoft.Json;

namespace ArmyPlanner.Models.Codices
{
    public class UserInterfaceItem
    {
        [JsonProperty("header")]
        public string Header { get; set; }

        [JsonProperty("property")]
        public string Property { get; set; }
    }
}