using DeveloperTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.ViewModels.Mail
{
    class MailBodyViewModel : DeveloperTest.ViewModels.Base.BaseViewModel
    {

        private string _content;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }


        private BodyType _type;
        public BodyType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }


        private MailEnvelopeViewModel _envelope;
        public MailEnvelopeViewModel Envelope
        {
            get
            {
                return _envelope;
            }
            set
            {
                _envelope = value;
                OnPropertyChanged();
            }
        }
    }
}
