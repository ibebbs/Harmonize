using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.State
{
    internal class Listening : IState
    {
        private readonly Event.IMediator _eventMediator;
        private readonly ITransition _transition;
        private readonly Packet.Endpoint.IFactory _packetEndpointFactory;
        private readonly Context.IListen _context;

        private Packet.Endpoint.IInstance _packetEndpoint;
        private IDisposable _subscription;

        public Listening(Event.IMediator eventMediator, ITransition transition, Packet.Endpoint.IFactory packetEndpointFactory, Context.IListen context)
        {
            _eventMediator = eventMediator;
            _transition = transition;
            _packetEndpointFactory = packetEndpointFactory;
            _context = context;
        }

        private void Publish(Packet.IPacket packet)
        {
        }

        public void OnEnter()
        {
            _packetEndpoint = _packetEndpointFactory.CreateEndpoint();
            _subscription = _packetEndpoint.Packets.Subscribe(Publish);

            _packetEndpoint.Open();

            _eventMediator.Publish(new Event.Started());
        }

        public void OnExit()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            if (_packetEndpoint != null)
            {
                _packetEndpoint.Close();
                _packetEndpoint.Dispose();
                _packetEndpoint = null;
            }
        }

        public Name Name
        {
            get { return Name.Listening; }
        }
    }
}
