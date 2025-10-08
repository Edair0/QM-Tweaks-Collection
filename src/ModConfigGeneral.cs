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
        public const string HeaderGeneral = "general";
        public const string HeaderVest = "vest";
        public const string HeaderHearing = "hearing";

		public const string general_weight_mult = "general_weight_mult";
        public const string general_satiety_mult = "general_satiety_mult";
        public const string vest_slots_flat = "vest_slots_flat";
        public const string vest_slots_mult = "vest_slots_mult";
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
            
            this.ModData.AddConfigHeader(Keys.HeaderGeneral, "General Settings");
			this.ModData.AddConfigValue(Keys.HeaderGeneral, Keys.general_weight_mult, "Global Weight Multiplier %", "A global multiplier to total character weight (in percent).\n0 = Disables weight completely, load is always 0KG.\n50 = Everything weights half as much as normal.\n100 = Normal weight.\n200 = Everything weights two times more.", 100, 0, 200);
			this.ModData.AddConfigValue(Keys.HeaderGeneral, Keys.general_satiety_mult, "Global Satiety Drain %", "A global multiplier to character satiety drain (in percent).\n0 = Disables drain completely.\n50 = Halves satiety drain.\n100 = Normal satiety drain.\n200 = Double satiety drain.", 100, 0, 200);
			
            this.ModData.AddConfigHeader(Keys.HeaderVest, "Vest Settings");
			this.ModData.AddConfigValue(Keys.HeaderVest, Keys.vest_slots_flat, "Flat Increase to Vest Slots", "A flat increase to the number of slots in vests.", 0, 0, 5);
			this.ModData.AddConfigValue(Keys.HeaderVest, Keys.vest_slots_mult, "Multiplier to Vest Slots", "A multiplier to the number of slots in vests.", 1, 1, 5);
			
            this.ModData.AddConfigHeader(Keys.HeaderHearing, "Hearing Settings");
			this.ModData.AddConfigValue(Keys.HeaderHearing, Keys.hearing_enabled, "Hearing Always Active", "When enabled the hearing effect is active even when walking and running.\nWounds can still disable hearing.", false);
			this.ModData.AddConfigValue(Keys.HeaderHearing, Keys.hearing_range_modifier, "Neural Sonar Range", "Modifies the radius of the hearing effect.\nDefault hearing radius is around 6 tiles.", 0, -5, 5);
			
            this.ModData.RegisterModConfigData(this.ModName, "QM Tweaks Collection");
		}
    }

}
