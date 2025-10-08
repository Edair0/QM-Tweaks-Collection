using HarmonyLib;
using MGSC;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MGSC.BinaryPresetsMap;

namespace EdairTweaks
{
    [HarmonyPatch(typeof(VestRecord), "get_SlotCapacity")]
    public static class PatchVest
    {
        private static void Postfix(ref int __result)
        {
            __result = Mathf.RoundToInt(__result * Plugin.ConfigGeneral.ModData.GetConfigValue<int>(Keys.vest_slots_mult));
            __result = Math.Min(__result + Plugin.ConfigGeneral.ModData.GetConfigValue<int>(Keys.vest_slots_flat), 8);
            __result = Math.Max(__result, 1);
        }
    }

    [HarmonyPatch(typeof(CreatureData), nameof(CreatureData.GetItemsWeight))]
    public static class PatchCreatureItemWeight
    {
        private static void Postfix(ref float __result)
        {
            __result = Mathf.Max(0f, __result * (Plugin.ConfigGeneral.ModData.GetConfigValue<int>(Keys.general_weight_mult) / 100f));
        }
    }

    [HarmonyPatch(typeof(CreatureData), nameof(CreatureData.GetItemsWeightSatietyDrain))]
    public static class PatchSatietyDrain
    {
        private static void Postfix(ref float __result)
        {
            __result = Mathf.Min(0f, __result * (Plugin.ConfigGeneral.ModData.GetConfigValue<int>(Keys.general_satiety_mult) / 100f));
        }
    }

    [HarmonyPatch(typeof(GlobalSettings), "get_NotifyHiddenEnemiesRadius")]
    public static class PatchPlayerSonar
    {
        public static void Postfix(ref int __result)
        {
            if (Plugin.ConfigGeneral.ModData.GetConfigValue<bool>(Keys.hearing_enabled))
            {
                __result += Plugin.ConfigGeneral.ModData.GetConfigValue<int>(Keys.hearing_range_modifier);
            }
        }
    }

    [HarmonyPatch(typeof(Player), nameof(Player.IsAbleToSpotAnEnemy))]
    public static class PatchPlayerSpotEnemy
    {
        public static void Postfix(Player __instance, ref bool __result)
        {
            if (Plugin.ConfigGeneral.ModData.GetConfigValue<bool>(Keys.hearing_enabled) && __result == false)
            {
                if (!__instance.CreatureData.EffectsController.HasAnyEffect<WoundEffectNoSpottedSignal>())
                {
                    __result = true;
                }
            }
        }
    }

    [HarmonyPatch(typeof(DifficultyScreen), nameof(DifficultyScreen.Configure))]

    public static class PatchDifficultyPresets
    {
        public static void Prefix(CustomDifficultyScreen __instance)
        {
            if (!Data.DifficultyPresets.ContainsKey("Personal"))
            {
                Data.DifficultyPresets.Add("Personal", CloneHelper.DeepClone<DifficultyPreset>(Data.DifficultyPresets[Data.Global.DefaultDifficulty]));
                Data.DifficultyPresets["Personal"].Id = "Personal";
                LocalizationHelper.AddKeyToAllDictionaries("ui.difficulty.Personal.name", "Personal");
                LocalizationHelper.AddKeyToAllDictionaries("ui.difficulty.Personal.desc", "PERSONAL GAME DIFFICULTY\n\nBASED ON MOD SETTINGS");
            }
            DifficultyPreset CustomDifficulty = Data.DifficultyPresets["Personal"];
            // Start
            CustomDifficulty.Tutorial = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_Tutorial);
            CustomDifficulty.SmoothProgression = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_SmoothProgression);
            CustomDifficulty.RndStartLocation = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_RndStartLocation);
            CustomDifficulty.RndStartingEquip = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_RndStartingEquip);
            CustomDifficulty.StartingEquip = (StartingEquip)Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_StartingEquip);
            CustomDifficulty.RndMercsAtStart = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_RndMercsAtStart);
            CustomDifficulty.StartingMercCount = Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_StartingMercCount);
            CustomDifficulty.RndClassesAtStart = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_RndClassesAtStart);
            CustomDifficulty.StartingClassesCount = Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_StartingClassesCount);
            // Operator
            CustomDifficulty.RevivePenalty = (RevivePenalty)Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_RevivePenalty);
            CustomDifficulty.DropPenalty = (DropPenalty)Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_DropPenalty);
            CustomDifficulty.DeathGift = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_DeathGift);
            CustomDifficulty.LosePerks = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_LosePerks);
            CustomDifficulty.LoseRank = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_LoseRank);
            CustomDifficulty.ExpMult = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_ExpMult) / 100f;
            CustomDifficulty.BackpacksSize = (BackpackSize) Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_BackpacksSize);
            CustomDifficulty.ItemsStackSize = (ItemStacksSize) Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_ItemsStackSize);
            // Mission
            CustomDifficulty.EvacRules = (EvacRules)Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_EvacRules);
            CustomDifficulty.EquipRepairAfterMission = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_EquipRepairAfterMission);
            CustomDifficulty.MissionStageCountMod = Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_MissionStageCountMod);
            CustomDifficulty.EnemyResistance = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_EnemyResistance);
            CustomDifficulty.MonsterPoints = Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_MonsterPoints) / 100f;
            CustomDifficulty.ItemPoints = Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_ItemPoints) / 100f;
            CustomDifficulty.KilledMobsItemsCondition = Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_KilledMobsItemsCondition) / 100f;
            // Sandbox
            CustomDifficulty.ForbidKillFaction = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_ForbidKillFaction);
            CustomDifficulty.FactionGrowthSpeed = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_FactionGrowthSpeed) / 100f;
            CustomDifficulty.FactionReputation = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_FactionReputation) / 100f;
            CustomDifficulty.MissionRewardPoints = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_MissionRewardPoints) / 100f;
            CustomDifficulty.BarterValue = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_BarterValue) / 100f;
            CustomDifficulty.MagnumCraftingTime = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_MagnumCraftingTime) / 100f;
            // Combat
            CustomDifficulty.EnemyActionPoint = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_EnemyActionPoint);
            CustomDifficulty.EnemyLos = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_EnemyLos);
            CustomDifficulty.EnemyHealth = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_EnemyHealth) / 100f;
            CustomDifficulty.EnemyDamageMult = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_EnemyDamageMult) / 100f;
            CustomDifficulty.EnemyDodgeMult = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_EnemyDodgeMult) / 100f;
            CustomDifficulty.EnemyResistance = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_EnemyResistance) / 100f;
            CustomDifficulty.QmorphLevelGrowth = Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_QmorphLevelGrowth) / 100f;
            CustomDifficulty.QmorphStatsAffect = Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_QmorphStatsAffect)/ 100f;
        }
    }
}