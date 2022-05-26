using GenshinLauncher.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GenshinLauncher.Services
{
    public static class PersistAndRestoreService
    {
        private const string SettingsFile = "accounts.json";

        public static AppConfig LoadSettings()
        {
            var settingsLocation = GetSettingsLocation();

            if (string.IsNullOrEmpty(settingsLocation))
            {
                return new AppConfig();
            }

            var properties = JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(settingsLocation), new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error
            });

            return properties ?? new AppConfig();
        }

        public static void SaveSettings()
        {
            File.WriteAllText(CreateSettingsFile(), JsonConvert.SerializeObject(App.Config), Encoding.UTF8);
        }

        private static string CreateSettingsFile()
        {
            var settingsLocation = GetSettingsLocation();

            if (!string.IsNullOrEmpty(settingsLocation))
            {
                return settingsLocation;
            }

            settingsLocation = Path.Combine(AppContext.BaseDirectory, SettingsFile);

            try
            {
                File.Create(settingsLocation).Close();
                return settingsLocation;
            }
            catch (UnauthorizedAccessException)
            {
                settingsLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Process.GetCurrentProcess().ProcessName, SettingsFile);
                File.Create(settingsLocation).Close();
                return settingsLocation;
            }
        }

        private static string GetSettingsLocation()
        {
            var locations = new[]
            {
                Path.Combine(AppContext.BaseDirectory, SettingsFile),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Process.GetCurrentProcess().ProcessName, SettingsFile)
            };

            var path = string.Empty;

            foreach (var location in locations)
            {
                path = File.Exists(location) ? location : string.Empty;

                if (!string.IsNullOrEmpty(path))
                {
                    break;
                }
            }

            return path;
        }
    }
}