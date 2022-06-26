using Newtonsoft.Json;

namespace ArmyPlanner.Models.Codices
{
    public class Header
    {
        [JsonProperty("weapon")]
        public string Weapon { get; set; }
        [JsonProperty("weapons")]
        public string Weapons { get; set; }
        [JsonProperty("range")]
        public string Range { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("strength")]
        public string Strength { get; set; }
        [JsonProperty("armour-penetration")]
        public string ArmourPenetration { get; set; }
        [JsonProperty("damage")]
        public string Damage { get; set; }
        [JsonProperty("abilities")]
        public string Abilities { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("move")]
        public string Move { get; set; }
        [JsonProperty("weapon-skill")]
        public string WeaponSkill { get; set; }
        [JsonProperty("ballistic-skill")]
        public string BallisticSkill { get; set; }
        [JsonProperty("toughness")]
        public string Toughness { get; set; }
        [JsonProperty("wounds")]
        public string Wounds { get; set; }
        [JsonProperty("attacks")]
        public string Attacks { get; set; }
        [JsonProperty("leadership")]
        public string Leadership { get; set; }
        [JsonProperty("save")]
        public string Save { get; set; }
        [JsonProperty("weapons-ranged")]
        public string WeaponsRanged { get; set; }
        [JsonProperty("weapons-melee")]
        public string WeaponsMelee { get; set; }
        [JsonProperty("wargear")]
        public string Wargear { get; set; }
        [JsonProperty("system")]
        public string System { get; set; }
        [JsonProperty("effect")]
        public string Effect { get; set; }
        [JsonProperty("battle-roll")]
        public string BattleRoll { get; set; }
        [JsonProperty("power")]
        public string Power { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }
        [JsonProperty("units")]
        public string Units { get; set; }
        [JsonProperty("unit-hq")]
        public string UnitHQ { get; set; }
        [JsonProperty("unit-troops")]
        public string UnitTroops { get; set; }
        [JsonProperty("unit-elites")]
        public string UnitElites { get; set; }
        [JsonProperty("unit-fast-attack")]
        public string UnitFastAttack { get; set; }
        [JsonProperty("unit-flyer")]
        public string UnitFlyer { get; set; }
        [JsonProperty("unit-heavy-support")]
        public string UnitHeavySupport { get; set; }
        [JsonProperty("unit-fortification")]
        public string UnitFortification { get; set; }
        [JsonProperty("unit-transport")]
        public string UnitTransport { get; set; }
        [JsonProperty("unit-lord-of-war")]
        public string UnitLordOfWar { get; set; }
        [JsonProperty("faction")]
        public string Faction { get; set; }
    }
}
