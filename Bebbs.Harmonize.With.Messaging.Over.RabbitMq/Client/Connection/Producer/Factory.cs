using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Client.Connection.Producer
{
    public interface IFactory
    {
        IInstance CreateProducer(IModel _model);
    }

    internal class Factory : IFactory
    {
        public IInstance CreateProducer(IModel _model)
        {
            return new Instance();
        }
    }
}
