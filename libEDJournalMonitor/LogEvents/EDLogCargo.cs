using Newtonsoft.Json;

namespace libEDJournalMonitor
{
    public class EDLogCargoItem
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Name_Localised")]
        public string Name_Localised { get; set; }
        [JsonProperty("Count")]
        public int Count { get; set; }
        [JsonProperty("Stolen")]
        public int Stolen { get; set; }
        [JsonProperty("MissionID")]
        public long? MissionID { get; set; }
    }

    sealed public class EDLogCargo : EDLogEntry
    {
        [JsonProperty("Vessel")]
        public string Vessel { get; set; }
        [JsonProperty("Count")]
        public int? Count { get; set; }
        [JsonProperty("Inventory")]
        public EDLogCargoItem[] Inventory { get; set; }

        public override void ProcessEvent(ref Commander Commander)
        {
            Commander.Cargo = (float)Count;

            if(Vessel == "Ship")
            {
                Commander.Inventory = Inventory;
            }
        }
    }
}
