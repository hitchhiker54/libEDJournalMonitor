using Newtonsoft.Json;

namespace libEDJournalMonitor
{
    sealed internal class EDLogClearSavedGame : EDLogEntry
    {

        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("FID")]
        public string FID { get; set; }

        public LogEntryType EntryType { get; } = LogEntryType.ClearSavedGame;

        public override void ProcessEvent(ref Commander Commander)
        {
            Commander.Name = Name;
            Commander.FID = FID;
        }
    }
}
