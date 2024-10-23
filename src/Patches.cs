using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace edair_mod_inventory
{
    [HarmonyPatch(typeof(MainMenuScreen), nameof(MainMenuScreen.Awake))]
    public static class PatchMainMenu
    {
        public static void Postfix()
        {
            Plugin.Config = ModConfigSerializer.ReloadConfig(Plugin.Config, Plugin.ConfigPath);
        }
    }

    [HarmonyPatch(typeof(BackpackRecord), "get_Height")]
    public static class PatchBackpack
    {
        private static void Postfix(ref int __result)
        {
            __result = Mathf.RoundToInt(__result * Plugin.Config.bp_slots_mult) + Plugin.Config.bp_slots_flat;
            __result = Math.Max(__result, 1);
        }
    }

    [HarmonyPatch(typeof(BackpackRecord), "get_TotalItemsWeightMult")]
    public static class PatchBackpackMult
    {
        private static void Postfix(ref float __result)
        {

            __result = Mathf.Max(__result - Plugin.Config.bp_weight_flat * 0.01f, 0.1f);
            __result = Mathf.Min(__result, 1.0f);
            //DevConsoleUI.Instance.PrintText("Value:" + __result.ToString());
        }
    }

    [HarmonyPatch(typeof(VestRecord), "get_SlotCapacity")]
    public static class PatchVest
    {
        private static void Postfix(ref int __result)
        {
            __result = Mathf.RoundToInt(__result * Plugin.Config.vest_slots_mult);
            __result = Math.Min(__result + Plugin.Config.vest_slots_flat, 6);
            __result = Math.Max(__result, 1);
        }
    }

    [HarmonyPatch(typeof(Mercenary), "GetItemsWeightSatietyDrain")]
    public static class PatchSatietyDrain
    {
        private static void Postfix(ref float __result, Mercenary __instance)
        {
            __result = -Mathf.RoundToInt(__instance.GetItemsWeight() / Plugin.Config.satiety_kg);
        }
    }
}
