using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Enums
{
    enum MailLogicStatus
    {
        [Description("Idle")]
        Idle,
        [Description("Connecting")]
        Connecting,
        [Description("Error")]
        Error,
        [Description("Fetching envelopes")]
        FetchinEnvelopesAndMessages,
        [Description("Fetching only messages")]
        FetchingMessages
    }
}
