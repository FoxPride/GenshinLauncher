using CliWrap;
using CliWrap.Builders;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GenshinLauncher.Helpers;
using GenshinLauncher.Models;
using GenshinLauncher.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFUI.Common;
using WPFUI.Controls;

namespace GenshinLauncher.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public Account? SelectedAccount
        {
            get => App.Config.SelectedAccount;
            set
            {
                if (App.Config.SelectedAccount == value) return;

                App.Config.SelectedAccount = value;
                OnPropertyChanged();

                if (value == null) return;

                if (RegistryHelper.UpdateAccountInRegistry(value))
                {
                    ShowSnackChangeAccountSuccess();
                }
                else
                {
                    ShowSnackChangeAccountWarning();
                }
            }
        }

        public ObservableCollection<Account> Accounts
        {
            get => App.Config.Accounts;
            set
            {
                if (App.Config.Accounts == value) return;

                App.Config.Accounts = value;
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        private int _width = 800;

        [ObservableProperty]
        private int _height = 600;

        private bool _isPresetDialogSubscribed;

        [ObservableProperty]
        private string _accountName;

        private bool _isAccountDialogSubscribed;

        private static void ShowSnackWarning()
        {
            var snackBar = (Application.Current.MainWindow as MainWindow)?.RootSnackbar;

            if (snackBar == null) return;

            snackBar.Appearance = Appearance.Danger;
            snackBar.Icon = SymbolRegular.Warning24;
            snackBar.Show("Error!", "Please add an account first!");
        }

        private static void ShowSnackChangeAccountWarning()
        {
            var snackBar = (Application.Current.MainWindow as MainWindow)?.RootSnackbar;

            if (snackBar == null) return;

            snackBar.Appearance = Appearance.Danger;
            snackBar.Icon = SymbolRegular.Warning24;
            snackBar.Show("Error!", "Account change failed!");
        }

        private static void ShowSnackChangeAccountSuccess()
        {
            var snackBar = (Application.Current.MainWindow as MainWindow)?.RootSnackbar;

            if (snackBar == null) return;

            snackBar.Appearance = Appearance.Success;
            snackBar.Icon = SymbolRegular.Checkmark28;
            snackBar.Show("Success!", "Successfully changed account!");
        }

        [ICommand]
        private void DeleteAccount()
        {
            if (SelectedAccount == null)
            {
                ShowSnackWarning();
                return;
            }

            Accounts.Remove(SelectedAccount);
            SelectedAccount = Accounts.FirstOrDefault();
        }

        [ICommand]
        private void SetLocation()
        {
            if (SelectedAccount == null)
            {
                ShowSnackWarning();
                return;
            }

            var text = LocationHelper.SetLocation();
            if (!string.IsNullOrEmpty(text))
            {
                SelectedAccount.Location = text;
            }
        }

        [ICommand]
        private async Task LaunchGame()
        {
            if (SelectedAccount == null)
            {
                ShowSnackWarning();
                return;
            }

            var client = Cli.Wrap(SelectedAccount.Location);

            var command = client.WithArguments(delegate (ArgumentsBuilder args)
            {
                args.Add("-screen-width").Add(SelectedAccount.Preset.Width)
                    .Add("-screen-height").Add(SelectedAccount.Preset.Height)
                    .Add("-screen-fullscreen").Add(SelectedAccount.IsFullScreen ? 1 : 0);

                if (SelectedAccount.IsBorderLess)
                {
                    args.Add("-popupwindow");
                }
                if (SelectedAccount.SelectedQuality != 0)
                {
                    args.Add("-screen-quality").Add(SelectedAccount.SelectedQuality);
                }
            });

            if (SelectedAccount.IsCloseBeforeStart)
            {
                var processes = Process.GetProcessesByName("GenshinImpact");
                foreach (var process in processes)
                {
                    process.Kill();
                    await process.WaitForExitAsync();
                    process.Dispose();
                }

                RegistryHelper.UpdateAccountInRegistry(SelectedAccount);
            }

            try
            {
                await command.ExecuteAsync();
            }
            catch (InvalidOperationException)
            {
                LocationHelper.SetLocation();
            }
            catch (Win32Exception)
            {
                LocationHelper.SetLocation();
            }
        }

        [ICommand]
        private void ShowAddAccountDialog()
        {
            var dialog = (Application.Current.MainWindow as MainWindow)?.AddAccountDialog;

            if (dialog == null) return;

            if (!_isAccountDialogSubscribed)
            {
                dialog.ButtonLeftClick += (_, _) =>
                {
                    var account = new Account
                    {
                        Name = string.IsNullOrEmpty(AccountName) ? "Default" : AccountName,
                        Id = Guid.NewGuid()
                    };

                    Accounts.Add(account);

                    SelectedAccount = account;

                    CloseAddAccountDialogAndReset(dialog);
                };

                dialog.ButtonRightClick += (_, _) =>
                {
                    CloseAddAccountDialogAndReset(dialog);
                };

                _isAccountDialogSubscribed = true;
            }

            dialog.Show();
        }

        private void CloseAddAccountDialogAndReset(Dialog dialog)
        {
            AccountName = string.Empty;

            dialog.Hide();
        }

        [ICommand]
        private void ShowAddPresetDialog()
        {
            if (SelectedAccount == null)
            {
                ShowSnackWarning();
                return;
            }

            var dialog = (Application.Current.MainWindow as MainWindow)?.AddPresetDialog;

            if (dialog == null) return;

            if (!_isPresetDialogSubscribed)
            {
                dialog.ButtonLeftClick += (_, _) =>
                {
                    SelectedAccount.Preset = Resolution.GetResolution(Width, Height);

                    OnPropertyChanged(nameof(SelectedAccount));

                    CloseAddPresetDialogAndReset(dialog);
                };

                dialog.ButtonRightClick += (_, _) =>
                {
                    CloseAddPresetDialogAndReset(dialog);
                };

                _isPresetDialogSubscribed = true;
            }

            dialog.Show();
        }

        private void CloseAddPresetDialogAndReset(Dialog dialog)
        {
            Width = 800;
            Height = 600;

            dialog.Hide();
        }
    }
}