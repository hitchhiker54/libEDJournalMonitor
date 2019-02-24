using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace libEDJournalMonitor
{
    sealed public class EDLogNewCommander : EDLogEntry
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("FID")]
        public string FID { get; set; }
        [JsonProperty("Package")]
        public string Package { get; set; }

        public override void ProcessEvent(ref Commander Commander)
        {
            Commander.Name = Name;
            Commander.FID = FID;
            Commander.Package = Package;
        }
    }
}
