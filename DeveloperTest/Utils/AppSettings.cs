using DeveloperTest.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Utils
{
    class AppSettings
    {
        private static readonly string defaultImapServerSettingsKey = "DefaultMailServer";
        private static readonly string defaultImapPortSettingsKey = "DefaultMailPort";
        private static readonly string defaultProtocolSettingsKey = "DefaultProtocol";
        private static readonly string defaultEncrptionSettingsKey = "DefaultEncrption";

        public static string DefaultServer
        {
            get
            {
                return getSetting(defaultImapServerSettingsKey);
            }
        }

        public static int DefaultPort
        {
            get
            {
                string strPort = getSetting(defaultImapPortSettingsKey);
                var port = 993;
                int.TryParse(strPort, out port);
                return port;
            }
        }

        public static ProtocolType DefaultProtocol
        {
            get
            {
                string strProtocol = getSetting(defaultProtocolSettingsKey);
                var protocol = ProtocolType.IMAP;
                Enum.TryParse<ProtocolType>(strProtocol, out protocol);
                return protocol;
            }
        }

        public static EncryptionType DefaultEncryption
        {
            get
            {
                string strEncryption = getSetting(defaultEncrptionSettingsKey);
                var encryption = EncryptionType.SSLTLS;
                Enum.TryParse<EncryptionType>(strEncryption, out encryption);
                return encryption;
            }
        }

        private static string getSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
