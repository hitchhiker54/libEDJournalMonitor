using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libEDJournalMonitor;
using Newtonsoft.Json;
using Xunit;

namespace libEdJournalMonitor.Tests
{
    public class LogTestsStartupCargo
    {
        [Fact]
        public void EDLogCargo_Vessel()
        {
            string entry = "{  \"timestamp\": \"2019-02-19T20:05:06Z\",  \"event\": \"Cargo\",  \"Vessel\": \"Ship\",  \"Count\": 0,  \"Inventory\": []}";

            EDLogCargo edLogCargo = new EDLogCargo();
            edLogCargo = JsonConvert.DeserializeObject<EDLogCargo>(entry);

            var expected = "Ship";
            var actual = edLogCargo.Vessel;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EDLogCargo_Count()
        {
            string entry = "{  \"timestamp\": \"2019-02-19T20:05:06Z\",  \"event\": \"Cargo\",  \"Vessel\": \"Ship\",  \"Count\": 0,  \"Inventory\": []}";

            EDLogCargo edLogCargo = new EDLogCargo();
            edLogCargo = JsonConvert.DeserializeObject<EDLogCargo>(entry);

            var expected = 0;
            var actual = edLogCargo.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EDLogCargo_InventoryName()
        {
            string entry = "{  \"timestamp\": \"2019-02-20T19:48:10Z\",  \"event\": \"Cargo\",  \"Vessel\": \"Ship\",  \"Count\": 15,  \"Inventory\": [    {      \"Name\": \"drones\",      \"Name_Localised\": \"Limpet\",      \"Count\": 15,      \"Stolen\": 0    }  ]}";

            EDLogCargo edLogCargo = new EDLogCargo();
            edLogCargo = JsonConvert.DeserializeObject<EDLogCargo>(entry);

            var expected = "drones";
            var actual = edLogCargo.Inventory[0].Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EDLogCargo_InventoryNameLocalised()
        {
            string entry = "{  \"timestamp\": \"2019-02-20T19:48:10Z\",  \"event\": \"Cargo\",  \"Vessel\": \"Ship\",  \"Count\": 15,  \"Inventory\": [    {      \"Name\": \"drones\",      \"Name_Localised\": \"Limpet\",      \"Count\": 15,      \"Stolen\": 0    }  ]}";

            EDLogCargo edLogCargo = new EDLogCargo();
            edLogCargo = JsonConvert.DeserializeObject<EDLogCargo>(entry);

            var expected = "Limpet";
            var actual = edLogCargo.Inventory[0].Name_Localised;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EDLogCargo_InventoryCount()
        {
            string entry = "{  \"timestamp\": \"2019-02-20T19:48:10Z\",  \"event\": \"Cargo\",  \"Vessel\": \"Ship\",  \"Count\": 15,  \"Inventory\": [    {      \"Name\": \"drones\",      \"Name_Localised\": \"Limpet\",      \"Count\": 15,      \"Stolen\": 0    }  ]}";

            EDLogCargo edLogCargo = new EDLogCargo();
            edLogCargo = JsonConvert.DeserializeObject<EDLogCargo>(entry);

            var expected = 15;
            var actual = edLogCargo.Inventory[0].Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EDLogCargo_InventoryStolen()
        {
            string entry = "{  \"timestamp\": \"2019-02-20T19:48:10Z\",  \"event\": \"Cargo\",  \"Vessel\": \"Ship\",  \"Count\": 15,  \"Inventory\": [    {      \"Name\": \"drones\",      \"Name_Localised\": \"Limpet\",      \"Count\": 15,      \"Stolen\": 0    }  ]}";

            EDLogCargo edLogCargo = new EDLogCargo();
            edLogCargo = JsonConvert.DeserializeObject<EDLogCargo>(entry);

            var expected = 0;
            var actual = edLogCargo.Inventory[0].Stolen;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EDLogCargo_InventoryMissionID()
        {
            string entry = "{  \"timestamp\": \"2019-02-20T19:48:10Z\",  \"event\": \"Cargo\",  \"Vessel\": \"Ship\",  \"Count\": 15,  \"Inventory\": [    {      \"Name\": \"drones\",      \"Name_Localised\": \"Limpet\",      \"Count\": 15,      \"Stolen\": 0    }  ]}";

            EDLogCargo edLogCargo = new EDLogCargo();
            edLogCargo = JsonConvert.DeserializeObject<EDLogCargo>(entry);

            long? expected = null;
            var actual = edLogCargo.Inventory[0].MissionID;

            Assert.Equal(expected, actual);
        }

        [Theory]    // correct parsing of inventory : cargo hold but empty (also covers no cargo bays), cargo hold with 3 cargo types
        [InlineData("{  \"timestamp\": \"2019-02-19T20:05:06Z\",  \"event\": \"Cargo\",  \"Vessel\": \"Ship\",  \"Count\": 0,  \"Inventory\": []}", 0)]
        [InlineData("{  \"timestamp\": \"2019-02-10T19:22:36Z\",  \"event\": \"Cargo\",  \"Vessel\": \"Ship\",  \"Count\": 64,  \"Inventory\": [    {      \"Name\": \"lowtemperaturediamond\",      \"Name_Localised\": \"Low Temperature Diamonds\",      \"Count\": 4,      \"Stolen\": 0    },    {      \"Name\": \"opal\",      \"Name_Localised\": \"Void Opals\",      \"Count\": 10,      \"Stolen\": 0    },    {      \"Name\": \"drones\",      \"Name_Localised\": \"Limpet\",      \"Count\": 50,      \"Stolen\": 0    }  ]}"
                        , 3)]
        public void EDLogCargo_UniqueItemCount(string entry, int expected)
        {            
            Commander Commander = new Commander();

            EDLogCargo edLogCargo = new EDLogCargo();
            edLogCargo = JsonConvert.DeserializeObject<EDLogCargo>(entry);

            edLogCargo.ProcessEvent(ref Commander);

            var actual = Commander.Inventory.Length;

            Assert.Equal(expected, actual);
        }  
    }

    public class LogTestsStartupCommander
    {
        [Fact]
        public void EDLogCommander_Name()
        {
            string entry = "{ \"timestamp\": \"2019-02-19T20:04:27Z\", \"event\": \"Commander\", \"FID\": \"F0000000\", \"Name\": \"Dave Hedgehog\"}";
            Commander Commander = new Commander();

            EDLogCommander edLogCommander = new EDLogCommander();
            edLogCommander = JsonConvert.DeserializeObject<EDLogCommander>(entry);

            edLogCommander.ProcessEvent(ref Commander);

            var expected = "Dave Hedgehog";
            var actual = Commander.Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EDLogCommander_FID()
        {
            string entry = "{ \"timestamp\": \"2019-02-19T20:04:27Z\", \"event\": \"Commander\", \"FID\": \"F0000000\", \"Name\": \"Dave Hedgehog\"}";
            Commander Commander = new Commander();

            EDLogCommander edLogCommander = new EDLogCommander();
            edLogCommander = JsonConvert.DeserializeObject<EDLogCommander>(entry);

            edLogCommander.ProcessEvent(ref Commander);

            var expected = "F0000000";
            var actual = Commander.FID;

            Assert.Equal(expected, actual);
        }
    }

    public class LogTestsStartupClearSavedGame
    {
        [Fact]
        public void EDLogCommander_Name()
        {
            string entry = "{ \"timestamp\": \"2019-02-19T20:04:27Z\", \"event\": \"ClearSavedGame\", \"FID\": \"F0000000\", \"Name\": \"Dave Hedgehog\"}";
            Commander Commander = new Commander();

            EDLogClearSavedGame edLogClearSavedGame = new EDLogClearSavedGame();
            edLogClearSavedGame = JsonConvert.DeserializeObject<EDLogClearSavedGame>(entry);

            edLogClearSavedGame.ProcessEvent(ref Commander);

            var expected = "Dave Hedgehog";
            var actual = Commander.Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EDLogCommander_FID()
        {
            string entry = "{ \"timestamp\": \"2019-02-19T20:04:27Z\", \"event\": \"ClearSavedGame\", \"FID\": \"F0000000\", \"Name\": \"Dave Hedgehog\"}";
            Commander Commander = new Commander();

            EDLogClearSavedGame edLogClearSavedGame = new EDLogClearSavedGame();
            edLogClearSavedGame = JsonConvert.DeserializeObject<EDLogClearSavedGame>(entry);

            edLogClearSavedGame.ProcessEvent(ref Commander);

            var expected = "F0000000";
            var actual = Commander.FID;

            Assert.Equal(expected, actual);
        }
    }

    public class LogTestsStartupLoadout
    {
        [Theory]
        [InlineData("{  \"timestamp\": \"2019-02-21T21:08:37Z\",  \"event\": \"Loadout\",  \"Ship\": \"Krait_Light\",  \"ShipID\": 2,  \"ShipName\": \"Nephele\",  \"ShipIdent\": \"DH-414\",  \"HullValue\": 35818757,  \"ModulesValue\": 28150003,  \"HullHealth\": 1.0,  \"Rebuy\": 3198440,  \"Modules\": [    {      \"Slot\": \"TinyHardpoint1\",      \"Item\": \"Hpt_HeatSinkLauncher_Turret_Tiny\",      \"On\": true,      \"Priority\": 0,      \"AmmoInClip\": 1,      \"AmmoInHopper\": 2,      \"Health\": 1.0,      \"Value\": 3500    },    {      \"Slot\": \"TinyHardpoint2\",      \"Item\": \"Hpt_CloudScanner_Size0_Class3\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 121899    },    {      \"Slot\": \"Armour\",      \"Item\": \"Krait_Light_Armour_Grade1\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"PowerPlant\",      \"Item\": \"Int_Powerplant_Size4_Class5\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 1610080,      \"Engineering\": {        \"Engineer\": \"Felicity Farseer\",        \"EngineerID\": 300100,        \"BlueprintID\": 128673765,        \"BlueprintName\": \"PowerPlant_Boosted\",        \"Level\": 1,        \"Quality\": 1.0,        \"ExperimentalEffect\": \"special_powerplant_lightweight\",        \"ExperimentalEffect_Localised\": \"Stripped Down\",        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 4.5,            \"OriginalValue\": 5.0,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 83.599998,            \"OriginalValue\": 88.0,            \"LessIsGood\": 0          },          {            \"Label\": \"PowerCapacity\",            \"Value\": 17.472,            \"OriginalValue\": 15.6,            \"LessIsGood\": 0          },          {            \"Label\": \"HeatEfficiency\",            \"Value\": 0.42,            \"OriginalValue\": 0.4,            \"LessIsGood\": 1          }        ]      }    },    {      \"Slot\": \"MainEngines\",      \"Item\": \"Int_Engine_Size5_Class5\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 5103953,      \"Engineering\": {        \"Engineer\": \"Felicity Farseer\",        \"EngineerID\": 300100,        \"BlueprintID\": 128673667,        \"BlueprintName\": \"Engine_Tuned\",        \"Level\": 3,        \"Quality\": 1.0,        \"ExperimentalEffect\": \"special_engine_lightweight\",        \"ExperimentalEffect_Localised\": \"Stripped Down\",        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 18.0,            \"OriginalValue\": 20.0,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 97.520004,            \"OriginalValue\": 106.0,            \"LessIsGood\": 0          },          {            \"Label\": \"PowerDraw\",            \"Value\": 6.6096,            \"OriginalValue\": 6.12,            \"LessIsGood\": 1          },          {            \"Label\": \"EngineOptimalMass\",            \"Value\": 789.599976,            \"OriginalValue\": 840.0,            \"LessIsGood\": 0          },          {            \"Label\": \"EngineOptPerformance\",            \"Value\": 118.000008,            \"OriginalValue\": 100.0,            \"LessIsGood\": 0          },          {            \"Label\": \"EngineHeatRate\",            \"Value\": 0.78,            \"OriginalValue\": 1.3,            \"LessIsGood\": 1          }        ]      }    },    {      \"Slot\": \"FrameShiftDrive\",      \"Item\": \"Int_Hyperdrive_Size5_Class5\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 5103953,      \"Engineering\": {        \"Engineer\": \"Felicity Farseer\",        \"EngineerID\": 300100,        \"BlueprintID\": 128673694,        \"BlueprintName\": \"FSD_LongRange\",        \"Level\": 5,        \"Quality\": 1.0,        \"ExperimentalEffect\": \"special_fsd_heavy\",        \"ExperimentalEffect_Localised\": \"Mass Manager\",        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 26.0,            \"OriginalValue\": 20.0,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 93.840004,            \"OriginalValue\": 120.0,            \"LessIsGood\": 0          },          {            \"Label\": \"PowerDraw\",            \"Value\": 0.69,            \"OriginalValue\": 0.6,            \"LessIsGood\": 1          },          {            \"Label\": \"FSDOptimalMass\",            \"Value\": 1692.599976,            \"OriginalValue\": 1050.0,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"LifeSupport\",      \"Item\": \"Int_LifeSupport_Size4_Class2\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 24118    },    {      \"Slot\": \"PowerDistributor\",      \"Item\": \"Int_PowerDistributor_Size4_Class5\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 376829,      \"Engineering\": {        \"Engineer\": \"The Dweller\",        \"EngineerID\": 300180,        \"BlueprintID\": 128673741,        \"BlueprintName\": \"PowerDistributor_PriorityEngines\",        \"Level\": 2,        \"Quality\": 0.8829,        \"Modifiers\": [          {            \"Label\": \"WeaponsCapacity\",            \"Value\": 30.08,            \"OriginalValue\": 32.0,            \"LessIsGood\": 0          },          {            \"Label\": \"WeaponsRecharge\",            \"Value\": 3.43,            \"OriginalValue\": 3.5,            \"LessIsGood\": 0          },          {            \"Label\": \"EnginesCapacity\",            \"Value\": 29.651602,            \"OriginalValue\": 23.0,            \"LessIsGood\": 0          },          {            \"Label\": \"EnginesRecharge\",            \"Value\": 2.32142,            \"OriginalValue\": 1.9,            \"LessIsGood\": 0          },          {            \"Label\": \"SystemsCapacity\",            \"Value\": 21.620001,            \"OriginalValue\": 23.0,            \"LessIsGood\": 0          },          {            \"Label\": \"SystemsRecharge\",            \"Value\": 1.786,            \"OriginalValue\": 1.9,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"Radar\",      \"Item\": \"Int_Sensors_Size6_Class2\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 189078,      \"Engineering\": {        \"Engineer\": \"Felicity Farseer\",        \"EngineerID\": 300100,        \"BlueprintID\": 128740671,        \"BlueprintName\": \"Sensor_LightWeight\",        \"Level\": 3,        \"Quality\": 0.976,        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 8.0576,            \"OriginalValue\": 16.0,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 63.0,            \"OriginalValue\": 90.0,            \"LessIsGood\": 0          },          {            \"Label\": \"SensorTargetScanAngle\",            \"Value\": 25.5,            \"OriginalValue\": 30.0,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"FuelTank\",      \"Item\": \"Int_FuelTank_Size5_Class3\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 97754    },    {      \"Slot\": \"Decal2\",      \"Item\": \"Decal_PowerPlay_Sirius\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"Decal3\",      \"Item\": \"Decal_PowerPlay_Sirius\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipName0\",      \"Item\": \"Nameplate_Explorer01_White\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipName1\",      \"Item\": \"Nameplate_Explorer01_White\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipID0\",      \"Item\": \"Nameplate_ShipID_SingleLine_White\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipID1\",      \"Item\": \"Nameplate_ShipID_SingleLine_White\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"Slot01_Size6\",      \"Item\": \"Int_PassengerCabin_Size6_Class3\",      \"On\": true,      \"Priority\": 1,      \"AmmoInClip\": 12,      \"Health\": 1.0,      \"Value\": 552698    },    {      \"Slot\": \"Slot02_Size5\",      \"Item\": \"Int_DroneControl_Collection_Size3_Class5\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 86400    },    {      \"Slot\": \"Slot03_Size5\",      \"Item\": \"Int_GuardianFSDBooster_Size5\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 5510635    },    {      \"Slot\": \"Slot04_Size5\",      \"Item\": \"Int_FuelScoop_Size5_Class5\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 9073694    },    {      \"Slot\": \"Slot05_Size3\",      \"Item\": \"Int_BuggyBay_Size2_Class2\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 21600    },    {      \"Slot\": \"Slot06_Size3\",      \"Item\": \"Int_ShieldGenerator_Size3_Class2\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 18812    },    {      \"Slot\": \"Slot07_Size3\",      \"Item\": \"Int_DockingComputer_Standard\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 4500    },    {      \"Slot\": \"Slot08_Size2\",      \"Item\": \"Int_DetailedSurfaceScanner_Tiny\",      \"On\": true,      \"Priority\": 0,      \"AmmoInClip\": 3,      \"Health\": 1.0,      \"Value\": 250000,      \"Engineering\": {        \"Engineer\": \"Felicity Farseer\",        \"EngineerID\": 300100,        \"BlueprintID\": 128740149,        \"BlueprintName\": \"Sensor_Expanded\",        \"Level\": 3,        \"Quality\": 1.0,        \"Modifiers\": [          {            \"Label\": \"PowerDraw\",            \"Value\": 0.0,            \"OriginalValue\": 0.0,            \"LessIsGood\": 1          },          {            \"Label\": \"DSS_PatchRadius\",            \"Value\": 26.0,            \"OriginalValue\": 20.0,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"PlanetaryApproachSuite\",      \"Item\": \"Int_PlanetApproachSuite\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 500    },    {      \"Slot\": \"Bobble03\",      \"Item\": \"Bobble_Station_Coriolis\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"Bobble08\",      \"Item\": \"Bobble_Oldskool_CobraMkIII\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"VesselVoice\",      \"Item\": \"VoicePack_Verity\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipCockpit\",      \"Item\": \"Krait_Light_Cockpit\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"CargoHatch\",      \"Item\": \"ModularCargoBayDoor\",      \"On\": true,      \"Priority\": 2,      \"Health\": 1.0    }  ]}"
            , 0)]
        [InlineData("{  \"timestamp\": \"2019-02-20T19:47:46Z\",  \"event\": \"Loadout\",  \"Ship\": \"Anaconda\",  \"ShipID\": 2,  \"ShipName\": \"Yutani\",  \"ShipIdent\": \"0HX-DW\",  \"HullValue\": 141889432,  \"ModulesValue\": 84164627,  \"HullHealth\": 1.0,  \"Rebuy\": 8477027,  \"Modules\": [    {      \"Slot\": \"SmallHardpoint2\",      \"Item\": \"Hpt_PulseLaser_Gimbal_Small\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 6435,      \"Engineering\": {        \"Engineer\": \"The Dweller\",        \"EngineerID\": 300180,        \"BlueprintID\": 128673572,        \"BlueprintName\": \"Weapon_LightWeight\",        \"Level\": 3,        \"Quality\": 0.953,        \"ExperimentalEffect\": \"special_weapon_lightweight\",        \"ExperimentalEffect_Localised\": \"Stripped Down\",        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 0.72342,            \"OriginalValue\": 2.0,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 24.0,            \"OriginalValue\": 40.0,            \"LessIsGood\": 0          },          {            \"Label\": \"PowerDraw\",            \"Value\": 0.313833,            \"OriginalValue\": 0.39,            \"LessIsGood\": 1          },          {            \"Label\": \"DistributorDraw\",            \"Value\": 0.23281,            \"OriginalValue\": 0.31,            \"LessIsGood\": 1          }        ]      }    },    {      \"Slot\": \"TinyHardpoint1\",      \"Item\": \"Hpt_HeatSinkLauncher_Turret_Tiny\",      \"On\": true,      \"Priority\": 0,      \"AmmoInClip\": 1,      \"AmmoInHopper\": 2,      \"Health\": 1.0,      \"Value\": 3072,      \"Engineering\": {        \"Engineer\": \"Ram Tah\",        \"EngineerID\": 300110,        \"BlueprintID\": 128731472,        \"BlueprintName\": \"Misc_LightWeight\",        \"Level\": 2,        \"Quality\": 1.0,        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 0.585,            \"OriginalValue\": 1.3,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 36.0,            \"OriginalValue\": 45.0,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"TinyHardpoint2\",      \"Item\": \"Hpt_HeatSinkLauncher_Turret_Tiny\",      \"On\": false,      \"Priority\": 0,      \"AmmoInClip\": 1,      \"AmmoInHopper\": 2,      \"Health\": 1.0,      \"Value\": 3072,      \"Engineering\": {        \"Engineer\": \"Ram Tah\",        \"EngineerID\": 300110,        \"BlueprintID\": 128731472,        \"BlueprintName\": \"Misc_LightWeight\",        \"Level\": 2,        \"Quality\": 1.0,        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 0.585,            \"OriginalValue\": 1.3,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 36.0,            \"OriginalValue\": 45.0,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"TinyHardpoint6\",      \"Item\": \"Hpt_CargoScanner_Size0_Class2\",      \"On\": false,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 35656    },    {      \"Slot\": \"TinyHardpoint7\",      \"Item\": \"Hpt_CloudScanner_Size0_Class2\",      \"On\": false,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 35656    },    {      \"Slot\": \"TinyHardpoint8\",      \"Item\": \"Hpt_XenoScanner_Basic_Tiny\",      \"On\": false,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 320901    },    {      \"Slot\": \"Armour\",      \"Item\": \"Anaconda_Armour_Grade1\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Engineering\": {        \"Engineer\": \"Selene Jean\",        \"EngineerID\": 300210,        \"BlueprintID\": 128673644,        \"BlueprintName\": \"Armour_HeavyDuty\",        \"Level\": 5,        \"Quality\": 0.9,        \"ExperimentalEffect\": \"special_armour_chunky\",        \"ExperimentalEffect_Localised\": \"Deep Plating\",        \"Modifiers\": [          {            \"Label\": \"DefenceModifierHealthMultiplier\",            \"Value\": 156.258087,            \"OriginalValue\": 79.999992,            \"LessIsGood\": 0          },          {            \"Label\": \"KineticResistance\",            \"Value\": -17.543602,            \"OriginalValue\": -20.000004,            \"LessIsGood\": 0          },          {            \"Label\": \"ThermicResistance\",            \"Value\": 2.047002,            \"OriginalValue\": 0.0,            \"LessIsGood\": 0          },          {            \"Label\": \"ExplosiveResistance\",            \"Value\": -37.134182,            \"OriginalValue\": -39.999996,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"PaintJob\",      \"Item\": \"PaintJob_Anaconda_Tactical_White\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"PowerPlant\",      \"Item\": \"Int_GuardianPowerplant_Size5\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 4552857    },    {      \"Slot\": \"MainEngines\",      \"Item\": \"Int_Engine_Size5_Class5\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 5103953,      \"Engineering\": {        \"Engineer\": \"Professor Palin\",        \"EngineerID\": 300220,        \"BlueprintID\": 128673669,        \"BlueprintName\": \"Engine_Tuned\",        \"Level\": 5,        \"Quality\": 0.958,        \"ExperimentalEffect\": \"special_engine_lightweight\",        \"ExperimentalEffect_Localised\": \"Stripped Down\",        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 18.0,            \"OriginalValue\": 20.0,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 89.040001,            \"OriginalValue\": 106.0,            \"LessIsGood\": 0          },          {            \"Label\": \"PowerDraw\",            \"Value\": 7.0992,            \"OriginalValue\": 6.12,            \"LessIsGood\": 1          },          {            \"Label\": \"EngineOptimalMass\",            \"Value\": 756.0,            \"OriginalValue\": 840.0,            \"LessIsGood\": 0          },          {            \"Label\": \"EngineOptPerformance\",            \"Value\": 127.790001,            \"OriginalValue\": 100.0,            \"LessIsGood\": 0          },          {            \"Label\": \"EngineHeatRate\",            \"Value\": 0.52,            \"OriginalValue\": 1.3,            \"LessIsGood\": 1          }        ]      }    },    {      \"Slot\": \"FrameShiftDrive\",      \"Item\": \"Int_Hyperdrive_Size6_Class5\",      \"On\": true,      \"Priority\": 0,      \"Health\": 0.90249,      \"Value\": 16179531,      \"Engineering\": {        \"Engineer\": \"Felicity Farseer\",        \"EngineerID\": 300100,        \"BlueprintID\": 128673694,        \"BlueprintName\": \"FSD_LongRange\",        \"Level\": 5,        \"Quality\": 1.0,        \"ExperimentalEffect\": \"special_fsd_heavy\",        \"ExperimentalEffect_Localised\": \"Mass Manager\",        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 52.0,            \"OriginalValue\": 40.0,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 110.262009,            \"OriginalValue\": 141.0,            \"LessIsGood\": 0          },          {            \"Label\": \"PowerDraw\",            \"Value\": 0.8625,            \"OriginalValue\": 0.75,            \"LessIsGood\": 1          },          {            \"Label\": \"FSDOptimalMass\",            \"Value\": 2901.599854,            \"OriginalValue\": 1800.0,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"LifeSupport\",      \"Item\": \"Int_LifeSupport_Size5_Class2\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 69713,      \"Engineering\": {        \"Engineer\": \"Bill Turner\",        \"EngineerID\": 300010,        \"BlueprintID\": 128731493,        \"BlueprintName\": \"Misc_LightWeight\",        \"Level\": 3,        \"Quality\": 0.98,        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 2.816,            \"OriginalValue\": 8.0,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 60.200001,            \"OriginalValue\": 86.0,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"PowerDistributor\",      \"Item\": \"Int_PowerDistributor_Size5_Class5\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 1089257,      \"Engineering\": {        \"Engineer\": \"The Dweller\",        \"EngineerID\": 300180,        \"BlueprintID\": 128673743,        \"BlueprintName\": \"PowerDistributor_PriorityEngines\",        \"Level\": 4,        \"Quality\": 1.0,        \"ExperimentalEffect\": \"special_powerdistributor_lightweight\",        \"ExperimentalEffect_Localised\": \"Stripped Down\",        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 18.0,            \"OriginalValue\": 20.0,            \"LessIsGood\": 1          },          {            \"Label\": \"WeaponsCapacity\",            \"Value\": 36.079998,            \"OriginalValue\": 41.0,            \"LessIsGood\": 0          },          {            \"Label\": \"WeaponsRecharge\",            \"Value\": 4.128,            \"OriginalValue\": 4.3,            \"LessIsGood\": 0          },          {            \"Label\": \"EnginesCapacity\",            \"Value\": 43.5,            \"OriginalValue\": 29.0,            \"LessIsGood\": 0          },          {            \"Label\": \"EnginesRecharge\",            \"Value\": 3.425,            \"OriginalValue\": 2.5,            \"LessIsGood\": 0          },          {            \"Label\": \"SystemsCapacity\",            \"Value\": 25.52,            \"OriginalValue\": 29.0,            \"LessIsGood\": 0          },          {            \"Label\": \"SystemsRecharge\",            \"Value\": 2.2,            \"OriginalValue\": 2.5,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"Radar\",      \"Item\": \"Int_Sensors_Size8_Class2\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 1530326,      \"Engineering\": {        \"Engineer\": \"Bill Turner\",        \"EngineerID\": 300010,        \"BlueprintID\": 128740673,        \"BlueprintName\": \"Sensor_LightWeight\",        \"Level\": 5,        \"Quality\": 1.0,        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 12.799999,            \"OriginalValue\": 64.0,            \"LessIsGood\": 1          },          {            \"Label\": \"Integrity\",            \"Value\": 60.0,            \"OriginalValue\": 120.0,            \"LessIsGood\": 0          },          {            \"Label\": \"SensorTargetScanAngle\",            \"Value\": 22.5,            \"OriginalValue\": 30.0,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"FuelTank\",      \"Item\": \"Int_FuelTank_Size5_Class3\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 97754    },    {      \"Slot\": \"Decal2\",      \"Item\": \"Decal_Cannon\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"Decal3\",      \"Item\": \"Decal_Cannon\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipName0\",      \"Item\": \"Nameplate_Expedition02_White\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipName1\",      \"Item\": \"Nameplate_Expedition02_White\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipID0\",      \"Item\": \"Nameplate_ShipID_Black\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipID1\",      \"Item\": \"Nameplate_ShipID_Black\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"Slot01_Size7\",      \"Item\": \"Int_FuelScoop_Size7_Class4\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 20002754    },    {      \"Slot\": \"Slot02_Size6\",      \"Item\": \"Int_Repairer_Size6_Class5\",      \"On\": false,      \"Priority\": 0,      \"AmmoInClip\": 8100,      \"Health\": 1.0,      \"Value\": 13430578    },    {      \"Slot\": \"Slot03_Size6\",      \"Item\": \"Int_Repairer_Size6_Class5\",      \"On\": false,      \"Priority\": 0,      \"AmmoInClip\": 8100,      \"Health\": 1.0,      \"Value\": 13430578    },    {      \"Slot\": \"Slot04_Size6\",      \"Item\": \"Int_BuggyBay_Size6_Class2\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 606528    },    {      \"Slot\": \"Slot05_Size5\",      \"Item\": \"Int_FuelTank_Size5_Class3\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 85780    },    {      \"Slot\": \"Slot06_Size5\",      \"Item\": \"Int_GuardianFSDBooster_Size5\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 5688921    },    {      \"Slot\": \"Slot07_Size5\",      \"Item\": \"Int_DroneControl_Repair_Size1_Class2\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 1053    },    {      \"Slot\": \"Slot08_Size4\",      \"Item\": \"Int_ShieldGenerator_Size4_Class2\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 52329,      \"Engineering\": {        \"Engineer\": \"Lei Cheung\",        \"EngineerID\": 300120,        \"BlueprintID\": 128673838,        \"BlueprintName\": \"ShieldGenerator_Reinforced\",        \"Level\": 4,        \"Quality\": 0.97,        \"ExperimentalEffect\": \"special_shield_lightweight\",        \"ExperimentalEffect_Localised\": \"Stripped Down\",        \"Modifiers\": [          {            \"Label\": \"Mass\",            \"Value\": 3.6,            \"OriginalValue\": 4.0,            \"LessIsGood\": 1          },          {            \"Label\": \"ShieldGenStrength\",            \"Value\": 118.637993,            \"OriginalValue\": 90.0,            \"LessIsGood\": 0          },          {            \"Label\": \"BrokenRegenRate\",            \"Value\": 2.277,            \"OriginalValue\": 2.53,            \"LessIsGood\": 0          },          {            \"Label\": \"EnergyPerRegen\",            \"Value\": 0.66,            \"OriginalValue\": 0.6,            \"LessIsGood\": 1          },          {            \"Label\": \"KineticResistance\",            \"Value\": 48.099995,            \"OriginalValue\": 39.999996,            \"LessIsGood\": 0          },          {            \"Label\": \"ThermicResistance\",            \"Value\": -3.800011,            \"OriginalValue\": -20.000004,            \"LessIsGood\": 0          },          {            \"Label\": \"ExplosiveResistance\",            \"Value\": 56.75,            \"OriginalValue\": 50.0,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"Slot09_Size4\",      \"Item\": \"Int_CorrosionProofCargoRack_Size4_Class1\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 82774    },    {      \"Slot\": \"Slot10_Size4\",      \"Item\": \"Int_DetailedSurfaceScanner_Tiny\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 219375,      \"Engineering\": {        \"Engineer\": \"Bill Turner\",        \"EngineerID\": 300010,        \"BlueprintID\": 128740146,        \"BlueprintName\": \"Sensor_Expanded\",        \"Level\": 5,        \"Quality\": 0.3543,        \"Modifiers\": [          {            \"Label\": \"PowerDraw\",            \"Value\": 0.0,            \"OriginalValue\": 0.0,            \"LessIsGood\": 1          },          {            \"Label\": \"DSS_PatchRadius\",            \"Value\": 28.708,            \"OriginalValue\": 20.0,            \"LessIsGood\": 0          }        ]      }    },    {      \"Slot\": \"Slot13_Size2\",      \"Item\": \"Int_DroneControl_UnkVesselResearch\",      \"On\": true,      \"Priority\": 0,      \"Health\": 1.0,      \"Value\": 1535274    },    {      \"Slot\": \"PlanetaryApproachSuite\",      \"Item\": \"Int_PlanetApproachSuite\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0,      \"Value\": 500    },    {      \"Slot\": \"Bobble01\",      \"Item\": \"Bobble_Planet_Earth\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"Bobble03\",      \"Item\": \"Bobble_Nav_Beacon\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"Bobble08\",      \"Item\": \"Bobble_Ship_CobraMkIII\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"Bobble10\",      \"Item\": \"Bobble_Station_Coriolis\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipKitSpoiler\",      \"Item\": \"Anaconda_Shipkit1_Spoiler4\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipKitBumper\",      \"Item\": \"Anaconda_Shipkit1_Bumper1\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipKitTail\",      \"Item\": \"Anaconda_Shipkit1_Tail4\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"VesselVoice\",      \"Item\": \"VoicePack_Verity\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"ShipCockpit\",      \"Item\": \"Anaconda_Cockpit\",      \"On\": true,      \"Priority\": 1,      \"Health\": 1.0    },    {      \"Slot\": \"CargoHatch\",      \"Item\": \"ModularCargoBayDoor\",      \"On\": true,      \"Priority\": 2,      \"Health\": 1.0    }  ]}"
            , 16)]

        public void EDLogLoadout_CargoCapacity(string entry, int expected)
        {
            Commander Commander = new Commander();

            EDLogLoadout edLogLoadout = new EDLogLoadout();
            edLogLoadout = JsonConvert.DeserializeObject<EDLogLoadout>(entry);

            edLogLoadout.ProcessEvent(ref Commander);

            var actual = Commander.CargoSpace;

            Assert.Equal(expected, actual);
        }
    }
}
