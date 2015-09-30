using DeveloperTest.Exceptions;
using DeveloperTest.Model.Connection;
using DeveloperTest.ViewModels;
using DeveloperTest.ViewModels.Mail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperTest.Logic.Mappers;
using System.ComponentModel;
using DeveloperTest.Model;
using System.Collections.Concurrent;
using System.Threading;
using DeveloperTest.Enums;
using DeveloperTest.Utils;

namespace DeveloperTest.Logic
{
    class MailLogic : DeveloperTest.ViewModels.Base.NotifyPropertyChangedBase
    {
        private readonly int backgroundBodyFetchDelay = 200;

        private static MailLogic _instance;
        public static MailLogic Instance
        {
            get
            {
                return _instance ?? (_instance = new MailLogic());
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshingEnvelopes
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                _isRefreshing = value;
                if (IsRefreshingEnvelopesChanged != null)
                    IsRefreshingEnvelopesChanged(this, null);
                OnPropertyChanged();
            }
        }
        public event EventHandler IsRefreshingEnvelopesChanged;


        private bool _isFetchingMessages;
        public bool IsFetchingMessages
        {
            get
            {
                return _isFetchingMessages;
            }
            set
            {
                _isFetchingMessages = value;
                if (IsFetchingMessagesChanged != null)
                    IsFetchingMessagesChanged(this, null);
                OnPropertyChanged();
            }
        }
        public event EventHandler IsFetchingMessagesChanged;

        private MailLogicStatus _currentStatus;
        public MailLogicStatus CurrentStatus
        {
            get
            {
                return _currentStatus;
            }
            set
            {
                _currentStatus = value;
                OnPropertyChanged();
            }
        }


        private int _TotalEnvelopesCount;
        public int TotalEnvelopesCount
        {
            get
            {
                return _TotalEnvelopesCount;
            }
            set
            {
                _TotalEnvelopesCount = value;
                OnPropertyChanged();
            }
        }

        
        private int _fetchedEnvelopesCount;
        public int FetchedEnvelopesCount
        {
            get
            {
                return _fetchedEnvelopesCount;
            }
            set
            {
                _fetchedEnvelopesCount = value;
                OnPropertyChanged();
            }
        }



        private int _fetchedMessagesCount;
        public int FetchedMessagesCount
        {
            get
            {
                return _fetchedMessagesCount;
            }
            set
            {
                _fetchedMessagesCount = value;
                OnPropertyChanged();
            }
        }

        public ConnectionSettingsViewModel ConnectionSettings
        {
            get
            {
                return MainWindowViewModel.Instance.ConnectionSettings;
            }
        }

        public MailConnection Connection { get; private set; }
        private bool _pleaseAbortEnvelopesRefresh = false;
        private bool _pleaseAbortMessagesFetch = false;
        private List<MailEnvelopeViewModel> _envelopesToFill;

        public MailLogic()
        {
            _envelopesToFill = new List<MailEnvelopeViewModel>();
            CurrentStatus = MailLogicStatus.Idle;
        }

        private bool connectAndLogin()
        {
            CurrentStatus = MailLogicStatus.Connecting;
            if (Connection != null)
                Connection.Dispose();

            switch (ConnectionSettings.ProtocolType)
            {
                case Enums.ProtocolType.IMAP:
                    Connection = new MailImapConnection(ConnectionSettings.EncryptionType, ConnectionSettings.Server, ConnectionSettings.Port, ConnectionSettings.Username, ConnectionSettings.Password);
                    break;
                case Enums.ProtocolType.POP3:
                default:
                    Connection = new MailPop3Connection(ConnectionSettings.EncryptionType, ConnectionSettings.Server, ConnectionSettings.Port, ConnectionSettings.Username, ConnectionSettings.Password);
                    break;
            }

            try
            {
                Connection.ConnectAndLogin();
                return true;
            }
            catch(MailServerException e)
            {
                //TODO: error handling
                return false;
            }
        }

        public async void FillEnvelopesAsync(ObservableCollection<MailEnvelopeViewModel> colEnvelopes)
        {
            TotalEnvelopesCount = 0;
            FetchedEnvelopesCount = 0;
            FetchedMessagesCount = 0;
            _envelopesToFill.Clear();
            Action bodyFetchingAction = async () =>
                {
                    IsFetchingMessages = true;
                    _pleaseAbortMessagesFetch = false;
                    while(true)
                    {
                        lock(_envelopesToFill)
                        {
                            if(_envelopesToFill.Count == 0)
                            {
                                break;
                            }
                        }
                        if (_pleaseAbortMessagesFetch)
                            break;
                        Thread.Sleep(backgroundBodyFetchDelay);
                        await FillBodyAsync(_envelopesToFill[0]);
                        if (!IsRefreshingEnvelopes)
                            CurrentStatus = MailLogicStatus.FetchingMessages;
                    }
                    IsFetchingMessages = false;
                    CurrentStatus = MailLogicStatus.Idle;
                };

            await Task.Run(() =>
                    {
                        this.IsRefreshingEnvelopes = true;
                        if (connectAndLogin())
                        {
                            MainWindowViewModel.Instance.RunInUIThread(() => colEnvelopes.Clear());

                            var uids = Connection.GetUids();
                            _pleaseAbortEnvelopesRefresh = false;
                            var count = 0;
                            var fetchingActionFired = false;
                            this.TotalEnvelopesCount = uids.Count();

                            CurrentStatus = MailLogicStatus.FetchinEnvelopesAndMessages;
                            foreach (var uidObject in uids)
                            {
                                if (_pleaseAbortEnvelopesRefresh)
                                    break;
                                if(count==5)
                                {
                                    fetchingActionFired = true;
                                    bodyFetchingAction();
                                }

                                EnvelopeModel model = null;
                                lock (Connection)
                                {
                                    model = Connection.GetEnvelope(uidObject);
                                }
                                if (model != null)
                                {
                                    MailEnvelopeViewModel envelopeVM = model.ToViewModel();
                                    MainWindowViewModel.Instance.RunInUIThread(() => colEnvelopes.Add(envelopeVM));
                                    lock (_envelopesToFill)
                                    {
                                        _envelopesToFill.Add(envelopeVM);
                                        FetchedEnvelopesCount++;
                                    }
                                }
                                count++;
                            }
                            if (!fetchingActionFired)
                                bodyFetchingAction();
                        }
                    });
            this.IsRefreshingEnvelopes = false;
        }

        public void AbortFillEnvelopes()
        {
            _pleaseAbortEnvelopesRefresh = true;
        }

        public void AbortMessagesFetch()
        {
            this._pleaseAbortMessagesFetch = true;
        }

        public async Task FillBodyAsync(MailEnvelopeViewModel envelope)
        {
            await Task.Run(() =>
                {
                    if (Connection.IsConnected || connectAndLogin())
                    {
                        BodyModel model = null;
                        lock (Connection)
                        {
                            model = Connection.GetBody(envelope.UID);
                        }
                        if (model != null)
                        {
                            envelope.SetBody(model.ToViewModel());
                            lock (_envelopesToFill)
                            {
                                _envelopesToFill.Remove(envelope);
                                FetchedMessagesCount++;
                            }
                        }
                    }
                });
        }
    }
}
