using MGSC;
using ModConfigMenu;
using ModConfigMenu.Objects;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdairTweaks
{
    public class ModConfigData
    {
        private string ConfigPath;
        private Dictionary<string, object> Settings;
        private List<ConfigValue> ConfigValues;

        public ModConfigData(string ConfigPath)
        {
            this.ConfigPath = ConfigPath;
            this.Settings = new Dictionary<string, object>();
            this.ConfigValues = new List<ConfigValue>();
            this.CreateConfig();
        }

        public void RegisterModConfigData(string menuName, string desc)
        {
            this.LoadConfig();
            LocalizationHelper.AddKeyToAllDictionaries($"{Plugin.ModAssemblyName}.{menuName}.key", desc);
            ModConfigMenuAPI.RegisterModConfig($"{Plugin.ModAssemblyName}.{menuName}.key", ConfigValues, OnSave);
        }

        public void AddConfigHeader(string key, string desc)
        {
            LocalizationHelper.AddKeyToAllDictionaries($"{Plugin.ModAssemblyName}.{key}.header", desc);
        }

        public void AddLocalizedConfigValue(string headerKey, string key, string labelKey, string tooltipKey, object defaultValue)
        {
            if (!Settings.ContainsKey(key)) { Settings[key] = defaultValue; }
            ConfigValue result = new ConfigValue(key, Settings[key], headerKey, defaultValue, tooltipKey, labelKey);
            ConfigValues.Add(result);
        }

        public void AddLocalizedConfigValue(string headerKey, string key, string labelKey, string tooltipKey, object defaultValue, float min, float max)
        {
            if (!Settings.ContainsKey(key)) { Settings[key] = defaultValue; }
            ConfigValue result = new ConfigValue(key, Settings[key], headerKey, defaultValue, tooltipKey, labelKey, min, max);
            ConfigValues.Add(result);
        }

        public void AddLocalizedConfigValue(string headerKey, string key, string labelKey, string tooltipKey, object defaultValue, List<string> dropdown)
        {
            if (!Settings.ContainsKey(key)) { Settings[key] = defaultValue; }
            ConfigValue result = new ConfigValue(key, Settings[key], headerKey, defaultValue, tooltipKey, labelKey, dropdown);
            MetaData newProp = new MetaData("type", "dropdown");
            result.Properties.Add(newProp);
            ConfigValues.Add(result);
        }

        public void AddConfigValue(string headerKey, string key, string label, string tooltip, object defaultValue)
        {
            if (!Settings.ContainsKey(key)) { Settings[key] = defaultValue; }
            ConfigValue result = new ConfigValue(key, Settings[key], $"{Plugin.ModAssemblyName}.{headerKey}.header", defaultValue, $"{Plugin.ModAssemblyName}.{key}.tooltip", $"{Plugin.ModAssemblyName}.{key}.label");
            LocalizationHelper.AddKeyToAllDictionaries($"{Plugin.ModAssemblyName}.{key}.label", label);
            LocalizationHelper.AddKeyToAllDictionaries($"{Plugin.ModAssemblyName}.{key}.tooltip", tooltip);
            ConfigValues.Add(result);
        }

        public void AddConfigValue(string headerKey, string key, string label, string tooltip, object defaultValue, float min, float max)
        {
            if (!Settings.ContainsKey(key)) { Settings[key] = defaultValue; }
            ConfigValue result = new ConfigValue(key, Settings[key], $"{Plugin.ModAssemblyName}.{headerKey}.header", defaultValue, $"{Plugin.ModAssemblyName}.{key}.tooltip", $"{Plugin.ModAssemblyName}.{key}.label", min, max);
            LocalizationHelper.AddKeyToAllDictionaries($"{Plugin.ModAssemblyName}.{key}.label", label);
            LocalizationHelper.AddKeyToAllDictionaries($"{Plugin.ModAssemblyName}.{key}.tooltip", tooltip);
            ConfigValues.Add(result);
        }

        public T GetConfigValue<T>(string key, T fallback = default)
        {
            if (Settings.TryGetValue(key, out var value))
            {
                try { return (T)Convert.ChangeType(value, typeof(T)); }
                catch { return fallback; }
            }
            return fallback;
        }

        private void CreateConfig()
        {
            if (!File.Exists(ConfigPath)) { Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath)); File.Create(ConfigPath).Close(); }
        }

        private void LoadConfig()
        {
            if (!File.Exists(ConfigPath)) 
            { 
                return; 
            }

            string[] data = File.ReadAllLines(ConfigPath);
            foreach (string line in data)
            {
                if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line)) { continue; }

                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    if (!Settings.ContainsKey(key)) { continue; }
                    else if (int.TryParse(value, out int intValue)) { Settings[key] = intValue; }
                    else if (float.TryParse(value, out float floatValue)) { Settings[key] = floatValue; }
                    else if (bool.TryParse(value, out bool boolValue)) { Settings[key] = boolValue; }
                    else { Settings[key] = value; }
                }
            }
            foreach (var configValue in ConfigValues)
            {
                if (Settings.ContainsKey(configValue.Key))
                {
                    configValue.Value = Settings[configValue.Key];
                }
            }
        }
        private void SaveConfig()
        {
            if (!File.Exists(ConfigPath)) { CreateConfig(); }
            File.WriteAllLines(ConfigPath, Settings.Select(entry => $"{entry.Key}={entry.Value}"));
        }

        protected virtual bool OnSave(Dictionary<string, object> newConfig, out string feedbackMessage)
        {
            feedbackMessage = "Saving";
            Settings = newConfig;
            SaveConfig();
            return true;
        }
    }
}
