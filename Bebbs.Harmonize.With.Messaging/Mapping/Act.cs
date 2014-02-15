using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Act : Message.IAct
    {
        public Component.IIdentity Entity { get; set; }
        public Component.IIdentity Actionable { get; set; }
        public Component.IIdentity Actor { get; set; }
        public IEnumerable<Component.IParameterValue> ParameterValues { get; set; }
    }
}
