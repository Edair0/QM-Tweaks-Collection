using HarmonyLib;
using MGSC;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using static MGSC.ConsoleDaemon;

namespace edair_mod_inventory
{
    public static class Plugin
    {
        public static string ModAssemblyName => Assembly.GetExecutingAssembly().GetName().Name;
        public static string ConfigPath => Path.Combine(Application.persistentDataPath + "\\edair_mod", Assembly.GetExecutingAssembly().GetName().Name) + ".json";
        public static string ModPersistenceFolder => Path.Combine(Application.persistentDataPath, Assembly.GetExecutingAssembly().GetName().Name);

        public static ModConfig Config { get; set; }

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            Config = ModConfigSerializer.LoadConfig(ConfigPath);
            new Harmony("Edair0_" + ModAssemblyName).PatchAll();
        }
    }
}
