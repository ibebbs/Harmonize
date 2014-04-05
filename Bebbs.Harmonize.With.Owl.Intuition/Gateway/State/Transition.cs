using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State
{
    public interface ITransition
    {
        void ToDisconnected();
        void ToConnecting();
        void ToConfiguration(Command.Endpoint.IInstance commandEndpoint, Values.Version version);
        void ToRegistration(Command.Endpoint.IInstance commandEndpoint, Values.Version version);
        void ToListening(Command.Endpoint.IInstance commandEndpoint, Values.Version version);
        void ToFaulted(Command.Endpoint.IInstance commandEndpoint, Exception exception);
    }

    internal class Transition : ITransition
    {
        private readonly Gateway.Event.IMediator _eventMediator;
        private readonly Event.IFactory _eventFactory;
        private readonly Context.IFactory _contextFactory;

        public Transition(Gateway.Event.IMediator eventMediator, Event.IFactory eventFactory, Context.IFactory contextFactory)
        {
            _eventMediator = eventMediator;
            _eventFactory = eventFactory;
            _contextFactory = contextFactory;
        }

        public void ToDisconnected()
        {
            Context.IDisconnected context = _contextFactory.ForDisconnected();
            Event.Transition message = _eventFactory.ForStateTransition(context);

            _eventMediator.Publish(message);
        }

        public void ToConnecting()
        {
            Context.IConnection context = _contextFactory.ForConnection();
            Event.Transition message = _eventFactory.ForStateTransition(context);

            _eventMediator.Publish(message);
        }

        public void ToConfiguration(Command.Endpoint.IInstance commandEndpoint, Values.Version version)
        {
            Context.IConfiguration context = _contextFactory.ForConfiguration(commandEndpoint, version);
            Event.Transition message = _eventFactory.ForStateTransition(context);

            _eventMediator.Publish(message);
        }
        
        public void ToRegistration(Command.Endpoint.IInstance commandEndpoint, Values.Version version)
        {
            Context.IRegistration context = _contextFactory.ForRegistration(commandEndpoint, version);
            Event.Transition message = _eventFactory.ForStateTransition(context);

            _eventMediator.Publish(message);
        }
        
        public void ToListening(Command.Endpoint.IInstance commandEndpoint, Values.Version version)
        {
            Context.IListen context = _contextFactory.ForListen(commandEndpoint, version);
            Event.Transition message = _eventFactory.ForStateTransition(context);

            _eventMediator.Publish(message);
        }

        public void ToFaulted(Command.Endpoint.IInstance commandEndpoint, Exception exception)
        {
            Context.IFault context = _contextFactory.ForFault(commandEndpoint, exception);
            Event.Transition message = _eventFactory.ForStateTransition(context);

            _eventMediator.Publish(message);
        }
    }
}
