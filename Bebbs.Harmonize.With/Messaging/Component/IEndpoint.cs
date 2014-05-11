using System;
using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Messaging.Component
{
    /// <summary>
    /// Represents a messaging endpoint that can be used to interact with harmonize
    /// </summary>
    public interface IEndpoint : Common.IEndpoint
    {
        /// <summary>
        /// Registers the specified <see cref="Component.IEntity"/> and ensures messages destined for the entity
        /// are routed to the specified consumer
        /// </summary>
        /// <param name="registrar">The host adding the component</param>
        /// <param name="component">The entity to register</param>
        /// <param name="consumer">The consumer that will process messages for the entity</param>
        void Add(With.Component.IIdentity registrar, With.Component.IComponent component, IObserver<Message.IMessage> consumer);

        /// <summary>
        /// Deregisters the specified <see cref="Component.IEntity"/> and ensures that messages are no longer
        /// received for the entity
        /// </summary>
        /// <param name="registrar">The host removing the component</param>
        /// <param name="component"></param>
        void Remove(With.Component.IIdentity registrar, With.Component.IIdentity component);
    }
}
