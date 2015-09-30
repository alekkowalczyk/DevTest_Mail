using Limilabs.Mail;
using Limilabs.Mail.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Model
{
    class EnvelopeModel
    {
        public AddressModel From { get; private set; }
        public string Subject { get; private set; }
        public List<AddressModel> Recipients { get; private set; }
        public DateTime? Date { get; private set; }

        public object UID { get; private set; }
        public EnvelopeModel(Limilabs.Client.IMAP.Envelope env)
        {
            UID = env.UID;
            if (env.From != null && env.From.Count > 0)
                From = new AddressModel(env.From[0]);
            Subject = env.Subject;
            Date = env.Date;

            Recipients = new List<AddressModel>();
            foreach(var envRec in env.To)
            {
                var mailBoxes = envRec.GetMailboxes();
                foreach(var mb in mailBoxes)
                {
                    Recipients.Add(new AddressModel(mb));
                }
            }
        }

        public EnvelopeModel(IMail imail, string uid)
        {
            UID = uid;
            if (imail.From != null && imail.From.Count > 0)
                From = new AddressModel(imail.From[0]);
            Subject = imail.Subject;
            Date = imail.Date;

            Recipients = new List<AddressModel>();
            foreach(var imRec in imail.To)
            {
                var mailBoxes = imRec.GetMailboxes();
                foreach (var mb in mailBoxes)
                {
                    Recipients.Add(new AddressModel(mb));
                }
            }
        }
    }
}
