using MGSC;
using ModConfigMenu;
using ModConfigMenu.Contracts;
using ModConfigMenu.Implementations;
using ModConfigMenu.Objects;
using ModConfigMenu.Services;
using Rewired.Localization;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

namespace EdairTweaks
{
    public class ModConfigData
    {
        private string ConfigPath;
        private Dictionary<string, object> Settings;
        private List<IConfigValue> ConfigValues;

        public ModConfigData(string ConfigPath)
        {
            this.ConfigPath = ConfigPath;
            this.Settings = new Dictionary<string, object>();
            this.ConfigValues = new List<IConfigValue>();
            this.LoadConfig();
        }

        public void RegisterModConfigData(string menuName)
        {
            ModConfigMenuAPI.RegisterModConfig(menuName, ConfigValues, OnSave);
        }

        public void AddConfigHeader(string headerKey, string locKey = null)
        {
            GetKeyEnsureLocalization(headerKey, KeyType.Header, locKey);
        }

        public void AddConfigValue(string headerKey, string valueKey, object defaultValue, string labelKey, string tooltipKey)
        {
            string headerKeyLoc = GetKeyEnsureLocalization(headerKey, KeyType.Header, valueKey);
            string labelKeyLoc = GetKeyEnsureLocalization(labelKey, KeyType.Label, valueKey);
            string tooltipKeyLoc = GetKeyEnsureLocalization(tooltipKey, KeyType.Tooltip, valueKey);

            if (!Settings.ContainsKey(valueKey)) { Settings.Add(valueKey, defaultValue); }
            ConfigValue result = new ConfigValue(valueKey, Settings[valueKey], headerKeyLoc, defaultValue, tooltipKeyLoc, labelKeyLoc);
            ConfigValues.Add(result);
        }

        public void AddConfigValue(string headerKey, string valueKey, int defaultValue, int min, int max, string labelKey, string tooltipKey)
        {
            string headerKeyLoc = GetKeyEnsureLocalization(headerKey, KeyType.Header, valueKey);
            string labelKeyLoc = GetKeyEnsureLocalization(labelKey, KeyType.Label, valueKey);
            string tooltipKeyLoc = GetKeyEnsureLocalization(tooltipKey, KeyType.Tooltip, valueKey);

            if (!Settings.ContainsKey(valueKey)) { Settings.Add(valueKey, defaultValue); }
            RangeConfig<int> result = new RangeConfig<int>(valueKey, GetConfigValue<int>(valueKey), defaultValue, min, max, headerKeyLoc, tooltipKeyLoc, labelKeyLoc);
            ConfigValues.Add(result);
        }

        public void AddConfigValue(string headerKey, string valueKey, string defaultValue, List<object> valueList, string labelKey, string tooltipKey)
        {
            string headerKeyLoc = GetKeyEnsureLocalization(headerKey, KeyType.Header, valueKey);
            string labelKeyLoc = GetKeyEnsureLocalization(labelKey, KeyType.Label, valueKey);
            string tooltipKeyLoc = GetKeyEnsureLocalization(tooltipKey, KeyType.Tooltip, valueKey);

            if (!Settings.ContainsKey(valueKey)) { Settings.Add(valueKey, defaultValue); }

            DropdownConfig result = new DropdownConfig(valueKey, GetConfigValue<string>(valueKey), headerKeyLoc, defaultValue, tooltipKeyLoc, labelKeyLoc, valueList);
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

        public TEnum GetEnumValue<TEnum>(string key, TEnum fallback = default) where TEnum : struct, Enum
        {
            string value = GetConfigValue<string>(key);
            if (string.IsNullOrEmpty(value)) { return fallback; }
            
            try
            {
                int dotIndex = value.IndexOf('.');
                if (dotIndex <= 0) { return fallback; }
                   
                string numberPart = value.Substring(0, dotIndex);
                if (int.TryParse(numberPart, out int index))
                {
                    index -= 1;

                    var values = (TEnum[])Enum.GetValues(typeof(TEnum));
                    if (index < 0) { return fallback; }      
                    if (index >= values.Length) { return values[values.Length - 1]; }
                    return values[index];
                }
                return fallback;
            }
            catch { return fallback; }
        }

        private string GetKeyEnsureLocalization(string key, KeyType keyType, string locKey = null)
        {
            if (key.StartsWith("STRING:") && locKey != null)
            {
                string data = key.Replace("STRING:", "");
                string result = $"{Plugin.ModAssemblyName}.{locKey}";
                if (keyType == KeyType.Header) { result += ".header"; }
                else if (keyType == KeyType.Label) { result += ".label"; }
                else if (keyType == KeyType.Tooltip) { result += ".tooltip"; }
                else if (keyType == KeyType.Description) { result += ".desc"; }
                
                if (Localization.HasKey(result)) { return result; }
                else { LocalizationHelper.AddKeyToAllDictionaries(result, data); return result; }
            }
            return key;
        }

        private void CreateConfig()
        {
            if (!File.Exists(ConfigPath)) { Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath)); File.Create(ConfigPath).Close(); }
        }

        private void LoadConfig()
        {
            if (!File.Exists(ConfigPath)) 
            {
                CreateConfig();
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
                    if (int.TryParse(value, out int intValue)) { Settings.Add(key, intValue); }
                    else if (float.TryParse(value, out float floatValue)) { Settings.Add(key, floatValue); }
                    else if (bool.TryParse(value, out bool boolValue)) { Settings.Add(key, boolValue); }
                    else { Settings.Add(key, value); }
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

        public enum KeyType
        {
            Header,
            Label,
            Tooltip,
            Description
        }

    }
}
