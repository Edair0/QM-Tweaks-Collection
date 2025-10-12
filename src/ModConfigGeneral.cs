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
        public const string HeaderInventory = "inventory";
        public const string HeaderQuest = "quest";

        public const string general_weight_mult = "general_weight_mult";
        public const string general_satiety_mult = "general_satiety_mult";
        public const string general_capsule_mult = "general_capsule_mult";
        public const string vest_slots_flat = "vest_slots_flat";
        public const string vest_slots_mult = "vest_slots_mult";

        public const string escort_tough = "escort_tough";
        public const string ritual_moretime = "ritual_moretime";
        public const string hearing_enabled = "sonar_enabled";
        public const string hearing_range_modifier = "sonar_range";
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
                "0     = No satiety drain.\n" +
                "50   = Half the normal drain\n" +
                "100 = Normal drain\n" +
                "200 = Double drain");
            
            this.ModData.AddConfigValue(Keys.HeaderCharacter, Keys.hearing_enabled, defaultValue: false,
                labelKey: "STRING:Hearing Always Active",
                tooltipKey: "STRING:When enabled, the hearing effect remains active while walking and running. Certain wounds can still disable it.");
            
            this.ModData.AddConfigValue(Keys.HeaderCharacter, Keys.hearing_range_modifier, defaultValue: 0, min: - 6, max: 6,
                labelKey: "STRING:Hearing Range",
                tooltipKey: "STRING:Modifies the effective radius of the player’s hearing (in tiles).\n" +
                "Default radius: ~6 tiles.");

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
			
            this.ModData.RegisterModConfigData(this.ModName, "QM Tweaks Collection");
		}
    }

}
