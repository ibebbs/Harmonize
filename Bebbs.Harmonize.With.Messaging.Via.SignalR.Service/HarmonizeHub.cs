using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    [HubName("HarmonizeHub")]
    public class HarmonizeHub : Hub
    {
        private readonly Registration.IFactory _registrationFactory;
        private readonly Client.IEndpoint _messagingEndpoint;

        private readonly Registration.Collection _registrations;

        public HarmonizeHub(Registration.IFactory registrationFactory, With.Messaging.Client.IEndpoint messagingEndpoint)
        {
            _registrationFactory = registrationFactory;
            _messagingEndpoint = messagingEndpoint;

            _registrations = new Registration.Collection();
        }

        private void Process(string connectionId, Common.Message message)
        {
            dynamic client = Clients.Client(connectionId);

            if (client != null)
            {
                client.Process(message);
            }
        }

        /// <summary>
        /// Registers the specified <see cref="Component.IEntity"/> and ensures messages destined for the entity
        /// are routed to the specified consumer
        /// </summary>
        /// <param name="registrar">The host registering the entity</param>
        /// <param name="entity">The entity to register</param>
        public void Register(Common.Entity entity)
        {
            Registration.IInstance registration = _registrationFactory.For(Context.ConnectionId, entity, Process);
            _registrations.Add(registration);

            _messagingEndpoint.Register(registration.Registrar, registration.Entity, registration.Consumer);
        }

        /// <summary>
        /// Deregisters the specified <see cref="Component.IEntity"/> and ensures that messages are no longer
        /// received for the entity
        /// </summary>
        /// <param name="registrar">The host deregistering the entity</param>
        /// <param name="entity"></param>
        public void Deregister(Common.Identity entity)
        {
            string registrationKey = Registration.Key.For(Context.ConnectionId, entity);

            Registration.IInstance registration;

            if (_registrations.TryGetValue(registrationKey, out registration))
            {
                _messagingEndpoint.Deregister(registration.Registrar, registration.Entity);

                _registrations.Remove(registration);
            }
        }
    }
}
