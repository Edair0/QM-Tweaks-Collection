using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace edair_mod_inventory
{
    public class ModConfig
    {
        public int bp_slots_flat;
        public float bp_slots_mult;
        public int bp_weight_flat;
        public int vest_slots_flat;
        public float vest_slots_mult;
        public int satiety_kg;

        [JsonIgnore]
        public DateTime LastConfigFileChange = DateTime.MinValue;

        public ModConfig()
        {
            SetDefaults(false);
        }

        public void SetVanilla(bool Save)
        {
            bp_slots_flat = 0;
            bp_slots_mult = 1;
            bp_weight_flat = 0;
            vest_slots_flat = 0;
            vest_slots_mult = 1;
            satiety_kg = 10;
            if (Save) { SaveConfig(); }
        }

        public void SetDefaults(bool Save)
        {
            bp_slots_flat = 0;
            bp_slots_mult = 2;
            bp_weight_flat = 35;
            vest_slots_flat = 0;
            vest_slots_mult = 2;
            satiety_kg = 50;
            if(Save) { SaveConfig(); }
        }
        public void VerifySettings()
        {
            if(bp_slots_mult == 0) { bp_slots_mult = 1; }
            if(vest_slots_mult == 0) { vest_slots_mult = 1; }
            if(satiety_kg == 0) { satiety_kg = 10; }
            SaveConfig();
        }

        public string TryChangeSettingInt(ref int Setting, string SettingName, string Value)
        {
            int oldValue = Setting;
            if (int.TryParse(Value, out int result))
            {
                Setting = result;
                VerifySettings();
                return $"Mod setting changed succesfully. Value of {SettingName} changed from {oldValue} to {Setting}";
            }
            return "Incorrect value. Value must be integer (e.g. 2)";
        }
        public string TryChangeSettingFloat(ref float Setting, string SettingName, string Value)
        {
            float oldValue = Setting;
            if (float.TryParse(Value, out float result))
            {
                Setting = result;
                VerifySettings();
                return $"Mod setting changed succesfully. Value of {SettingName} changed from {oldValue} to {Setting}";
            }
            return "Incorrect value. Value must be float (e.g. 1.5)";
        }
        public void SaveConfig()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented, };

            if (File.Exists(Plugin.ConfigPath))
            {
                string json = JsonConvert.SerializeObject(Plugin.Config, serializerSettings);
                File.WriteAllText(Plugin.ConfigPath, json);
            }
        }

    }

    public static class ModConfigSerializer
    {
        public static ModConfig LoadConfig(string configPath)
        {
            ModConfig config;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented, };

            string configDirectory = Path.GetDirectoryName(configPath);
            Directory.CreateDirectory(configDirectory);

            if (File.Exists(configPath))
            {
                try
                {
                    string sourceJson = File.ReadAllText(configPath);
                    config = JsonConvert.DeserializeObject<ModConfig>(sourceJson, serializerSettings);
                    string upgradeConfig = JsonConvert.SerializeObject(config, serializerSettings);

                    if (upgradeConfig != sourceJson)
                    {
                        Debug.Log("Updating config with missing elements");
                        File.WriteAllText(configPath, upgradeConfig);
                    }
                    config.LastConfigFileChange = new FileInfo(configPath).LastWriteTime;
                    return config;
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error parsing configuration. Ignoring config file and using defaults");
                    Debug.LogException(ex);
                    //Not overwriting in case the user just made a typo.
                    config = new ModConfig();
                    return config;
                }
            }
            else
            {
                //New config
                config = new ModConfig();

                string json = JsonConvert.SerializeObject(config, serializerSettings);
                File.WriteAllText(configPath, json);

                config.LastConfigFileChange = new FileInfo(configPath).LastWriteTime;
                return config;
            }
        }

        public static ModConfig ReloadConfig(ModConfig currentConfig, string configPath)
        {
            if (currentConfig.LastConfigFileChange == new FileInfo(configPath).LastWriteTime) { return currentConfig; }
            //Try to reload, but return current config if it fails
            try
            {
                ModConfig config;
                config = JsonConvert.DeserializeObject<ModConfig>(File.ReadAllText(configPath));
                config.LastConfigFileChange = new FileInfo(configPath).LastWriteTime;
                return config;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error parsing configuration. Keeping existing config.");
                Debug.LogException(ex);
                return currentConfig;
            }
        }

    }

}
