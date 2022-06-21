using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArmyPlanner.Models.Codices
{
    public class UserInterface
    {
        [JsonProperty("element")]
        public string Element { get; set; }

        [JsonProperty("items")]
        public List<UserInterfaceItem> Items { get; set; }

        public UserInterface()
        {
            this.Items = new List<UserInterfaceItem>();
        }
    }
}