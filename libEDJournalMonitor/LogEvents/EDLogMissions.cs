using System;
using System.Collections.Generic;
using System.Text;

namespace libEDJournalMonitor
{
    public class EDLogMission
    {
        public long MissionID { get; set; }
        public string Name { get; set; }
        public bool PassengerMission { get; set; }
        public long Expires { get; set; }
    }

    sealed public class EDLogMissions : EDLogEntry
    {
        public EDLogMission[] Active { get; set; }
        public EDLogMission[] Failed { get; set; }
        public EDLogMission[] Complete { get; set; }

        public override void ProcessEvent(ref Commander Commander)
        {
            Commander.Missions = this;
        }
    }  
}
