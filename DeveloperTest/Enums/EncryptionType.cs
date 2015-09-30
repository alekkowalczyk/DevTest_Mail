using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Enums
{
    enum EncryptionType
    {
        [Description("Unencrypted")]
        Unencrypted, 
        [Description(@"SSL/TLS")]
        SSLTLS, 
        [Description("StartTLS")]
        STARTTLS
    }
}
