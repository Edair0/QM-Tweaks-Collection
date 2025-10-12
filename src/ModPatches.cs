using HarmonyLib;
using MGSC;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static HarmonyLib.Code;
using static MGSC.BinaryPresetsMap;

namespace EdairTweaks
{
    [HarmonyPatch(typeof(VestRecord), nameof(VestRecord.SlotCapacity), MethodType.Getter)]
    public static class PatchVest
    {
        public static void Postfix(ref int __result)
        {
            __result = Mathf.RoundToInt(__result * Plugin.ConfigGeneral.ModData.GetConfigValue<int>(Keys.vest_slots_mult));
            __result = Math.Min(__result + Plugin.ConfigGeneral.ModData.GetConfigValue<int>(Keys.vest_slots_flat), 8);
            __result = Math.Max(__result, 1);
        }
    }

    [HarmonyPatch(typeof(CreatureData), nameof(CreatureData.GetItemsWeight))]
    public static class PatchCreatureItemWeight
    {
        public static void Postfix(ref float __result)
        {
            __result = Mathf.Max(0f, __result * (Plugin.ConfigGeneral.ModData.GetConfigValue<int>(Keys.general_weight_mult) / 100f));
        }
    }

    [HarmonyPatch(typeof(CreatureData), nameof(CreatureData.GetStarvMult))]
    public static class PatchSatietyDrain
    {
        public static void Postfix(ref float __result)
        {
            float mult = Plugin.ConfigGeneral.ModData.GetConfigValue<float>(Keys.general_satiety_mult) / 100f;
            __result = Mathf.Max(0f, __result * mult);
        }
    }

    [HarmonyPatch(typeof(AutonomousCapsuleDepartment), nameof(AutonomousCapsuleDepartment.InvokeCapsule))]
    public static class PatchMagnumCapsuleInvoke
    {
        public static void Prefix(AutonomousCapsuleDepartment __instance)
        {
            if (__instance.IsActiveDepartment() && __instance.HasFreeCapsule())
            {
                __instance.OnPerksUpdated();
            }
        }
    }

    [HarmonyPatch(typeof(AutonomousCapsuleDepartment), nameof(AutonomousCapsuleDepartment.OnPerksUpdated))]
    public static class PatchMagnumCapsulePerkUpdate
    {
        public static bool Prefix(AutonomousCapsuleDepartment __instance)
        {
            int mult = Plugin.ConfigGeneral.ModData.GetConfigValue<int>(Keys.general_capsule_mult);
            if (mult > 1) 
            {
                int autonomousCapsuleInventorySize = __instance._magnumSpaceship.AutonomousCapsuleInventorySize;
                if (autonomousCapsuleInventorySize * mult != __instance.CapsuleStorage.Height * __instance.CapsuleStorage.Width || mult != __instance.CapsuleStorage.Height)
                {
                    List<BasePickupItem> list = new List<BasePickupItem>(__instance.CapsuleStorage.Items);
                    __instance.CapsuleStorage.RemoveAllItems();
                    __instance.CapsuleStorage.Resize(autonomousCapsuleInventorySize, 1 * mult);
                    foreach (BasePickupItem basePickupItem in list)
                    {
                        __instance.CapsuleStorage.TryPutItem(basePickupItem, CellPosition.Zero, false, true);
                    }
                }
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(EscortModeHelper), nameof(EscortModeHelper.SpawnEscortCreature))]
    public static class PatchEscort
    {
        public static void Postfix(Creatures creatures)
        {
            if (Plugin.ConfigGeneral.ModData.GetConfigValue<bool>(Keys.escort_tough))
            {
                foreach (Monster monster in creatures.Monsters)
                {
                    if (monster.CreatureData.CreatureAlliance == CreatureAlliance.PlayerAlliance && monster.CreatureData.IsQuestCreature)
                    {
                        monster.CreatureData.Health.IncreaseMaxHealth(monster.CreatureData.Health.MaxValue * 3);
                        monster.CreatureData.Health.Restore(monster.CreatureData.Health.MaxValue);
                        monster.CreatureData.OverallResistMult += 0.5f;
                        monster.CreatureData.BaseOverallDodgeMult += 0.5f;
                        HealthRegenEffect healthRegen = new HealthRegenEffect(5, 100);
                        healthRegen.Endless = true;
                        healthRegen.IsPermanent = true;
                        monster.CreatureData.EffectsController.Add(healthRegen, false);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(GlobalSettings), nameof(GlobalSettings.RitualMissionDefenseDurationTurns), MethodType.Getter)]
    public static class PatchRitualTurnLimit
    {
        public static void Postfix(ref int __result)
        {
            if (Plugin.ConfigGeneral.ModData.GetConfigValue<bool>(Keys.ritual_moretime))
            {
                __result *= 5;
            }         
        }
    }

    [HarmonyPatch(typeof(GlobalSettings), nameof(GlobalSettings.NotifyHiddenEnemiesRadius), MethodType.Getter)]
    public static class PatchPlayerHearingRange
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
            CustomDifficulty.StartingEquip = Plugin.ConfigPreset.ModData.GetEnumValue<StartingEquip>(Keys.Preset_StartingEquip);
            CustomDifficulty.RndMercsAtStart = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_RndMercsAtStart);
            CustomDifficulty.StartingMercCount = Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_StartingMercCount);
            CustomDifficulty.RndClassesAtStart = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_RndClassesAtStart);
            CustomDifficulty.StartingClassesCount = Plugin.ConfigPreset.ModData.GetConfigValue<int>(Keys.Preset_StartingClassesCount);
            // Operator
            CustomDifficulty.RevivePenalty = Plugin.ConfigPreset.ModData.GetEnumValue<RevivePenalty>(Keys.Preset_RevivePenalty);
            CustomDifficulty.DropPenalty = Plugin.ConfigPreset.ModData.GetEnumValue<DropPenalty>(Keys.Preset_DropPenalty);
            CustomDifficulty.DeathGift = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_DeathGift);
            CustomDifficulty.LosePerks = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_LosePerks);
            CustomDifficulty.LoseRank = Plugin.ConfigPreset.ModData.GetConfigValue<bool>(Keys.Preset_LoseRank);
            CustomDifficulty.ExpMult = Plugin.ConfigPreset.ModData.GetConfigValue<float>(Keys.Preset_ExpMult) / 100f;
            CustomDifficulty.BackpacksSize = Plugin.ConfigPreset.ModData.GetEnumValue<BackpackSize>(Keys.Preset_BackpacksSize);
            CustomDifficulty.ItemsStackSize = Plugin.ConfigPreset.ModData.GetEnumValue<ItemStacksSize>(Keys.Preset_ItemsStackSize);
            // Mission
            CustomDifficulty.EvacRules = Plugin.ConfigPreset.ModData.GetEnumValue<EvacRules>(Keys.Preset_EvacRules);
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