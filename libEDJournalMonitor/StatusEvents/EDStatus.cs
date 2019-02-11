using System;
using System.Collections.Generic;
using System.Text;

namespace libEDJournalMonitor
{
    sealed internal class EDStatus : EDLogEntry
    {
        public class FuelTanks
        {
            public float FuelMain { get; set; }
            public float FuelReservoir { get; set; }
        }

        public uint? Flags { get; set; } = 0;
        public int[] Pips { get; set; }
        public int? Firegroup { get; set; }
        public GuiFocus GuiFocus { get; set; }
        public FuelTanks Fuel { get; set; }
        public float? Cargo { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public float? Altitude { get; set; }
        public float? Heading { get; set; }

        public bool HasLatLong
        {
            get
            {
                return Latitude != null;
            }
        }

        public override void ProcessEvent(ref Commander commander)
        {
            commander.Firegroup = Firegroup;
            commander.Flags = Flags;
            commander.GuiFocus = GuiFocus;
            commander.Cargo = Cargo;
            commander.Latitude = Latitude;
            commander.Longitude = Longitude;
            commander.Altitude = Altitude;
            commander.Heading = Heading;
        }
    }
}
