using DeveloperTest.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Exceptions
{
    class MailServerException : Exception
    {
        public MailServerException(string msg) : base(msg)
        {
            ErrorProvider.DisplayError("Gmail authentication failed, please read the opened support page.");
        }
    }
}
