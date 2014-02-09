using System;

namespace Bebbs.Harmonize.With.Messaging
{
    public interface IEndpoint
    {
        void Publish(Schema.Message message);

        IObservable<Schema.Message> Messages { get; }
    }
}
