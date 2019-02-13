using System;
using System.Collections.Generic;
using System.Text;

namespace libEDJournalMonitor
{
    public partial class Commander
    {
        public string Name { get; internal set; }
        public string FID { get; internal set; }
        public long Credits { get; internal set; }
        public long Loan { get; internal set; }
        public bool? Wanted { get; internal set; }

        public string ShipType { get; internal set; }
        public string ShipName { get; internal set; }
        public string ShipIdent { get; internal set; }
        public float FuelLevel { get; internal set; }
        public float FuelUsed { get; internal set; }        // fuel used last jump
        public bool BoostUsed { get; internal set; }        // was any fsd boost used
        public float FuelCapacity { get; internal set; }
        public int HullValue { get; internal set; }
        public int ModulesValue { get; internal set; }
        public float HullHealth { get; internal set; }
        public int Rebuy { get; internal set; }
        public bool? Hot { get; internal set; }
        public float? Cargo { get; internal set; }
        public float CargoSpace { get; internal set; } = 0;
        public EDLogCargoItem[] Inventory { get; internal set; }

        public StarSystem CurrentSystem { get; internal set; } = new StarSystem();
        public bool NearBody { get; internal set; }      // my own addition for checking correct body name
        public string Body { get; internal set; }
        public int? BodyID { get; internal set; }
        public string BodyType { get; internal set; }
        public string StationName { get; internal set; }
        public string StationType { get; internal set; }
        public long? MarketID { get; internal set; }
        public float? Latitude { get; internal set; }
        public float? Longitude { get; internal set; }
        public float? Altitude { get; internal set; }
        public float? Heading { get; internal set; }
        public int? Firegroup { get; internal set; }

        public GuiFocus GuiFocus { get; internal set; }
        public uint? Flags { get; internal set; }      

        //internal JournalCodexEntry LastCodexEntry { get; set; } = new JournalCodexEntry();
    }
}
