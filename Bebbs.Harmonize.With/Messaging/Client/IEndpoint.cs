using System;
using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Messaging.Client
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
        /// <param name="registrar">The host registering the entity</param>
        /// <param name="entity">The entity to register</param>
        /// <param name="consumer">The consumer that will process messages for the entity</param>
        void Register(With.Component.IIdentity registrar, With.Component.IEntity entity, IObserver<Message.IMessage> consumer);

        /// <summary>
        /// Deregisters the specified <see cref="Component.IEntity"/> and ensures that messages are no longer
        /// received for the entity
        /// </summary>
        /// <param name="registrar">The host deregistering the entity</param>
        /// <param name="entity"></param>
        void Deregister(With.Component.IIdentity registrar, With.Component.IEntity entity);

        /// <summary>
        /// Publishes a message for the specified observation
        /// </summary>
        /// <param name="observation"></param>
        void Publish(With.Component.IIdentity entity, With.Component.IIdentity observable, DateTimeOffset date, With.Component.IMeasurement measurement);

        /// <summary>
        /// Publishes a message to perform the specified action
        /// </summary>
        /// <param name="action"></param>
        void Perform(With.Component.IIdentity actor, With.Component.IIdentity entity, With.Component.IIdentity actionable, IEnumerable<With.Component.IParameterValue> parameterValues);
    }
}
