using Newtonsoft.Json;

namespace ArmyPlanner.Services.Roster.Models
{
    internal class RosterIndexEntry
    {
        [JsonProperty("file-id")]
        public string FileId { get; set; } = string.Empty;
    }
}