using System;
using System.Collections.Generic;
using System.Text;

namespace libEDJournalMonitor
{
    public partial class Commander
    {
        public bool Docked
        {
            get
            {
                return (Flags & (uint)StatusFlags.Docked) != 0;
            }
        }

        public bool Landed
        {
            get
            {
                return (Flags & (uint)StatusFlags.Landed) != 0;
            }
        }

        public bool LandingGearDown
        {
            get
            {
                return (Flags & (uint)StatusFlags.LandingGearDown) != 0;
            }
        }

        public bool ShieldUp
        {
            get
            {
                return (Flags & (uint)StatusFlags.ShieldsUp) != 0;
            }
        }

        public bool Supercruise
        {
            get
            {
                return (Flags & (uint)StatusFlags.Supercruise) != 0;
            }
        }

        public bool FlightAssistOff
        {
            get
            {
                return (Flags & (uint)StatusFlags.FlightAssistOff) != 0;
            }
        }

        public bool HardpointsDeployed
        {
            get
            {
                return (Flags & (uint)StatusFlags.HardpointsDeployed) != 0;
            }
        }

        public bool InWing
        {
            get
            {
                return (Flags & (uint)StatusFlags.InWing) != 0;
            }
        }

        public bool LightsOn
        {
            get
            {
                return (Flags & (uint)StatusFlags.LightsOn) != 0;
            }
        }

        public bool CargoScoopDeployed
        {
            get
            {
                return (Flags & (uint)StatusFlags.CargoScoopDeployed) != 0;
            }
        }

        public bool SilentRunning
        {
            get
            {
                return (Flags & (uint)StatusFlags.SilentRunning) != 0;
            }
        }

        public bool ScoopingFuel
        {
            get
            {
                return (Flags & (uint)StatusFlags.ScoopingFuel) != 0;
            }
        }

        public bool SrvHandbrake
        {
            get
            {
                return (Flags & (uint)StatusFlags.SrvHandbrake) != 0;
            }
        }

        public bool SrvTurret
        {
            get
            {
                return (Flags & (uint)StatusFlags.SrvTurret) != 0;
            }
        }

        public bool SrvUnderShip
        {
            get
            {
                return (Flags & (uint)StatusFlags.SrvUnderShip) != 0;
            }
        }

        public bool SrvDriveAssist
        {
            get
            {
                return (Flags & (uint)StatusFlags.SrvDriveAssist) != 0;
            }
        }

        public bool FsdMassLocked
        {
            get
            {
                return (Flags & (uint)StatusFlags.FsdMassLocked) != 0;
            }
        }

        public bool FsdCharging
        {
            get
            {
                return (Flags & (uint)StatusFlags.FsdCharging) != 0;
            }
        }

        public bool FsdCooldown
        {
            get
            {
                return (Flags & (uint)StatusFlags.FsdCooldown) != 0;
            }
        }

        public bool LowFuel
        {
            get
            {
                return (Flags & (uint)StatusFlags.LowFuel) != 0;
            }
        }

        public bool Overheating
        {
            get
            {
                return (Flags & (uint)StatusFlags.Overheating) != 0;
            }
        }

        public bool HasLatLong
        {
            get
            {
                return (Flags & (uint)StatusFlags.HasLatLong) != 0;
            }
        }

        public bool InDanger
        {
            get
            {
                return (Flags & (uint)StatusFlags.IsInDanger) != 0;
            }
        }

        public bool BeingInterdicted
        {
            get
            {
                return (Flags & (uint)StatusFlags.BeingInterdicted) != 0;
            }
        }

        public bool InMainShip
        {
            get
            {
                return (Flags & (uint)StatusFlags.InMainShip) != 0;
            }
        }

        public bool InFighter
        {
            get
            {
                return (Flags & (uint)StatusFlags.InFighter) != 0;
            }
        }

        public bool InSrv
        {
            get
            {
                return (Flags & (uint)StatusFlags.InSrv) != 0;
            }
        }

        public bool AnalysisMode
        {
            get
            {
                return (Flags & (uint)StatusFlags.InSrv) != 0;
            }
        }

        public bool NightVision
        {
            get
            {
                return (Flags & (uint)StatusFlags.InSrv) != 0;
            }
        }
    }
}
