using System;

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
        /// <param name="entity">The entity to register</param>
        /// <param name="consumer">The consumer that will process messages for the entity</param>
        void Register(Component.IEntity entity, IObserver<Message.IMessage> consumer);

        /// <summary>
        /// Deregisters the specified <see cref="Component.IEntity"/> and ensures that messages are no longer
        /// received for the entity
        /// </summary>
        /// <param name="entity"></param>
        void Deregister(Component.IEntity entity);

        /// <summary>
        /// Publishes the specified <see cref="Message.IObservation"/>
        /// </summary>
        /// <param name="observation"></param>
        void Publish(Message.IObservation observation);

        /// <summary>
        /// Performs the specified <see cref="Message.IAct"/>
        /// </summary>
        /// <param name="action"></param>
        void Perform(Message.IAct action);
    }
}
