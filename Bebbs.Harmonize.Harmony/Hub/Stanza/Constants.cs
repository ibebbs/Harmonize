using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Harmony.Hub.Stanza
{
    internal class Constants
    {
        public const string XmppQueryActionElement = "oa";
        public const string XmppMimeAttribute = "mime";
        public const string XmppErrorCodeAttribute = "errorcode";
        public const string XmppErrorCodeForOk = "200";
        public const string SessionTokenRequestFormat = "token={0}:name={1}#{2}#{3}";
        public const string LogitechConnectNamespace = "connect.logitech.com";
        public const string SessionRequestPath = "vnd.logitech.connect/vnd.logitech.pair";
        public const string ConfigurationRequestPath = "vnd.logitech.harmony/vnd.logitech.harmony.engine?config";
        public const string ExecuteCommandPath = "vnd.logitech.harmony/vnd.logitech.harmony.engine?holdAction";
        public const string ExecuteCommandPattern = "action={{\"type\"::\"IRCommand\",\"deviceId\"::\"{0}\",\"command\"::\"{1}\"}}:status=press";
        public const string SessionName = "1vm7ATw/tN6HXGpQcCs/A5MkuvI";
        public const string SessionOs = "iOS6.0.1";
        public const string SessionDevice = "iPhone";
    }
}
