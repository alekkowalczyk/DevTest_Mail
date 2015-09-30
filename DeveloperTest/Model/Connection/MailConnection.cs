using DeveloperTest.Enums;
using DeveloperTest.Exceptions;
using DeveloperTest.Logic;
using Limilabs.Client;
using Limilabs.Client.IMAP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Model.Connection
{
    abstract class MailConnection : IDisposable
    {
        private readonly string GMAIL_LOGINONBROWSER_MESSAGE = "[ALERT] Please log in via your web browser:";
        private readonly string GMAIL_DOMAIN = "gmail.com";
        protected ClientBase _connection;
        private EncryptionType _encryption;
        private string _server;
        private int _port;
        private string _username;
        private string _password;

        public bool IsConnected
        {
            get
            {
                return _connection.Connected;
            }
        }

        public MailConnection(EncryptionType encryption, string server, int port, string username, string password)
        {
            _encryption = encryption;
            _server = server.Trim();
            _port = port;
            _username = username.Trim();
            _password = password.Trim();
        }

        protected abstract ClientBase getClientObject();
        protected abstract void close();
        protected abstract void startTSL();
        protected abstract void login(string username, string password);
        public abstract IEnumerable GetUids();
        public abstract EnvelopeModel GetEnvelope(object uid);
        public abstract BodyModel GetBody(object uid);

        public void ConnectAndLogin()
        {
            if(_connection != null)
            {
                this.Dispose();
            }
            _connection = getClientObject();

            try
            {

                if (_encryption == EncryptionType.Unencrypted)
                {
                    _connection.Connect(this._server, this._port);
                }
                else
                {
                    _connection.ConnectSSL(this._server, this._port);
                    if (_encryption == EncryptionType.STARTTLS)
                        startTSL();
                }

                login(_username, _password);
            }
            catch(ImapResponseException ire)
            {
                //aditional authentication by gmail
                if (ire.Message.StartsWith(GMAIL_LOGINONBROWSER_MESSAGE, StringComparison.InvariantCultureIgnoreCase) && _server.EndsWith(GMAIL_DOMAIN, StringComparison.InvariantCultureIgnoreCase))
                {
                    //tried to open the gmail authentication page, but it doesn't help
                    //if (ire.Response.Lines.Count > 0)
                    //{
                    //    System.Diagnostics.Process.Start(ire.Response.Lines[0].Substring(ire.Response.Lines[0].IndexOf("http")));
                    //    StatusProvider.DisplayStatus("Please authenticate on the opened web page, and try again.");
                    //}
                    //switching "unsafe authentication" is needed in gmail settings, so displaying the support page describing it
                    var httpPos = ire.Message.IndexOf("http");
                    if(httpPos>-1)
                    {
                        var url = ire.Message.Substring(httpPos);
                        var spacePos = url.IndexOf(" ");
                        if (spacePos > -1)
                            url = url.Substring(0, spacePos);
                        System.Diagnostics.Process.Start(url);
                        ErrorProvider.DisplayError("Gmail authentication failed, please read the opened support page.");
                    }
                }
                else
                    throw new MailServerException(ire.Message);
            }
            catch(ServerException se)
            {
                 throw new MailServerException(se.Message);
            }
        }

        public void Dispose()
        {
            if(_connection!=null)
            {
                close();
                _connection.Dispose();
            }
        }
    }
}
