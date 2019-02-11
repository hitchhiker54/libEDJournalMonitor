using System;
using System.Collections.Generic;
using System.Text;

namespace libEDJournalMonitor
{
    public class StarSystem
    {
        public string SystemName { get; internal set; }
        public ulong SystemAddress { get; internal set; }
        public float[] StarPos { get; internal set; }
        public string SystemFaction { get; internal set; }
        public string FactionState { get; internal set; }
        public string SystemAllegiance { get; internal set; }
        public string SystemEconomy { get; internal set; }
        public string SystemSecondEconomy { get; internal set; }
        public string SystemGovernment { get; internal set; }
        public string SystemSecurity { get; internal set; }
        public long? Population { get; internal set; }
        public string[] Powers { get; set; }
        public string PowerplayState { get; set; }
    }
}
