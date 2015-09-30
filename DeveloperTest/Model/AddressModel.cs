using Limilabs.Mail.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Model
{
    class AddressModel
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        public AddressModel(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public AddressModel(MailBox mailBox)
        {
            Name = mailBox.Name;
            Address = mailBox.Address;
        }
    }
}
