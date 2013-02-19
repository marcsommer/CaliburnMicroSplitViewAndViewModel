using System.Collections.ObjectModel;
using Caliburn.Micro;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Common.ViewModels
{
    public class MainViewModel : Screen
    {
        public MainViewModel()
        {
            Accounts = new ObservableCollection<AccountViewModel>();
        }

        public ObservableCollection<AccountViewModel> Accounts { get; private set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            LoadAccounts();
        }

        private void LoadAccounts()
        {
            Accounts.Clear();

            Accounts.Add(new AccountViewModel
                         {
                             Name = "Account 1"
                         });
            Accounts.Add(new AccountViewModel
                         {
                             Name = "Account 2"
                         });
            Accounts.Add(new AccountViewModel
                         {
                             Name = "Account 3"
                         });
            Accounts.Add(new AccountViewModel
                         {
                             Name = "Account 4"
                         });
        }

        public void TakeActionOnEventArgs(ItemClickEventArgs args)
        {
            AccountViewModel account = args.ClickedItem as AccountViewModel;

            if (account != null)
            {
                var dialog = new MessageDialog(string.Format("You clicked {0}", account.Name), "Using $eventArgs");
                dialog.ShowAsync();
            }
        }

        public void TakeActionOnAccount(AccountViewModel account)
        {
            if (account != null)
            {
                var dialog = new MessageDialog(string.Format("You clicked {0}", account.Name), "Using $account");
                dialog.ShowAsync();
            }
        }
    }
}