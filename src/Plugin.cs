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

            AddCommand(typeof(ModCommand), ModCommand.CommandName);
        }

        public static void AddCommand(Type commandType, string name)
        {
            ConsoleDaemon consoleDaemon = ConsoleDaemon.Instance;
            if (ConsoleDaemon.Instance == null) return;

            MethodInfo helpMethod = commandType.GetMethod("Help", BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Standard, new Type[2] { typeof(string), typeof(bool) }, null);
            MethodInfo fetchAutocompleteMethod = commandType.GetMethod("FetchAutocompleteOptions", BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Standard, new Type[2] { typeof(string), typeof(string[]) }, null);
            if (helpMethod == null && fetchAutocompleteMethod == null) return;

            MethodInfo isAvailableMethod = commandType.GetMethod("IsAvailable", BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Standard, new Type[0], null);
            MethodInfo showInHelpAndAutocompleteMethod = commandType.GetMethod("ShowInHelpAndAutocomplete", BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Standard, new Type[0], null);

            consoleDaemon.Commands[name.ToLower()] = new CommandInterface(commandType, consoleDaemon.Resolve, helpMethod, fetchAutocompleteMethod, isAvailableMethod, showInHelpAndAutocompleteMethod);
            consoleDaemon.AlternateCommandNames[name] = name;
        }
    }
}
