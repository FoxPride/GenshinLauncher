using GenshinLauncher.Helpers;
using Newtonsoft.Json;
using System;

namespace GenshinLauncher.Models
{
    public class Account
    {
        public string AccountString { get; set; }

        public Guid Id { get; set; }

        public bool IsBorderLess { get; set; }

        public bool IsFullScreen { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public Resolution Preset { get; set; }

        public Quality SelectedQuality { get; set; }

        public string SettingsString { get; set; }

        public Account()
        {
            Name = "Current";
            AccountString = RegistryHelper.GetStringFromRegistry("MIHOYOSDK_ADL_PROD_OVERSEA_h1158948810");
            SettingsString = RegistryHelper.GetStringFromRegistry("GENERAL_DATA_h2389025596");
            SelectedQuality = Quality.Default;
            Preset = RegistryHelper.GetLastResolution();
            Location = LocationHelper.GetLocation();
            IsFullScreen = false;
            IsBorderLess = true;
            Id = Guid.Empty;
        }

        [JsonConstructor]
        public Account(string name, string accountString, string settingsString, Quality selectedQuality, Resolution preset, string location, bool isFullScreen, bool isBorderLess, Guid id)
        {
            Name = name;
            AccountString = accountString;
            SettingsString = settingsString;
            SelectedQuality = selectedQuality;
            Preset = preset;
            Location = location;
            IsFullScreen = isFullScreen;
            IsBorderLess = isBorderLess;
            Id = id;
        }
    }
}