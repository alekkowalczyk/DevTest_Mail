using DeveloperTest.Commands;
using DeveloperTest.Logic;
using DeveloperTest.Model.Connection;
using DeveloperTest.ViewModels.Mail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace DeveloperTest.ViewModels
{
    class MainWindowViewModel : DeveloperTest.ViewModels.Base.BaseViewModel
    {
        public static readonly string ResourceKey = "MainWindowViewModelInstance";
        
        private readonly SynchronizationContext _syncContext;

        public static MainWindowViewModel Instance
        {
            get
            {
                return App.Current.Resources[ResourceKey] as MainWindowViewModel;
            }
        }


        private ConnectionSettingsViewModel _connectionSettings;
        public ConnectionSettingsViewModel ConnectionSettings
        {
            get
            {
                return _connectionSettings;
            }
            set
            {
                _connectionSettings = value;
                OnPropertyChanged();
            }
        }

       
        private EnvelopesViewModel _envelopes;
        public EnvelopesViewModel Envelopes
        {
            get
            {
                return _envelopes;
            }
            set
            {
                _envelopes = value;
                OnPropertyChanged();
            }
        }
  
        
        public MainWindowViewModel()
        {
            ConnectionSettings = new ConnectionSettingsViewModel();
            Envelopes = new EnvelopesViewModel();
            _syncContext = SynchronizationContext.Current;
        }


        public void RunInUIThread(Action action)
        {
            _syncContext.Post((o) => action(), null);
        }
    }
}
