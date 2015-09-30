using DeveloperTest.Commands;
using DeveloperTest.Logic;
using DeveloperTest.ViewModels.Mail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeveloperTest.ViewModels
{
    class EnvelopesViewModel : DeveloperTest.ViewModels.Base.BaseViewModel
    {
        private ObservableCollection<MailEnvelopeViewModel> _envelopes;
        public ObservableCollection<MailEnvelopeViewModel> Envelopes
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


        private MailEnvelopeViewModel _selectedEnvelope;
        public MailEnvelopeViewModel SelectedEnvelope
        {
            get
            {
                return _selectedEnvelope;
            }
            set
            {
                _selectedEnvelope = value;
                if(_selectedEnvelope != null && !_selectedEnvelope.IsBodyLoaded)
                {
                    MailLogic.Instance.FillBodyAsync(_selectedEnvelope);
                }
                OnPropertyChanged();
            }
        }

        private ICommand _refreshEnvelopesCommand;
        public ICommand RefreshEnvelopesCommand
        {
            get
            {
                return _refreshEnvelopesCommand ?? (_refreshEnvelopesCommand = new RefreshEnvelopesCommand());
            }
        }

        private ICommand _abortRefreshEnvelopesCommand;
        public ICommand AbortRefreshEnvelopesCommand
        {
            get
            {
                return _abortRefreshEnvelopesCommand ?? (_abortRefreshEnvelopesCommand = new LambdaCommand(() =>
                    {
                        MailLogic.Instance.AbortFillEnvelopes();
                    }));
            }
        }

        private ICommand _abortMessagesFetchCommand;
        public ICommand AbortMessagesFetchCommand
        {
            get
            {
                return _abortMessagesFetchCommand ?? (_abortMessagesFetchCommand = new LambdaCommand(() =>
                {
                    MailLogic.Instance.AbortMessagesFetch();
                }));
            }
        }


        public EnvelopesViewModel()
        {
            Envelopes = new ObservableCollection<MailEnvelopeViewModel>();
        }


        public void RefreshAsync()
        {
            MailLogic.Instance.FillEnvelopesAsync(this.Envelopes);
        }
    }
}
