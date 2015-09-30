using DeveloperTest.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.ViewModels.Mail
{
    class AddressViewModel : BaseViewModel
    {

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }


        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Name))
                return string.Format("{0} <{1}>", Name, Address);
            else
                return Address;
        }
    }
}
