using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Bebbs.Harmonize.With.Store.Using.EventStore.Event
{
    public interface IProvider
    {
        EventData ToEvent(Message.IRegister message);

        EventData ToEvent(Message.IDeregister message);

        EventData ToEvent(Message.IObservation message);

        EventData ToEvent(Message.IAction message);
    }

    internal class Provider : IProvider
    {
        private byte[] ToBody(object message)
        {
            string json = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(json);
        }

        public EventData ToEvent(Message.IRegister message)
        {
            return new EventData(Guid.NewGuid(), "Register", true, ToBody(message), null);            
        }

        public EventData ToEvent(Message.IDeregister message)
        {
            return new EventData(Guid.NewGuid(), "Deregister", true, ToBody(message), null); 
        }

        public EventData ToEvent(Message.IObservation message)
        {
            return new EventData(Guid.NewGuid(), "Observation", true, ToBody(message), null); 
        }

        public EventData ToEvent(Message.IAction message)
        {
            return new EventData(Guid.NewGuid(), "Action", true, ToBody(message), null); 
        }
    }
}
