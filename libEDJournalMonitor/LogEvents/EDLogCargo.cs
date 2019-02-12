using Newtonsoft.Json;

namespace libEDJournalMonitor
{
    public class CargoItem
    {
        [JsonProperty("Name")]
        public string Name { get; internal set; }
        [JsonProperty("Name_Localised")]
        public string Name_Localised { get; internal set; }
        [JsonProperty("Count")]
        public int Count { get; internal set; }
        [JsonProperty("Stolen")]
        public int Stolen { get; internal set; }
        [JsonProperty("MissionID")]
        public long? MissionID { get; internal set; }
    }

    sealed public class EDLogCargo : EDLogEntry
    {
        [JsonProperty("Vessel")]
        public string Vessel { get; set; }
        [JsonProperty("Count")]
        public int? Count { get; set; }
        [JsonProperty("Inventory")]
        public CargoItem[] Inventory { get; set; }

        public override void ProcessEvent(ref Commander Commander)
        {
            Commander.Cargo = (float)Count;

            if(Vessel == "Ship")
            {
                Commander.Inventory = Inventory;
            }

            EntryType = LogEntryType.Cargo;
        }
    }
}
