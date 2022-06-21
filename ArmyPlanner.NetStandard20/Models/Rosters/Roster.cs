using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ArmyPlanner.Models.Rosters
{
    public class Roster
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// the unique path of the game like => /w40k
        /// 
        /// the beginning '/' is important!
        /// </summary>
        [JsonProperty("required-game")]
        public string RequiredGame { get; set; }

        [JsonProperty("required-codizes")]
        public List<RequiredCodex> RequiredCodizes { get; set; }

        [JsonProperty("unit-configurations")]
        public List<UnitConfiguration> UnitConfigurations { get; set; }

        public Roster()
        {
            this.RequiredCodizes = new List<RequiredCodex>();
            this.UnitConfigurations = new List<UnitConfiguration>();
        }
    }
}