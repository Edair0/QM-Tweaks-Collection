using MGSC;
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

            string tooltip_empty = "ui.empty.tooltip";
            string tooltipFlat = "A flat value.";
            string tooltipPercent = "A percentage value.";

            //this.ModData.AddConfigHeader(Keys.HeaderStart, "START");
            this.ModData.AddLocalizedConfigValue(Keys.HeaderStart, Keys.Preset_Tutorial, "ui.difficulty.Tutorial", tooltip_empty, false);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderStart, Keys.Preset_SmoothProgression, "ui.difficulty.SmoothProgression", tooltip_empty, false);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderStart, Keys.Preset_RndStartLocation, "ui.difficulty.RndStartLocation", tooltip_empty, false);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderStart, Keys.Preset_RndStartingEquip, "ui.difficulty.RndStartingEquip", tooltip_empty, false);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderStart, Keys.Preset_StartingEquip, "ui.difficulty.StartingEquip", tooltip_empty, 2, new List<string> { Localization.Get("ui.difficulty.StartingEquip.none"), Localization.Get("ui.difficulty.StartingEquip.low"), Localization.Get("ui.difficulty.StartingEquip.normal"), Localization.Get("ui.difficulty.StartingEquip.high") });
            this.ModData.AddLocalizedConfigValue(Keys.HeaderStart, Keys.Preset_RndMercsAtStart, "ui.difficulty.RndMercsAtStart", tooltip_empty, false);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderStart, Keys.Preset_StartingMercCount, "ui.difficulty.StartingMercCount", tooltipFlat, 4, 1, 6);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderStart, Keys.Preset_RndClassesAtStart, "ui.difficulty.RndClassesAtStart", tooltip_empty, false);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderStart, Keys.Preset_StartingClassesCount, "ui.difficulty.StartingClassesCount", tooltipFlat, 2, 1, 4);

            //this.ModData.AddConfigHeader(Keys.HeaderOperator, "OPERATOR");
            this.ModData.AddLocalizedConfigValue(Keys.HeaderOperator, Keys.Preset_RevivePenalty, "ui.difficulty.RevivePenalty", tooltip_empty, 1, new List<string> { Localization.Get("ui.difficulty.RevivePenalty.no_penalty"), Localization.Get("ui.difficulty.RevivePenalty.new_clone"), Localization.Get("ui.difficulty.RevivePenalty.lose_chip") });
            this.ModData.AddLocalizedConfigValue(Keys.HeaderOperator, Keys.Preset_DropPenalty, "ui.difficulty.DropPenalty", tooltip_empty, 2, new List<string> { Localization.Get("ui.difficulty.DropPenalty.none"), Localization.Get("ui.difficulty.DropPenalty.bag"), Localization.Get("ui.difficulty.DropPenalty.full") });
            this.ModData.AddLocalizedConfigValue(Keys.HeaderOperator, Keys.Preset_DeathGift, "ui.difficulty.DeathGift", tooltip_empty, false);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderOperator, Keys.Preset_LosePerks, "ui.difficulty.LosePerks", tooltip_empty, true);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderOperator, Keys.Preset_LoseRank, "ui.difficulty.LoseRank", tooltip_empty, true);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderOperator, Keys.Preset_ExpMult, "ui.difficulty.ExpMult", tooltipPercent, 100, 20, 300);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderOperator, Keys.Preset_BackpacksSize, "ui.difficulty.BackpacksSize", tooltip_empty, 0, new List<string> { Localization.Get("ui.difficulty.BackpacksSize.X1"), Localization.Get("ui.difficulty.BackpacksSize.X2"), Localization.Get("ui.difficulty.BackpacksSize.X3"), Localization.Get("ui.difficulty.BackpacksSize.X4") });
            this.ModData.AddLocalizedConfigValue(Keys.HeaderOperator, Keys.Preset_ItemsStackSize, "ui.difficulty.ItemsStackSize", tooltip_empty, 0, new List<string> { Localization.Get("ui.difficulty.ItemsStackSize.X1"), Localization.Get("ui.difficulty.ItemsStackSize.X2"), Localization.Get("ui.difficulty.ItemsStackSize.X3"), Localization.Get("ui.difficulty.ItemsStackSize.X4") });

            //this.ModData.AddConfigHeader(Keys.HeaderMission, "MISSION");
            this.ModData.AddLocalizedConfigValue(Keys.HeaderMission, Keys.Preset_AutoSave, "ui.difficulty.Autosave", tooltip_empty, false);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderMission, Keys.Preset_EvacRules, "ui.difficulty.EvacRules", tooltip_empty, 1, new List<string> { Localization.Get("ui.difficulty.EvacRules.free_evac"), Localization.Get("ui.difficulty.EvacRules.chip_evac"), Localization.Get("ui.difficulty.EvacRules.mission_evac") });
            this.ModData.AddLocalizedConfigValue(Keys.HeaderMission, Keys.Preset_EquipRepairAfterMission, "ui.difficulty.EquipRepairAfterMission", tooltip_empty, false);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderMission, Keys.Preset_MissionStageCountMod, "ui.difficulty.MissionStageCountMod", tooltipFlat, 0, -2, 3);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderMission, Keys.Preset_MonsterPoints, "ui.difficulty.MonsterPoints", tooltipPercent, 100, 20, 200);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderMission, Keys.Preset_ItemPoints, "ui.difficulty.ItemPoints", tooltipPercent, 100, 25, 300);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderMission, Keys.Preset_KilledMobsItemsCondition, "ui.difficulty.KilledMobsItemsCondition", tooltipPercent, 100, 0 ,100);

            //this.ModData.AddConfigHeader(Keys.HeaderSandbox, "SANDBOX");
            this.ModData.AddLocalizedConfigValue(Keys.HeaderSandbox, Keys.Preset_ForbidKillFaction, "ui.difficulty.ForbidKillFaction", tooltip_empty, false);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderSandbox, Keys.Preset_FactionGrowthSpeed, "ui.difficulty.FactionGrowthSpeed", tooltipPercent, 100, 25, 200);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderSandbox, Keys.Preset_FactionReputation, "ui.difficulty.FactionReputation", tooltipPercent, 100, 50, 200);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderSandbox, Keys.Preset_MissionRewardPoints, "ui.difficulty.MissionRewardPoints", tooltipPercent, 100, 60, 200);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderSandbox, Keys.Preset_BarterValue, "ui.difficulty.BarterValue", tooltipPercent, 100, 10, 200);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderSandbox, Keys.Preset_MagnumCraftingTime, "ui.difficulty.EnemyActionPoint", tooltipPercent, 100, 20, 400);

            //this.ModData.AddConfigHeader(Keys.HeaderCombat, "COMBAT");
            this.ModData.AddLocalizedConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyActionPoint, "ui.difficulty.EnemyActionPoint", tooltipFlat, 0, -1, 2);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyLos, "ui.difficulty.EnemyLos", tooltipFlat, 0, -2, 4);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyHealth, "ui.difficulty.EnemyHealth", tooltipPercent, 100, 50, 200);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyDamageMult, "ui.difficulty.EnemyDamageMult", tooltipPercent, 100, 50, 200);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyDodgeMult, "ui.difficulty.EnemyDodgeMult", tooltipPercent, 100, 50, 200);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderCombat, Keys.Preset_EnemyResistance, "ui.difficulty.EnemyResistance", tooltipPercent, 100, 50, 200);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderCombat, Keys.Preset_QmorphLevelGrowth, "ui.difficulty.QmorphLevelGrowth", tooltipPercent, 100, 50, 400);
            this.ModData.AddLocalizedConfigValue(Keys.HeaderCombat, Keys.Preset_QmorphStatsAffect, "ui.difficulty.QmorphStatsAffect", tooltipPercent, 100, 50, 200);

            this.ModData.RegisterModConfigData(this.ModName, "QM Custom Preset");
        }

    }
}
