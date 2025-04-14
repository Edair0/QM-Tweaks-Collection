using MGSC;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading;
using UnityEngine;

namespace edair_mod_inventory
{

    [ConsoleCommand(new string[] { CommandName })]
    public class ModCommand
    {
        public const string CommandName = "mod_inventory";
        public static List<string> SettingsList { get; set; } = new List<string>() { "show", "reload", "default", "vanilla", "bp_slots_flat", "bp_slots_mult", "bp_weight_flat", "vest_slots_flat", "vest_slots_mult", "satiety_kg" };

        public static string Help(string command, bool verbose)
        {
            if (!verbose) { return $"Modifies settings of inventory mod. Use help {CommandName} for usage."; }
            return  $"Usage: {CommandName} show - Displays current mod settings.\n" +
                    $"Usage: {CommandName} reload - Use to manually reload config file.\n" +
                    $"Usage: {CommandName} default - Changes all settings to default values.\n" +
                    $"Usage: {CommandName} vanilla - Changes all settings to vanilla values.\n" +
                    $"Usage: {CommandName} <setting> <value> - Modifies mod setting.\n" +
                    $"Following can be used as <setting>:\n" +
                    $"  bp_slots_flat     - Add or remove flat amount of slot rows to all backpacks.\n" +
                    $"  bp_slots_mult     - Multiply slot rows of all backpacks.\n" +
                    $"  bp_weight_flat    - Increase or decrease weight reduction % of all backpacks by flat amount.\n" +
                    $"  vest_slots_flat   - Add or remove flat amount of slots to all vests.\n" +
                    $"  vest_slots_mult   - Multiply slots of all vests.\n" +
                    $"  satiety_kg        - Set how many KG of weight are needed for 1 satiety drain.\n";

        }

        public string Execute(string[] tokens)
        {  
            if (tokens.Length == 0) { return Help(CommandName, true); }
            else if (tokens.Length == 1)
            {
                switch (tokens[0])
                {
                    case "show":
                        string result = string.Empty;
                        result += $"bp_slots_flat = {Plugin.Config.bp_slots_flat} [Mod default = 0, Vanilla = 0]\n";
                        result += $"bp_slots_mult = {Plugin.Config.bp_slots_mult} [Mod default = 2, Vanilla = 1]\n";
                        result += $"bp_weight_flat = {Plugin.Config.bp_weight_flat} [Mod default = 35, Vanilla = 0]\n";
                        result += $"vest_slots_flat = {Plugin.Config.vest_slots_flat} [Mod default = 0, Vanilla = 0]\n";
                        result += $"vest_slots_mult = {Plugin.Config.vest_slots_mult} [Mod default = 2, Vanilla = 1]\n";
                        result += $"satiety_kg = {Plugin.Config.satiety_kg} [Mod default = 50, Vanilla = 10]\n";
                        return result;
                    case "reload": 
                        Plugin.Config = ModConfigSerializer.ReloadConfig(Plugin.Config, Plugin.ConfigPath);
                        return "Reloaded config file.";
                    case "default":
                        Plugin.Config.SetDefaults(true);
                        return "Restored config file to default values.";
                    case "vanilla":
                        Plugin.Config.SetVanilla(true);
                        return "Restored config file to vanilla values.";
                    default: return "Incorrect <value> parameter.";
                }
            }
            else if (tokens.Length == 2) 
            {
                string result = string.Empty;
                switch (tokens[0])
                {
                    case "bp_slots_flat": result = Plugin.Config.TryChangeSettingInt(ref Plugin.Config.bp_slots_flat, tokens[0], tokens[1]); break;
                    case "bp_slots_mult": result = Plugin.Config.TryChangeSettingFloat(ref Plugin.Config.bp_slots_mult, tokens[0], tokens[1]); break;
                    case "bp_weight_flat": result = Plugin.Config.TryChangeSettingInt(ref Plugin.Config.bp_weight_flat, tokens[0], tokens[1]); break;
                    case "vest_slots_flat": result = Plugin.Config.TryChangeSettingInt(ref Plugin.Config.vest_slots_flat, tokens[0], tokens[1]); break;
                    case "vest_slots_mult": result = Plugin.Config.TryChangeSettingFloat(ref Plugin.Config.vest_slots_mult, tokens[0], tokens[1]); break;
                    case "satiety_kg": result = Plugin.Config.TryChangeSettingInt(ref Plugin.Config.satiety_kg, tokens[0], tokens[1]); break;
                    default: result = "Incorrect <value> parameter." ; break;
                }
                return result;
            }
            return $"Too many parameters used. Use help {CommandName} for command usage information.";
        }
        public static List<string> FetchAutocompleteOptions(string command, string[] tokens)
        {
            string enteredText = ((tokens.Length != 0) ? tokens[0] : "");
            List<string> list = SettingsList.Where((string name) => name.StartsWith(enteredText)).ToList<string>();
            if (list == null || list.Count == 0) { return null; }
            List<string> list2 = new List<string>();
            foreach (string text in list) { list2.Add(command + " " + text); }
            return list2;
        }

        public static bool IsAvailable()
        {
            return true;
        }

        public static bool ShowInHelpAndAutocomplete()
        {
            return true;
        }
    }
}
