using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    public interface IHarmonizeConnector
    {
        Task Initialize();

        Task Cleanup();

        void Register(string connectionId, Common.Entity entity, Action<string, Common.Message> process);

        void Observe(string connectionId, Common.Identity entity, Common.Identity source, Common.Identity observable);

        void Deregister(string connectionId, Common.Identity entity);
    }

    internal class HarmonizeConnector : IHarmonizeConnector
    {
        private readonly Registration.IFactory _registrationFactory;
        private readonly Client.IEndpoint _messagingEndpoint;
        private readonly IMapper _mapper;

        private readonly Registration.Collection _registrations;

        public HarmonizeConnector(Registration.IFactory registrationFactory, With.Messaging.Client.IEndpoint messagingEndpoint, IMapper mapper)
        {
            _registrationFactory = registrationFactory;
            _messagingEndpoint = messagingEndpoint;
            _mapper = mapper;

            _registrations = new Registration.Collection();
        }

        public Task Initialize()
        {
            return _messagingEndpoint.Initialize();
        }

        public Task Cleanup()
        {
            return _messagingEndpoint.Cleanup();
        }

        public void Register(string connectionId, Common.Entity entity, Action<string, Common.Message> process)
        {
            Registration.IInstance registration = _registrationFactory.For(connectionId, entity, process);

            _registrations.Add(registration);

            _messagingEndpoint.Register(registration.Registrar, registration.Entity, registration.Consumer);
        }

        public void Observe(string connectionId, Common.Identity entity, Common.Identity source, Common.Identity observable)
        {
            string registrationKey = Registration.Key.For(connectionId, entity);

            Registration.IInstance registration;

            if (_registrations.TryGetValue(registrationKey, out registration))
            {
                _messagingEndpoint.Observe(registration.Registrar, _mapper.Map(source), _mapper.Map(observable));
            }
        }

        public void Deregister(string connectionId, Common.Identity entity)
        {
            string registrationKey = Registration.Key.For(connectionId, entity);

            Registration.IInstance registration;

            if (_registrations.TryGetValue(registrationKey, out registration))
            {
                _messagingEndpoint.Deregister(registration.Registrar, registration.Entity);

                _registrations.Remove(registration);
            }
        }
    }
}
