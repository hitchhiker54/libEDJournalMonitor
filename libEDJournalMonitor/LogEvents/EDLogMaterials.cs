using Newtonsoft.Json;

namespace libEDJournalMonitor
{
    public class EDLogMaterial
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Count")]
        public int Count { get; set; }
    }

    sealed public class EDLogMaterials : EDLogEntry
    {
        [JsonProperty("Raw")]
        public EDLogMaterial[] Raw { get; set; }
        [JsonProperty("Manufactured")]
        public EDLogMaterial[] Manufactured { get; set; }
        [JsonProperty("Encoded")]
        public EDLogMaterial[] Encoded { get; set; }

        public override void ProcessEvent(ref Commander Commander)
        {
            Commander.Materials = this;
        }
    }
}
