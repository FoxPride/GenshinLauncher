using GenshinLauncher.Models;
using Microsoft.Win32;
using System.Linq;
using System.Text;

namespace GenshinLauncher.Helpers
{
    internal static class RegistryHelper
    {
        private const string InstallPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Genshin Impact";
        private const string Path = "HKEY_CURRENT_USER\\SOFTWARE\\miHoYo\\Genshin Impact";

        public static Resolution GetLastResolution()
        {
            var width = GetValueFromRegistry<int>("Screenmanager Resolution Width_h182942802");
            var height = GetValueFromRegistry<int>("Screenmanager Resolution Height_h2627697771");
            if (width != 0 && height != 0)
            {
                return Resolution.GetResolution(width, height);
            }
            return Resolution.Presets.Last();
        }

        public static string GetStringFromRegistry(string key)
        {
            var value = GetValueFromRegistry<byte[]>(key);
            return value != null ? Encoding.UTF8.GetString(value) : string.Empty;
        }

        public static string SearchForInstallPath()
        {
            var path = GetValueFromRegistry<string>($"HKEY_LOCAL_MACHINE\\{InstallPath}", "InstallPath");

            if (!string.IsNullOrEmpty(path))
            {
                return path;
            }

            path = GetValueFromRegistry<string>($"HKEY_CURRENT_USER\\{InstallPath}", "InstallPath");

            return !string.IsNullOrEmpty(path) ? path : string.Empty;
        }

        public static bool UpdateAccountInRegistry(Account account)
        {
            try
            {
                SetStringInRegistry("MIHOYOSDK_ADL_PROD_OVERSEA_h1158948810", account.AccountString);
                SetStringInRegistry("GENERAL_DATA_h2389025596", account.SettingsString);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void SetStringInRegistry(string key, string value)
        {
            Registry.SetValue(Path, key, Encoding.UTF8.GetBytes(value));
        }

        private static T? GetValueFromRegistry<T>(string path, string key)
        {
            var value = Registry.GetValue(path, key, null);

            return value != null ? (T)value : default;
        }

        private static T? GetValueFromRegistry<T>(string key)
        {
            var value = Registry.GetValue(Path, key, null);

            return value != null ? (T)value : default;
        }
    }
}