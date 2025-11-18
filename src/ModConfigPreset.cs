using MGSC;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EdairTweaks
{
    public static partial class Keys
    {
        public const string HeaderStart = "ui.label.general";
        public const string HeaderOperator = "ui.label.operative";
        public const string HeaderMission = "ui.label.mission";
        public const string HeaderSandbox = "ui.label.sandbox";
        public const string HeaderCombat = "ui.label.combat";

        public const string Preset_Tutorial = "preset_tutorial";
        public const string Preset_SmoothProgression = "preset_smoothprogression";
        public const string Preset_RndStartLocation = "preset_rndstartlocation";
        public const string Preset_RndStartingEquip = "preset_rndstartingequip";
        public const string Preset_StartingEquip = "preset_startingequip";
        public const string Preset_RndMercsAtStart = "preset_rndmercsatstart";
        public const string Preset_StartingMercCount = "preset_startingmercscount";
        public const string Preset_RndClassesAtStart = "preset_rndclassesatstart";
        public const string Preset_StartingClassesCount = "preset_startingclassescount";
        public const string Preset_RevivePenalty = "preset_revivepenalty";
        public const string Preset_DropPenalty = "preset_droppenalty";
        public const string Preset_DeathGift = "preset_deathgift";
        public const string Preset_LosePerks = "preset_loseperks";
        public const string Preset_LoseRank = "preset_loserank";
        public const string Preset_ExpMult = "preset_expmult";
        public const string Preset_BackpacksSize = "preset_backpackssize";
        public const string Preset_ItemsStackSize = "preset_itemsstacksize";
        public const string Preset_AutoSave = "preset_autosave";
        public const string Preset_EvacRules = "preset_evacrules";
        public const string Preset_EquipRepairAfterMission = "preset_equiprepairaftermission";
        public const string Preset_MissionStageCountMod = "preset_missionstagecountmod";
        public const string Preset_MonsterPoints = "preset_monsterpoints";
        public const string Preset_ItemPoints = "preset_itempoints";
        public const string Preset_KilledMobsItemsCondition = "preset_killedmobsitemscondition";
        public const string Preset_ForbidKillFaction = "preset_forbiddkillfaction";
        public const string Preset_FactionGrowthSpeed = "preset_factiongrowthspeed";
        public const string Preset_FactionReputation = "preset_factionreputation";
        public const string Preset_MissionRewardPoints = "preset_missionrewardpoints";
        public const string Preset_BarterValue = "preset_bartervalue";
        public const string Preset_MagnumCraftingTime = "preset_magnumcraftingtime";
        public const string Preset_EnemyActionPoint = "preset_enemyactionpoint";
        public const string Preset_EnemyLos = "preset_enemylos";
        public const string Preset_EnemyHealth = "preset_enemyhealth";
        public const string Preset_EnemyDamageMult = "preset_enemydamagemult";
        public const string Preset_EnemyDodgeMult = "preset_enemydodgemult";
        public const string Preset_EnemyResistance = "preset_enemyresistance";
        public const string Preset_QmorphLevelGrowth = "preset_qmorphlevelgrowth";
        public const string Preset_QmorphStatsAffect = "preset_qmorphstatsaffect";

    }

    public class ModConfigPreset
    {
        private string ModName;
        public ModConfigData ModData;

        public ModConfigPreset(string ModName, string ConfigPath)
        {
            this.ModName = ModName;
            this.ModData = new ModConfigData(ConfigPath);

            string strOn = Localization.Get("ui.mapobject.stationscaner.on");
            string strOff = Localization.Get("ui.mapobject.stationscaner.off");



            // ======================================== START TAB ========================================

            this.ModData.AddConfigValue(Keys.HeaderStart, Keys.Preset_Tutorial, defaultValue: false, 
                labelKey: "ui.difficulty.Tutorial", 
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOn}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOn}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOff}");

            this.ModData.AddConfigValue(Keys.HeaderStart, Keys.Preset_SmoothProgression, defaultValue: false, 
                labelKey: "ui.difficulty.SmoothProgression",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}:  {strOn} \n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}:  {strOn} \n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOff}");

            this.ModData.AddConfigValue(Keys.HeaderStart, Keys.Preset_RndStartLocation, defaultValue: false, 
                labelKey: "ui.difficulty.RndStartLocation",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOff}");

            this.ModData.AddConfigValue(Keys.HeaderStart, Keys.Preset_RndStartingEquip, defaultValue: false, 
                labelKey: "ui.difficulty.RndStartingEquip",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOn}");

            this.ModData.AddConfigValue(Keys.HeaderStart, Keys.Preset_StartingEquip, 
                defaultValue: $"3. {Localization.Get("ui.difficulty.StartingEquip.normal")}", 
                valueList: new List<object>{ 
                    $"1. {Localization.Get("ui.difficulty.StartingEquip.none")}", 
                    $"2. {Localization.Get("ui.difficulty.StartingEquip.low")}", 
                    $"3. {Localization.Get("ui.difficulty.StartingEquip.normal")}", 
                    $"4. {Localization.Get("ui.difficulty.StartingEquip.high")}"},
                labelKey: "ui.difficulty.StartingEquip",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {Localization.Get("ui.difficulty.StartingEquip.high")}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {Localization.Get("ui.difficulty.StartingEquip.normal")}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {Localization.Get("ui.difficulty.StartingEquip.low")}");

            this.ModData.AddConfigValue(Keys.HeaderStart, Keys.Preset_RndMercsAtStart, defaultValue: false,
                labelKey: "ui.difficulty.RndMercsAtStart",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOn}");
            this.ModData.AddConfigValue(Keys.HeaderStart, Keys.Preset_StartingMercCount, defaultValue: 4, min: 1, max: 6,
                labelKey: "ui.difficulty.StartingMercCount",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 6\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 4\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 2");
            this.ModData.AddConfigValue(Keys.HeaderStart, Keys.Preset_RndClassesAtStart, defaultValue: false, 
                labelKey: "ui.difficulty.RndClassesAtStart",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOn}");
            this.ModData.AddConfigValue(Keys.HeaderStart, Keys.Preset_StartingClassesCount, defaultValue: 2, min: 1, max: 4, 
                labelKey: "ui.difficulty.StartingClassesCount",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 4\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 2\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 2");



            // ======================================== OPERATOR TAB ========================================
            this.ModData.AddConfigValue(Keys.HeaderOperator, Keys.Preset_RevivePenalty,
                defaultValue: $"2. {Localization.Get("ui.difficulty.RevivePenalty.new_clone")}",
                valueList: new List<object>{
                    $"1. {Localization.Get("ui.difficulty.RevivePenalty.no_penalty")}",
                    $"2. {Localization.Get("ui.difficulty.RevivePenalty.new_clone")}",
                    $"3. {Localization.Get("ui.difficulty.RevivePenalty.lose_chip")}"},
                labelKey: "ui.difficulty.RevivePenalty",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {Localization.Get("ui.difficulty.RevivePenalty.no_penalty")}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {Localization.Get("ui.difficulty.RevivePenalty.new_clone")}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {Localization.Get("ui.difficulty.RevivePenalty.lose_chip")}");

            this.ModData.AddConfigValue(Keys.HeaderOperator, Keys.Preset_DropPenalty,
                defaultValue: $"3. {Localization.Get("ui.difficulty.DropPenalty.full")}",
                valueList: new List<object>{
                    $"1. {Localization.Get("ui.difficulty.DropPenalty.none")}",
                    $"2. {Localization.Get("ui.difficulty.DropPenalty.bag")}",
                    $"3. {Localization.Get("ui.difficulty.DropPenalty.full")}"},

                labelKey: "ui.difficulty.DropPenalty",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {Localization.Get("ui.difficulty.DropPenalty.none")}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {Localization.Get("ui.difficulty.DropPenalty.full")}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {Localization.Get("ui.difficulty.DropPenalty.full")}");

            this.ModData.AddConfigValue(Keys.HeaderOperator, Keys.Preset_DeathGift, defaultValue: false, 
                labelKey: "ui.difficulty.DeathGift",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOn}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOn}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOff}");

            this.ModData.AddConfigValue(Keys.HeaderOperator, Keys.Preset_LosePerks, defaultValue: true, 
                labelKey: "ui.difficulty.LosePerks",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOn}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOn}");

            this.ModData.AddConfigValue(Keys.HeaderOperator, Keys.Preset_LoseRank, defaultValue: true, 
                labelKey: "ui.difficulty.LoseRank",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOn}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOn}");

            this.ModData.AddConfigValue(Keys.HeaderOperator, Keys.Preset_ExpMult, defaultValue: 100, min: 20, max: 300, 
                labelKey: "ui.difficulty.ExpMult",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 50%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 200%");

            this.ModData.AddConfigValue(Keys.HeaderOperator, Keys.Preset_BackpacksSize,
                defaultValue: $"1. {Localization.Get("ui.difficulty.BackpacksSize.X1")}",
                valueList: new List<object>{
                    $"1. {Localization.Get("ui.difficulty.BackpacksSize.X1")}",
                    $"2. {Localization.Get("ui.difficulty.BackpacksSize.X2")}",
                    $"3. {Localization.Get("ui.difficulty.BackpacksSize.X3")}",
                    $"4. {Localization.Get("ui.difficulty.BackpacksSize.X4")}"},
                labelKey: "ui.difficulty.BackpacksSize",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {Localization.Get("ui.difficulty.BackpacksSize.X2")}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {Localization.Get("ui.difficulty.BackpacksSize.X1")}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {Localization.Get("ui.difficulty.BackpacksSize.X1")}");

            this.ModData.AddConfigValue(Keys.HeaderOperator, Keys.Preset_ItemsStackSize,
                defaultValue: $"1. {Localization.Get("ui.difficulty.ItemsStackSize.X1")}",
                valueList: new List<object>{
                    $"1. {Localization.Get("ui.difficulty.ItemsStackSize.X1")}",
                    $"2. {Localization.Get("ui.difficulty.ItemsStackSize.X2")}",
                    $"3. {Localization.Get("ui.difficulty.ItemsStackSize.X3")}",
                    $"4. {Localization.Get("ui.difficulty.ItemsStackSize.X4")}"},
                labelKey: "ui.difficulty.ItemsStackSize",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {Localization.Get("ui.difficulty.ItemsStackSize.X1")}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {Localization.Get("ui.difficulty.ItemsStackSize.X1")}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {Localization.Get("ui.difficulty.ItemsStackSize.X1")}");



            // ======================================== MISSION TAB ========================================
            this.ModData.AddConfigValue(Keys.HeaderMission, Keys.Preset_AutoSave, defaultValue: false, 
                labelKey: "ui.difficulty.Autosave",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOn}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOff}");

            this.ModData.AddConfigValue(Keys.HeaderMission, Keys.Preset_EvacRules,
                defaultValue: $"2. {Localization.Get("ui.difficulty.EvacRules.chip_evac")}",
                valueList: new List<object>{
                    $"1. {Localization.Get("ui.difficulty.EvacRules.free_evac")}",
                    $"2. {Localization.Get("ui.difficulty.EvacRules.chip_evac")}",
                    $"3. {Localization.Get("ui.difficulty.EvacRules.mission_evac")}"},
                labelKey: "ui.difficulty.EvacRules",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {Localization.Get("ui.difficulty.EvacRules.free_evac")}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {Localization.Get("ui.difficulty.EvacRules.chip_evac")}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {Localization.Get("ui.difficulty.EvacRules.mission_evac")}");

            this.ModData.AddConfigValue(Keys.HeaderMission, Keys.Preset_EquipRepairAfterMission, defaultValue: false, 
                labelKey: "ui.difficulty.EquipRepairAfterMission",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOn}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOff}");

            this.ModData.AddConfigValue(Keys.HeaderMission, Keys.Preset_MissionStageCountMod, defaultValue: 0, min: -2, max: 3, 
                labelKey: "ui.difficulty.MissionStageCountMod",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 0\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 0\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 1");

            this.ModData.AddConfigValue(Keys.HeaderMission, Keys.Preset_MonsterPoints, defaultValue: 100, min: 0, max: 300, 
                labelKey: "ui.difficulty.MonsterPoints",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 80%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 120%");

            this.ModData.AddConfigValue(Keys.HeaderMission, Keys.Preset_ItemPoints, defaultValue: 100, min: 0, max: 300, 
                labelKey: "ui.difficulty.ItemPoints",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 120%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 80%");

            this.ModData.AddConfigValue(Keys.HeaderMission, Keys.Preset_KilledMobsItemsCondition, defaultValue: 100, min: 0, max: 100, 
                labelKey: "ui.difficulty.KilledMobsItemsCondition",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 70%");



            // ======================================== SANDBOX TAB ========================================
            this.ModData.AddConfigValue(Keys.HeaderSandbox, Keys.Preset_ForbidKillFaction, defaultValue: false, 
                labelKey: "ui.difficulty.ForbidKillFaction",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: {strOn}\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: {strOff}\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: {strOff}");

            this.ModData.AddConfigValue(Keys.HeaderSandbox, Keys.Preset_FactionGrowthSpeed, defaultValue: 100, min: 0, max: 400, 
                labelKey: "ui.difficulty.FactionGrowthSpeed",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 80%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 120%");

            this.ModData.AddConfigValue(Keys.HeaderSandbox, Keys.Preset_FactionReputation, defaultValue: 100, min: 0, max: 400, 
                labelKey: "ui.difficulty.FactionReputation",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 120%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 80%");

            this.ModData.AddConfigValue(Keys.HeaderSandbox, Keys.Preset_MissionRewardPoints, defaultValue: 100, min: 0, max: 400, 
                labelKey: "ui.difficulty.MissionRewardPoints",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 120%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 80%");

            this.ModData.AddConfigValue(Keys.HeaderSandbox, Keys.Preset_BarterValue, defaultValue: 100, min: 0, max: 400, 
                labelKey: "ui.difficulty.BarterValue",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 120%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 80%");

            this.ModData.AddConfigValue(Keys.HeaderSandbox, Keys.Preset_MagnumCraftingTime, defaultValue: 100, min: 0, max: 400, 
                labelKey: "ui.difficulty.MagnumCraftingTime",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 200%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 50%");



            // ======================================== COMBAT TAB ========================================
            this.ModData.AddConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyActionPoint, defaultValue: 0, min: -1, max: 2, 
                labelKey: "ui.difficulty.EnemyActionPoint",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: -1\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 0\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 1");

            this.ModData.AddConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyLos, defaultValue: 0, min: -2, max: 4, 
                labelKey: "ui.difficulty.EnemyLos",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: -1\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 0\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 2");

            this.ModData.AddConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyHealth, defaultValue: 100, min: 50, max: 200, 
                labelKey: "ui.difficulty.EnemyHealth",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 80%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 120%");

            this.ModData.AddConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyDamageMult, defaultValue: 100, min: 25, max: 200, 
                labelKey: "ui.difficulty.EnemyDamageMult",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 80%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 120%");

            this.ModData.AddConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyDodgeMult, defaultValue: 100, min: 25, max: 200, 
                labelKey: "ui.difficulty.EnemyDodgeMult",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 70%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 100%");

            this.ModData.AddConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyResistance, defaultValue: 100, min: 25, max: 200, 
                labelKey: "ui.difficulty.EnemyResistance",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 80%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 120%");

            this.ModData.AddConfigValue(Keys.HeaderCombat, Keys.Preset_QmorphLevelGrowth, defaultValue: 100, min: 25, max: 400, 
                labelKey: "ui.difficulty.QmorphLevelGrowth",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 80%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 120%");

            this.ModData.AddConfigValue(Keys.HeaderCombat, Keys.Preset_QmorphStatsAffect, defaultValue: 100, min: 25, max: 200, 
                labelKey: "ui.difficulty.QmorphStatsAffect",
                tooltipKey: "STRING:Default values:\n\n" +
                $"{Localization.Get("ui.difficulty.Easy.name")}: 80%\n" +
                $"{Localization.Get("ui.difficulty.Normal.name")}: 100%\n" +
                $"{Localization.Get("ui.difficulty.Hard.name")}: 120%");


            this.ModData.RegisterModConfigData(this.ModName);
        }

    }
}
