using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Action : Message, With.Message.IAction
    {
        Component.IIdentity IAction.Entity
        {
            get { return Entity; }
        }

        Component.IIdentity IAction.Actionable
        {
            get { return Actionable; }
        }

        Component.IIdentity IAction.Actor
        {
            get { return Actor; }
        }

        IEnumerable<Component.IParameterValue> IAction.ParameterValues
        {
            get { return ParameterValues; }
        }

        public Identity Entity { get; set; }

        public Identity Actionable { get; set; }

        public Identity Actor { get; set; }

        public ParameterValue[] ParameterValues { get; set; }
    }
}
