using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bebbs.Harmonize.With.Messaging.Via.SignalR.Common;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    public interface IHarmonizeConnector
    {
        Task Initialize();

        Task Cleanup();

        void Register(string connectionId, Common.Dto.Identity registrar, Common.Dto.Entity entity, Action<string, Common.Dto.Identity, Common.Dto.Identity, Common.Dto.Message> process);

        void Observe(string connectionId, Common.Dto.Identity registrar, Common.Dto.Identity entity, Common.Dto.Identity source, Common.Dto.Identity observable);

        void Deregister(string connectionId, Common.Dto.Identity registrar, Common.Dto.Entity entity);
    }

    internal class HarmonizeConnector : IHarmonizeConnector
    {
        private readonly Registration.IFactory _registrationFactory;
        private readonly Client.IEndpoint _messagingEndpoint;

        private readonly Registration.Collection _registrations;

        public HarmonizeConnector(Registration.IFactory registrationFactory, With.Messaging.Client.IEndpoint messagingEndpoint)
        {
            _registrationFactory = registrationFactory;
            _messagingEndpoint = messagingEndpoint;

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

        public void Register(string connectionId, Common.Dto.Identity registrar, Common.Dto.Entity entity, Action<string, Common.Dto.Identity, Common.Dto.Identity, Common.Dto.Message> process)
        {
            Registration.IInstance registration = _registrationFactory.For(connectionId, registrar, entity, process);

            _registrations.Add(registration);

            _messagingEndpoint.Register(registration.Registrar, registration.Entity, registration.Consumer);
        }

        public void Observe(string connectionId, Common.Dto.Identity registrar, Common.Dto.Identity entity, Common.Dto.Identity source, Common.Dto.Identity observable)
        {
            string registrationKey = Registration.Key.For(connectionId, registrar, entity);

            Registration.IInstance registration;

            if (_registrations.TryGetValue(registrationKey, out registration))
            {
                _messagingEndpoint.Observe(entity.AsComponent(), source.AsComponent(), observable.AsComponent());
            }
        }

        public void Deregister(string connectionId, Common.Dto.Identity registrar, Common.Dto.Entity entity)
        {
            string registrationKey = Registration.Key.For(connectionId, registrar, entity.Identity);

            Registration.IInstance registration;

            if (_registrations.TryGetValue(registrationKey, out registration))
            {
                _messagingEndpoint.Deregister(registration.Registrar, registration.Entity);

                _registrations.Remove(registration);
            }
        }
    }
}
