using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Attachment
{
    internal interface IDescription
    {
        string ApplicationName { get; }

        string ConnectionSpecification { get; }

        string ServiceName { get; }

        ushort ServicePort { get; }
    }

    internal class Description : IDescription
    {
        public Description(string applicationName, string connectionSpecification, string serviceName, ushort servicePort)
        {
            ApplicationName = applicationName;
            ConnectionSpecification = connectionSpecification;
            ServiceName = serviceName;
            ServicePort = servicePort;
        }

        public string ApplicationName { get; private set; }

        public string ConnectionSpecification { get; private set; }

        public string ServiceName { get; private set; }

        public ushort ServicePort { get; private set; }
    }
}
