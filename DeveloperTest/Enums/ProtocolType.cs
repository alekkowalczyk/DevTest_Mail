using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Enums
{
    enum ProtocolType
    {
        [Description("POP3")]
        POP3, 
        [Description("IMAP")]
        IMAP
    }
}
