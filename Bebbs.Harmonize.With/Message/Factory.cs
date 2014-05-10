using System;
using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Message
{
    public interface IFactory
    {
        With.Message.IRegister BuildRegistration(Component.IIdentity registrar, Component.IEntity entity);

        With.Message.IDeregister BuildDeregistration(Component.IIdentity registrar, Component.IIdentity entity);

        With.Message.IObservation BuildObservation(Component.IIdentity entity, Component.IIdentity observable, DateTimeOffset date, Component.IMeasurement measurement);

        With.Message.IAction BuildAction(Component.IIdentity actor, Component.IIdentity entity, Component.IIdentity actionable, IEnumerable<Component.IParameterValue> parameterValues);
    }

    internal class Factory : IFactory
    {
        public IRegister BuildRegistration(Component.IIdentity registrar, Component.IEntity entity)
        {
            return new Register(registrar, entity);
        }

        public IDeregister BuildDeregistration(Component.IIdentity registrar, Component.IIdentity entity)
        {
            return new Deregister(registrar, entity);
        }

        public IObservation BuildObservation(Component.IIdentity entity, Component.IIdentity observable, DateTimeOffset date, Component.IMeasurement measurement)
        {
            return new Observation(entity, observable, date, measurement);
        }

        public IAction BuildAction(Component.IIdentity actor, Component.IIdentity entity, Component.IIdentity actionable, IEnumerable<Component.IParameterValue> parameterValues)
        {
            return new Action(entity, actionable, actor, parameterValues);
        }
    }
}
