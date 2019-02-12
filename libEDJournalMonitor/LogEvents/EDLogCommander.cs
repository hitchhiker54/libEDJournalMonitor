using Newtonsoft.Json;

namespace libEDJournalMonitor
{
    sealed public class EDLogCommander : EDLogEntry
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("FID")]
        public string FID { get; set; }  

        public override void ProcessEvent(ref Commander Commander)
        {
            Commander.Name = Name;
            Commander.FID = FID;

            EntryType = LogEntryType.Commander;
        }
    }
}
