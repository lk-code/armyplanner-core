using Newtonsoft.Json;

namespace ArmyPlanner.Models.Codices
{
    public class CodexLanguage
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("lang")]
        public string LanguageCode { get; set; }
    }
}