using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Client.Connection.Producer
{
    public interface IInstance
    {
        byte[] BuildRegistration(With.Component.IEntity entity);

        byte[] BuildDeregistration(With.Component.IEntity entity);

        byte[] BuildObservation(With.Message.IObservation observation);

        byte[] BuildAction(With.Message.IAct action);
    }

    internal class Instance : IInstance
    {
        public byte[] BuildRegistration(With.Component.IEntity entity)
        {
            throw new NotImplementedException();
        }

        public byte[] BuildDeregistration(With.Component.IEntity entity)
        {
            throw new NotImplementedException();
        }

        public byte[] BuildObservation(With.Message.IObservation observation)
        {
            throw new NotImplementedException();
        }

        public byte[] BuildAction(With.Message.IAct action)
        {
            throw new NotImplementedException();
        }
    }
}
