using HarmonyLib;
using MGSC;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using UnityEngine;
using static MGSC.ConsoleDaemon;

namespace EdairTweaks
{
    public static class Plugin
    {
        public static string ModAssemblyName => Assembly.GetExecutingAssembly().GetName().Name;
        private static string ModPersistenceFolder => Path.Combine($"{Application.persistentDataPath}/../Quasimorph_ModConfigs", ModAssemblyName);
        private static string ConfigPath => Path.Combine(ModPersistenceFolder, "config.txt");
        private static string PresetPath => Path.Combine(ModPersistenceFolder, "preset.txt");

        public static ModConfigGeneral ConfigGeneral { get; set; }
        public static ModConfigPreset ConfigPreset { get; set; }

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            LocalizationHelper.AddKeyToAllDictionaries("ui.empty.tooltip", " ");
            //File.WriteAllLines($"{ModPersistenceFolder}/eng.txt", Localization.Instance.currentDict.Select(entry => $"{entry.Key}={entry.Value}"));

            ConfigGeneral = new ModConfigGeneral("general", ConfigPath);
            ConfigPreset = new ModConfigPreset("preset", PresetPath);   
            new Harmony("Edair0_" + ModAssemblyName).PatchAll();

        }
    }
}
