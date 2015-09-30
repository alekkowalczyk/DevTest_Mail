using DeveloperTest.Enums;
using Limilabs.Client;
using Limilabs.Client.IMAP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Model.Connection
{
    class MailImapConnection : MailConnection
    {
        private Imap imapConnection
        {
            get
            {
                if (this._connection is Imap)
                    return this._connection as Imap;
                return null;
            }
        }

        public MailImapConnection(EncryptionType encryption, string server, int port, string username, string password):
            base(encryption, server, port, username, password)
        {

        }

        protected override ClientBase getClientObject()
        {
            return new Imap();
        }

        protected override void close()
        {
            if (this.imapConnection != null)
            {
                try
                {
                    this.imapConnection.Close();
                }
                catch(ServerException se)
                {
                    //TODO: error handling
                }
            }
        }

        protected override void startTSL()
        {
            if (this.imapConnection != null)
            {
                this.imapConnection.StartTLS();
            }
        }

        protected override void login(string username, string password)
        {
            if (this.imapConnection != null)
            {
                this.imapConnection.Login(username, password);
            }
        }

        public override IEnumerable GetUids()
        {
            if(this.imapConnection!= null)
            {
                this.imapConnection.ExamineInbox();
                var retList = this.imapConnection.Search(Flag.All);
                retList.Reverse();
                return retList;
            }
            return new List<long>();
        }

        public override EnvelopeModel GetEnvelope(object uid)
        {
            if(uid is long)
            {
                if (this.imapConnection != null)
                {
                    var env = imapConnection.GetEnvelopeByUID((long)uid);
                    return new EnvelopeModel(env);
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
            if (uid is long)
            {
                if (this.imapConnection != null)
                {
                    var structure = imapConnection.GetBodyStructureByUID((long)uid);
                    if (structure.Html != null)
                    {
                        var html = this.imapConnection.GetTextByUID(structure.Html);
                        return BodyModel.GetHtmlBody(html);                        
                    }
                    else if (structure.Text != null)
                    {
                        var txt = this.imapConnection.GetTextByUID(structure.Text);
                        return BodyModel.GetTextBody(txt);
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
