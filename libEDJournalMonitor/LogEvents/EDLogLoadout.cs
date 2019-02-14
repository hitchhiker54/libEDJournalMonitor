using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace libEDJournalMonitor
{
    sealed public class EDLogLoadout : EDLogEntry
    {
        public class EDLogModule
        {
            [JsonProperty("Slot")]
            public string Slot { get; set; }
            [JsonProperty("Item")]
            public string Item { get; set; }
            [JsonProperty("On")]
            public bool On { get; set; }
            [JsonProperty("Priority")]
            public int Priority { get; set; }
            [JsonProperty("Health")]
            public float Health { get; set; }
            [JsonProperty("Value")]
            public int Value { get; set; }
            [JsonProperty("Engineering")]
            public EDLogModuleEngineering Engineering { get; set; }
        }

        public class EDLogModuleEngineering
        {
            [JsonProperty("Engineer")]
            public string Engineer { get; set; }
            [JsonProperty("EngineerID")]
            public int EngineerID { get; set; }
            [JsonProperty("BlueprintID")]
            public int BlueprintID { get; set; }
            [JsonProperty("BlueprintName")]
            public string BlueprintName { get; set; }
            [JsonProperty("Level")]
            public int Level { get; set; }
            [JsonProperty("Quality")]
            public float Quality { get; set; }
            [JsonProperty("ExperimantalEffect")]
            public string ExperimantalEffect { get; set; }
            [JsonProperty("ExperimantalEffect_Localised")]
            public string ExperimantalEffect_Localised { get; set; }
            [JsonProperty("Modifiers")]
            public EDLogModuleModifiers[] Modifiers { get; set; }
        }

        public class EDLogModuleModifiers
        {
            [JsonProperty("Label")]
            public string Label { get; set; }
            [JsonProperty("Value")]
            public float? Value { get; set; }
            [JsonProperty("OriginalValue")]
            public float? OriginalValue { get; set; }
            [JsonProperty("LessIsGood")]
            public bool? LessIsGood { get; set; }
        }

        [JsonProperty("Ship")]
        public string Ship { get; set; }
        [JsonProperty("ShipID")]
        public int ShipID { get; set; }
        [JsonProperty("ShipName")]
        public string ShipName { get; set; }
        [JsonProperty("ShipIdent")]
        public string ShipIdent { get; set; }
        [JsonProperty("HullValue")]
        public int HullValue { get; set; }
        [JsonProperty("ModulesValue")]
        public int ModulesValue { get; set; }
        [JsonProperty("HullHealth")]
        public float HullHealth { get; set; }
        [JsonProperty("Rebuy")]
        public int Rebuy { get; set; }
        [JsonProperty("Hot")]
        public bool Hot { get; set; }
        [JsonProperty("Modules")]
        public EDLogModule[] Modules { get; set; }

        public override void ProcessEvent(ref Commander Commander)
        {
            Commander.HullValue = HullValue;
            Commander.ModulesValue = ModulesValue;
            Commander.HullHealth = HullHealth;
            Commander.Rebuy = Rebuy;
            Commander.Hot = Hot;

            Commander.CargoSpace = 0;
            foreach (var module in Modules)
            {
                if (module.Item.Contains("CargoRack"))
                {
                    if (module.Item.Contains("Size1"))
                    {
                        Commander.CargoSpace += 2;
                    }
                    else if (module.Item.Contains("Size2"))
                    {
                        Commander.CargoSpace += 4;
                    }
                    else if (module.Item.Contains("Size3"))
                    {
                        Commander.CargoSpace += 8;
                    }
                    else if (module.Item.Contains("Size4"))
                    {
                        Commander.CargoSpace += 16;
                    }
                    else if (module.Item.Contains("Size5"))
                    {
                        Commander.CargoSpace += 32;
                    }
                    else if (module.Item.Contains("Size6"))
                    {
                        Commander.CargoSpace += 64;
                    }
                    else if (module.Item.Contains("Size7"))
                    {
                        Commander.CargoSpace += 128;
                    }
                    else if (module.Item.Contains("Size8"))
                    {
                        Commander.CargoSpace += 256;
                    }
                }
            }
        }
    }
}
