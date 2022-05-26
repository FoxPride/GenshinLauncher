using GenshinLauncher.Models;
using GenshinLauncher.Services;
using System.Windows;

namespace GenshinLauncher
{
    public partial class App
    {
        public static AppConfig Config { get; set; }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Config = PersistAndRestoreService.LoadSettings();
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            PersistAndRestoreService.SaveSettings();
        }
    }
}