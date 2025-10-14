using MGSC;
using ModConfigMenu;
using ModConfigMenu.Objects;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EdairTweaks
{
    public static partial class Keys
    {
        public const string HeaderCharacter = "character";
        public const string general_weight_mult = "general_weight_mult";
        public const string general_satiety_mult = "general_satiety_mult";
        public const string hearing_enabled = "hearing_enabled";
        public const string hearing_range_stealth = "hearing_range_stealth";
        public const string hearing_range_walk = "hearing_range_walk";
        public const string hearing_range_run = "hearing_range_run";

        public const string HeaderInventory = "inventory";
        public const string vest_slots_flat = "vest_slots_flat";
        public const string general_capsule_mult = "general_capsule_mult";

        public const string HeaderQuest = "quest";
        public const string escort_tough = "escort_tough";
        public const string ritual_moretime = "ritual_moretime";

        public const string HeaderOther = "other";
        public const string amputation_chance = "amputation_chance";
    }


    public class ModConfigGeneral
    {
        private string ModName;
		public ModConfigData ModData;

        public ModConfigGeneral(string ModName, string ConfigPath)
        {
            this.ModName = ModName;
			this.ModData = new ModConfigData(ConfigPath);

            // ======================================== Character Settings ========================================
            this.ModData.AddConfigHeader("STRING:Character Settings", Keys.HeaderCharacter);
            
            this.ModData.AddConfigValue(Keys.HeaderCharacter, Keys.general_weight_mult, defaultValue: 100, min: 10, max: 200, 
                labelKey: "STRING:Global Weight",
                tooltipKey: "STRING:A global multiplier for the player character’s carried weight (in percent).\n\n" +
                "0     = No weight (0 KG)\n" +
                "50   = Player carries items at half their normal weight\n" +
                "100 = Normal weight\n" +
                "200 = Player carries items at double weight");
			
            this.ModData.AddConfigValue(Keys.HeaderCharacter, Keys.general_satiety_mult, defaultValue: 100, min: 10, max: 200,
                labelKey: "STRING:Global Satiety Drain",
                tooltipKey: "STRING:A global multiplier for the player character’s satiety drain rate (in percent).\n\n" +
                "0     = No satiety drain\n" +
                "50   = Half the normal drain\n" +
                "100 = Normal drain\n" +
                "200 = Double drain");

            this.ModData.AddConfigValue(Keys.HeaderCharacter, Keys.hearing_enabled, defaultValue: false,
                labelKey: "STRING:Custom Hearing Settings",
                tooltipKey: "STRING:Allows modifying hearing ranges for each movement mode.\n\n" +
                "ON  = Uses custom settings below\n" +
                "OFF = Uses default settings");

            this.ModData.AddConfigValue(Keys.HeaderCharacter, Keys.hearing_range_stealth, defaultValue: 6, min: 0, max: 16,
                labelKey: "STRING:Hearing Range - Stealth",
                tooltipKey: "STRING:Modifies the effective radius of the player’s hearing while in stealth mode.\n\n" +
                "Default radius: 6 tiles");

            this.ModData.AddConfigValue(Keys.HeaderCharacter, Keys.hearing_range_walk, defaultValue: 0, min: 0, max: 16,
                labelKey: "STRING:Hearing Range - Walk",
                tooltipKey: "STRING:Modifies the effective radius of the player’s hearing while in walk mode.\n\n" +
                "Default radius: 0 tiles - OFF");

            this.ModData.AddConfigValue(Keys.HeaderCharacter, Keys.hearing_range_run, defaultValue: 0, min: 0, max: 16,
                labelKey: "STRING:Hearing Range - Run",
                tooltipKey: "STRING:Modifies the effective radius of the player’s hearing while in run mode.\n\n" +
                "Default radius: 0 tiles - OFF");

            // ======================================== Inventory Settings ========================================
            this.ModData.AddConfigHeader("STRING:Inventory Settings", Keys.HeaderInventory);

            this.ModData.AddConfigValue(Keys.HeaderInventory, Keys.vest_slots_flat, defaultValue: 0, min: 0, max: 8,
                labelKey: "STRING:Vest Slot Bonus",
                tooltipKey: "STRING:Adds a flat number of extra slots to vests.\nThe maximum possible total is 8 slots.");
            
            this.ModData.AddConfigValue(Keys.HeaderInventory, Keys.general_capsule_mult, defaultValue: 1, min: 1, max: 4,
                labelKey: "STRING:Capsule Storage Multiplier",
                tooltipKey: $"STRING:Multiplies the capsule’s inventory capacity.\n\n" +
                "1 = Normal capacity\n" +
                "2 = Double capacity\n" +
                "3 = Triple capacity");

            // ======================================== Quest Settings ========================================
            this.ModData.AddConfigHeader("STRING:Quest Settings", Keys.HeaderQuest);

            this.ModData.AddConfigValue(Keys.HeaderQuest, Keys.escort_tough, defaultValue: false,
                labelKey: "STRING:Tough VIPs",
                tooltipKey: "STRING:Increases the durability of VIPs during escort missions.\n\n" +
                "   3x More HP\n" +
                "   1.5x Dodge\n" +
                "   1.5x Resists\n" +
                "   5 HP Regen");

            this.ModData.AddConfigValue(Keys.HeaderQuest, Keys.ritual_moretime, defaultValue: false,
                labelKey: "STRING:Extended Ritual Timer",
                tooltipKey: "STRING:Multiplies the turn limit in ritual missions, giving 5× more time to complete them.");

            // ======================================== Other Settings ========================================
            this.ModData.AddConfigHeader("STRING:Other Settings", Keys.HeaderOther);

            this.ModData.AddConfigValue(Keys.HeaderOther, Keys.amputation_chance, defaultValue: 30, min: 0, max: 100,
                labelKey: "STRING:Implant Amputation Chance",
                tooltipKey: $"STRING:Chance to obtain an implant from an amputated part of a corpse.\n\n" +
                "0% = No chance\n" +
                "30% = Default chance\n" +
                "100% = Guaranteed");

            this.ModData.RegisterModConfigData(this.ModName, "QM Tweaks Collection");
		}
    }

}
