using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperTest.Enums;
using DeveloperTest.Logic;
using DeveloperTest.Utils;

namespace DeveloperTest.ViewModels
{
    class ConnectionSettingsViewModel : DeveloperTest.ViewModels.Base.BaseViewModel
    {
        private ProtocolType _protocolType;
        public ProtocolType ProtocolType
        {
            get
            {
                return _protocolType;
            }
            set
            {
                _protocolType = value;
                OnPropertyChanged();
            }
        }


        private EncryptionType _encryptionType;
        public EncryptionType EncryptionType
        {
            get
            {
                return _encryptionType;
            }
            set
            {
                _encryptionType = value;
                OnPropertyChanged();
            }
        }


        private string _server;
        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
                OnPropertyChanged();
            }
        }


        private int _port;
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                OnPropertyChanged();
            }
        }


        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }


        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }


        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public ConnectionSettingsViewModel()
        {
            ProtocolType = AppSettings.DefaultProtocol;
            EncryptionType = AppSettings.DefaultEncryption;
            Server = AppSettings.DefaultServer;
            Port = AppSettings.DefaultPort;
            Username = "";
            Password = "";
            IsEnabled = true;
            MailLogic.Instance.IsRefreshingEnvelopesChanged += (s, e) => this.IsEnabled = !MailLogic.Instance.IsRefreshingEnvelopes;
        }
    }
}
