using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Message
{
    public interface IAct
    {
        Component.IIdentity Entity { get; }
        Component.IIdentity Actionable { get; }
        Component.IIdentity Actor { get; }
        IEnumerable<Component.IParameterValue> ParameterValues { get; }
    }

    public class Act : IAct
    {
        public Act(Component.IIdentity entity, Component.IIdentity actionable, Component.IIdentity actor, IEnumerable<Component.IParameterValue> parameterValues)
        {
            Entity = entity;
            Actionable = actionable;
            Actor = actor;
            ParameterValues = parameterValues;
        }

        public Component.IIdentity Entity { get; private set; }

        public Component.IIdentity Actionable { get; private set; }

        public Component.IIdentity Actor { get; private set; }

        public IEnumerable<Component.IParameterValue> ParameterValues { get; private set; }
    }
}
