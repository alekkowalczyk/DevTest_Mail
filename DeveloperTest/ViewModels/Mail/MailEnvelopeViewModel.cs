using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.ViewModels.Mail
{
    class MailEnvelopeViewModel : DeveloperTest.ViewModels.Base.BaseViewModel
    {


        private AddressViewModel _from;
        public AddressViewModel From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
                OnPropertyChanged();
            }
        }


        private string _subject;
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
                OnPropertyChanged();
            }
        }


        private DateTime? _date;
        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<AddressViewModel> _recipients;
        public ObservableCollection<AddressViewModel> Recipients
        {
            get
            {
                return _recipients;
            }
            set
            {
                _recipients = value;
                OnPropertyChanged();
            }
        }

        public object UID { get; set; }

        private MailBodyViewModel _body;
        public MailBodyViewModel Body
        {
            get
            {
                return _body;
            }
            private set
            {
                _body = value;
                OnPropertyChanged();
                OnPropertyChanged(() => IsBodyLoaded);
            }
        }
        public bool IsBodyLoaded
        {
            get
            {
                return Body != null;
            }
        }

        public void SetBody(MailBodyViewModel body)
        {
            body.Envelope = this;
            Body = body;
        }
    }
}
