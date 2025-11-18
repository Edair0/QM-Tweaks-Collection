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
            ConfigGeneral = new ModConfigGeneral("QM Tweaks Collection", ConfigPath);
            ConfigPreset = new ModConfigPreset("QM Custom Preset", PresetPath);
            Harmony harmony = new Harmony("Edair0_" + ModAssemblyName);

            HarmonyHelper.ApplyPatches(harmony,
                new HarmonyHelper.PatchInfo(typeof(VestRecord), nameof(VestRecord.SlotCapacity), MethodType.Getter, typeof(PatchVest)),
                new HarmonyHelper.PatchInfo(typeof(CreatureData), nameof(CreatureData.GetItemsWeight), MethodType.Normal, typeof(PatchCreatureItemWeight)),
                new HarmonyHelper.PatchInfo(typeof(CreatureData), nameof(CreatureData.GetStarvMult), MethodType.Normal, typeof(PatchSatietyDrain)),
                new HarmonyHelper.PatchInfo(typeof(AutonomousCapsuleDepartment), nameof(AutonomousCapsuleDepartment.InvokeCapsule), MethodType.Normal, typeof(PatchMagnumCapsuleInvoke)),
                new HarmonyHelper.PatchInfo(typeof(AutonomousCapsuleDepartment), nameof(AutonomousCapsuleDepartment.OnPerksUpdated), MethodType.Normal, typeof(PatchMagnumCapsulePerkUpdate)),
                new HarmonyHelper.PatchInfo(typeof(EscortModeHelper), nameof(EscortModeHelper.SpawnEscortCreature), MethodType.Normal, typeof(PatchEscort)),
                new HarmonyHelper.PatchInfo(typeof(GlobalSettings), nameof(GlobalSettings.CounterattackMissionDurationTurns), MethodType.Getter, typeof(PatchRitualTurnLimit)),
                new HarmonyHelper.PatchInfo(typeof(GlobalSettings), nameof(GlobalSettings.ImplantGainChanceOnAmputation), MethodType.Getter, typeof(PatchAmputationChance)),
                new HarmonyHelper.PatchInfo(typeof(Player), nameof(Player.IsAbleToSpotAnEnemy), MethodType.Normal, typeof(PatchPlayerSpotEnemy)),
                new HarmonyHelper.PatchInfo(typeof(DifficultyScreen), nameof(DifficultyScreen.Configure), MethodType.Normal, typeof(PatchDifficultyPresets))
            );

        }


    }

    public static class HarmonyHelper
    {
        public class PatchInfo
        {
            public Type TargetType;
            public string MemberName;
            public MethodType MethodType;
            public Type PatchClass;

            public PatchInfo(Type targetType, string memberName, MethodType methodType, Type patchClass)
            {
                TargetType = targetType;
                MemberName = memberName;
                MethodType = methodType;
                PatchClass = patchClass;
            }
        }

        public static void ApplyPatches(Harmony harmony, params PatchInfo[] patches)
        {
            foreach (var patch in patches)
            {
                try
                {
                    MethodBase original = null;

                    switch (patch.MethodType)
                    {
                        case MethodType.Getter:
                            original = patch.TargetType.GetProperty(patch.MemberName)?.GetGetMethod();
                            break;
                        case MethodType.Setter:
                            original = patch.TargetType.GetProperty(patch.MemberName)?.GetSetMethod();
                            break;
                        default:
                            original = patch.TargetType.GetMethod(patch.MemberName);
                            break;
                    }

                    if (original == null)
                    {
                        Debug.LogError($"{Plugin.ModAssemblyName}: Original method not found: {patch.TargetType}.{patch.MemberName}");
                        continue;
                    }

                    MethodInfo prefix = patch.PatchClass.GetMethod("Prefix", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    MethodInfo postfix = patch.PatchClass.GetMethod("Postfix", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                    harmony.Patch(original,
                        prefix: prefix != null ? new HarmonyMethod(prefix) : null,
                        postfix: postfix != null ? new HarmonyMethod(postfix) : null
                    );
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{Plugin.ModAssemblyName}: Patch failed: {patch.PatchClass.Name} -> {ex}");
                }
            }
        }
    }


}
