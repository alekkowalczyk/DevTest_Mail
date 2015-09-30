using DeveloperTest.Enums;
using Limilabs.Client;
using Limilabs.Client.POP3;
using Limilabs.Mail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Model.Connection
{
    class MailPop3Connection : MailConnection
    {
        private Pop3 pop3Connection
        {
            get
            {
                if (this._connection is Pop3)
                    return this._connection as Pop3;
                return null;
            }
        }

        public MailPop3Connection(EncryptionType encryption, string server, int port, string username, string password) :
            base(encryption, server, port, username, password)
        {

        }

        protected override Limilabs.Client.ClientBase getClientObject()
        {
            return new Pop3();
        }

        protected override void close()
        {
            if(pop3Connection!=null)
            {
                try
                {
                    this.pop3Connection.Close(); 
                }
                catch (ServerException se)
                {
                    //TODO: error handling
                }
            }
        }

        protected override void startTSL()
        {
            if(pop3Connection!=null)
            {
                pop3Connection.StartTLS();
            }
        }

        protected override void login(string username, string password)
        {
            if (pop3Connection != null)
            {
                pop3Connection.Login(username, password);
            }
        }

        public override IEnumerable GetUids()
        {
            if (pop3Connection != null)
            {
               var retList = pop3Connection.GetAll();
               retList.Reverse();
               return retList;
            }
            return new List<string>();
        }

        public override EnvelopeModel GetEnvelope(object uid)
        {
            if(uid is string)
            {
                if (pop3Connection != null)
                {
                    MailBuilder builder = new MailBuilder();
                    var headers = pop3Connection.GetHeadersByUID(uid as string);
                    IMail email = builder.CreateFromEml(headers);
                    return new EnvelopeModel(email, uid as string);
                }
            }
            else
            {
                //TODO: error handling
            }
            return null;
        }

        public override BodyModel GetBody(object uid)
        {
            if (uid is string)
            {
                if (pop3Connection != null)
                {
                    MailBuilder builder = new MailBuilder();
                    var headers = pop3Connection.GetMessageByUID(uid as string);
                    IMail email = builder.CreateFromEml(headers);
                    if (!string.IsNullOrWhiteSpace(email.Html))
                    {
                        return BodyModel.GetHtmlBody(email.Html);
                    }
                    else if (!string.IsNullOrWhiteSpace(email.Text))
                    {
                        return BodyModel.GetTextBody(email.Text);
                    }
                }
            }
            else
            {
                //TODO: error handling
            }
            return null;
        }
    }
}
