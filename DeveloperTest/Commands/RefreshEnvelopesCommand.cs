using DeveloperTest.Logic;
using DeveloperTest.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeveloperTest.Commands
{
    class RefreshEnvelopesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public RefreshEnvelopesCommand()
        {
            MailLogic.Instance.IsRefreshingEnvelopesChanged += (s, e) => RefreshCanExecute();
            MailLogic.Instance.IsFetchingMessagesChanged += (s, e) => RefreshCanExecute();
        }

        public void Execute(object parameter)
        {
            MainWindowViewModel.Instance.Envelopes.RefreshAsync();
        }

        public bool CanExecute(object parameter)
        {
            return !MailLogic.Instance.IsRefreshingEnvelopes && !MailLogic.Instance.IsFetchingMessages;
        }

        public void RefreshCanExecute()
        {
            MainWindowViewModel.Instance.RunInUIThread(() =>
                {
                    if (CanExecuteChanged != null)
                        CanExecuteChanged(this, null);
                });
            
        }
    }
}
