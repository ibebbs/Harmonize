using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    [HubName("HarmonizeHub")]
    public class HarmonizeHub : Hub
    {
        private readonly IHarmonizeConnector _connector;

        public HarmonizeHub(IHarmonizeConnector connector)
        {
            _connector = connector;
        }

        private void Process(string connectionId, Common.Identity entity, Common.Message message)
        {
            dynamic client = Clients.Client(connectionId);

            if (client != null)
            {
                client.Process(new Common.Identity { Value = connectionId }, entity, message);
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
            _connector.Register(Context.ConnectionId, entity, Process);
        }

        /// <summary>
        /// Observes the observable with the specified <see cref="Component.Identity"/> of the entity with the
        /// specified <see cref="Component.Identity"/>
        /// </summary>
        /// <param name="entity">The observer entity</param>
        /// <param name="source">The observed entity</param>
        /// <param name="observable">The observable</param>
        public void Observe(Common.Identity entity, Common.Identity source, Common.Identity observable)
        {
            _connector.Observe(Context.ConnectionId, entity, source, observable);
        }

        /// <summary>
        /// Deregisters the specified <see cref="Component.IEntity"/> and ensures that messages are no longer
        /// received for the entity
        /// </summary>
        /// <param name="registrar">The host deregistering the entity</param>
        /// <param name="entity"></param>
        public void Deregister(Common.Identity entity)
        {
            _connector.Deregister(Context.ConnectionId, entity);
        }
    }
}
