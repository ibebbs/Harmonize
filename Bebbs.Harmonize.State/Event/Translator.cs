using EventStore.ClientAPI;
using ServiceStack.Text;
using System;
using System.Text;

namespace Bebbs.Harmonize.State.Event
{
    public interface ITranslator
    {
        EventData Translate(With.Message.IObservation message);

        EventData Translate(With.Message.IStopped message);

        EventData Translate(With.Message.IStarted message);

        EventData Translate(With.Message.IDeregister message);

        EventData Translate(With.Message.IRegister message);
    }

    internal class Translator : ITranslator
    {
        private readonly IMapper _mapper;
        private readonly ISerializer _serializer;
        private readonly IEncoder _encoder;

        public Translator(IMapper mapper, ISerializer serializer, IEncoder encoder)
        {
            _mapper = mapper;
            _serializer = serializer;
            _encoder = encoder;
        }

        public EventData Translate(With.Message.IObservation message)
        {
            Observed value = _mapper.Map(message);

            string text = _serializer.Serialize(value);

            byte[] data = _encoder.Encode(text);

            return new EventData(Guid.NewGuid(), typeof(Observed).FullName, true, data, new byte[0]);
        }

        public EventData Translate(With.Message.IStopped message)
        {
            Stopped value = _mapper.Map(message);

            string text = _serializer.Serialize(value);

            byte[] data = _encoder.Encode(text);

            return new EventData(Guid.NewGuid(), typeof(Stopped).FullName, true, data, new byte[0]);
        }

        public EventData Translate(With.Message.IStarted message)
        {
            Started value = _mapper.Map(message);

            string text = _serializer.Serialize(value);

            byte[] data = _encoder.Encode(text);

            return new EventData(Guid.NewGuid(), typeof(Started).FullName, true, data, new byte[0]);
        }

        public EventData Translate(With.Message.IDeregister message)
        {
            Deregistered value = _mapper.Map(message);

            string text = _serializer.Serialize(value);

            byte[] data = _encoder.Encode(text);

            return new EventData(Guid.NewGuid(), typeof(Deregistered).FullName, true, data, new byte[0]);
        }

        public EventData Translate(With.Message.IRegister message)
        {
            Registered value = _mapper.Map(message);

            string text = _serializer.Serialize(value);

            byte[] data = _encoder.Encode(text);

            return new EventData(Guid.NewGuid(), typeof(Registered).FullName, true, data, new byte[0]);
        }
    }
}
