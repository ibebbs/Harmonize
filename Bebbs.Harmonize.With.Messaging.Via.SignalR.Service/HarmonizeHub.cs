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

        private void Process(string connectionId, Common.Dto.Identity registrar, Common.Dto.Identity entity, Common.Dto.Message message)
        {
            dynamic client = Clients.Client(connectionId);

            if (client != null)
            {
                HarmonizeDispatcher.Dispatch(client, registrar, entity, message);
            }
        }

        /// <summary>
        /// Registers the specified <see cref="Component.IEntity"/> and ensures messages destined for the entity
        /// are routed to the specified consumer
        /// </summary>
        /// <param name="registrar">The host registering the entity</param>
        /// <param name="entity">The entity to register</param>
        public void Register(Common.Dto.Identity registrar, Common.Dto.Entity entity)
        {
            _connector.Register(Context.ConnectionId, registrar, entity, Process);
        }

        /// <summary>
        /// Observes the observable with the specified <see cref="Component.Identity"/> of the entity with the
        /// specified <see cref="Component.Identity"/>
        /// </summary>
        /// <param name="entity">The observer entity</param>
        /// <param name="source">The observed entity</param>
        /// <param name="observable">The observable</param>
        public void Observe(Common.Dto.Identity registrar, Common.Dto.Identity entity, Common.Dto.Identity source, Common.Dto.Identity observable)
        {
            _connector.Observe(Context.ConnectionId, registrar, entity, source, observable);
        }

        /// <summary>
        /// Deregisters the specified <see cref="Component.IEntity"/> and ensures that messages are no longer
        /// received for the entity
        /// </summary>
        /// <param name="registrar">The host deregistering the entity</param>
        /// <param name="entity"></param>
        public void Deregister(Common.Dto.Identity registrar, Common.Dto.Entity entity)
        {
            _connector.Deregister(Context.ConnectionId, registrar, entity);
        }
    }
}
