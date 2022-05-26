using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GenshinLauncher.Models
{
    public class AppConfig
    {
        [JsonConstructor]
        public AppConfig(Guid selectedAccountId, ObservableCollection<Account> accounts)
        {
            SelectedAccount = accounts.FirstOrDefault(r => r.Id == selectedAccountId);
            Accounts = accounts;
        }

        public AppConfig()
        {
            Accounts = new ObservableCollection<Account>();
            SelectedAccount = null;
        }

        public ObservableCollection<Account> Accounts { get; set; }

        [JsonIgnore]
        public Account? SelectedAccount { get; set; }

        public Guid? SelectedAccountId => SelectedAccount?.Id;
    }
}