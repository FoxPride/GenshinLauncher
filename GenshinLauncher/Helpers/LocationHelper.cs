using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;

namespace GenshinLauncher.Helpers
{
    internal static class LocationHelper
    {
        public static string GetLocation()
        {
            return CheckDefaultLocations() ?? SetLocation();
        }

        public static string SetLocation()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Executable|*.exe|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true && File.Exists(openFileDialog.FileName))
                return openFileDialog.FileName;

            return string.Empty;
        }

        private static string? CheckDefaultLocations()
        {
            const string gameName = "GenshinImpact.exe";

            var installationPath = $"{RegistryHelper.SearchForInstallPath()}\\Genshin Impact Game\\{gameName}";

            if (File.Exists(installationPath))
            {
                return installationPath;
            }

            return new[]
            {
                $"C:\\Program Files\\Genshin Impact\\Genshin Impact Game\\{gameName}",
                AppContext.BaseDirectory + gameName
            }.FirstOrDefault(File.Exists);
        }
    }
}