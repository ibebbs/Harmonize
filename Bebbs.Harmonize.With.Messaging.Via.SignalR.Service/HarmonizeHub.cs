using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    public class HarmonizeHub : Hub
    {
        /// <summary>
        /// Registers the specified <see cref="Component.IEntity"/> and ensures messages destined for the entity
        /// are routed to the specified consumer
        /// </summary>
        /// <param name="registrar">The host registering the entity</param>
        /// <param name="entity">The entity to register</param>
        public void Register(With.Component.IIdentity registrar, With.Component.IEntity entity)
        {
            // TODO: Implement
        }

        /// <summary>
        /// Deregisters the specified <see cref="Component.IEntity"/> and ensures that messages are no longer
        /// received for the entity
        /// </summary>
        /// <param name="registrar">The host deregistering the entity</param>
        /// <param name="entity"></param>
        public void Deregister(With.Component.IIdentity registrar, With.Component.IEntity entity)
        {
            // TODO: Implement
        }

        /// <summary>
        /// Publishes a message for the specified observation
        /// </summary>
        /// <param name="observation"></param>
        public void Publish(With.Component.IIdentity entity, With.Component.IIdentity observable, DateTimeOffset date, With.Component.IMeasurement measurement)
        {
            // TODO: Implement
        }

        /// <summary>
        /// Publishes a message to perform the specified action
        /// </summary>
        /// <param name="action"></param>
        public void Perform(With.Component.IIdentity actor, With.Component.IIdentity entity, With.Component.IIdentity actionable, IEnumerable<With.Component.IParameterValue> parameterValues)
        {
            // TODO: Implement
        }
    }
}
