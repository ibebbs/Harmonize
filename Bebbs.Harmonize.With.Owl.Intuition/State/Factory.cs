using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.State
{
    public interface IFactory
    {
        IState For(Context.IContext context);
    }

    internal class Factory : IFactory
    {
        private readonly Event.IMediator _eventMediator;
        private readonly ITransition _transition;
        private readonly Command.Endpoint.IFactory _commandEndpointFactory;
        private readonly Packet.Endpoint.IFactory _packetEndpointFactory;

        public Factory(Event.IMediator eventMediator, ITransition transition, Command.Endpoint.IFactory commandEndpointFactory, Packet.Endpoint.IFactory packetEndpointFactory)
        {
            _eventMediator = eventMediator;
            _transition = transition;
            _commandEndpointFactory = commandEndpointFactory;
            _packetEndpointFactory = packetEndpointFactory;
        }

        private IState ForContext(Context.IDisconnected context)
        {
            return new Disconnected(_eventMediator, _transition);
        }

        private IState ForContext(Context.IConnection context)
        {
            return new Connecting(_eventMediator, _transition, _commandEndpointFactory, context);
        }

        private IState ForContext(Context.IConfiguration context)
        {
            return new Configuring(_eventMediator, _transition, context);
        }

        private IState ForContext(Context.IRegistration context)
        {
            return new Registering(_eventMediator, _transition, context);
        }

        private IState ForContext(Context.IListen context)
        {
            return new Listening(_eventMediator, _transition, _packetEndpointFactory, context);
        }

        private IState ForContext(Context.IFault context)
        {
            return new Faulted(_eventMediator, _transition, context);
        }

        private IState ForContext(Context.IContext context)
        {
            throw new InvalidOperationException(string.Format("Unable to determine state for context of type '{0}'", context.GetType().FullName));
        }

        public IState For(Context.IContext context)
        {
            dynamic dynamicContext = context;

            return ForContext(dynamicContext);
        }
    }
}
