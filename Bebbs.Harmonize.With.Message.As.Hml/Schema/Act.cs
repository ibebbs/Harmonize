using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Act : Message, With.Message.IAct
    {
        Component.IIdentity IAct.Entity
        {
            get { return Entity; }
        }

        Component.IIdentity IAct.Actionable
        {
            get { return Actionable; }
        }

        Component.IIdentity IAct.Actor
        {
            get { return Actor; }
        }

        IEnumerable<Component.IParameterValue> IAct.ParameterValues
        {
            get { return ParameterValues; }
        }

        public Identity Entity { get; set; }

        public Identity Actionable { get; set; }

        public Identity Actor { get; set; }

        public ParameterValue[] ParameterValues { get; set; }
    }
}
