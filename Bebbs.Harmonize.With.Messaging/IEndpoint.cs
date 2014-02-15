using System;

namespace Bebbs.Harmonize.With.Messaging
{
    public interface IEndpoint
    {
        void Initialize();
        void Cleanup();
    }

    public interface IMessageEndpoint : IEndpoint
    {
        void Publish(Schema.Message message);

        IObservable<Schema.Message> Messages { get; }
    }

    public interface ISerializedEndpoint : IEndpoint
    {
        void Publish(string message);

        IObservable<string> Messages { get; }
    }
}
