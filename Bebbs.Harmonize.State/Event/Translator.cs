using AutoMapper;
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
        private static readonly Encoding Encoding = Encoding.UTF8;

        static Translator()
        {
            Mapper.CreateMap<With.Message.IObservation, Observed>();
            Mapper.CreateMap<With.Message.IStopped, Stopped>();
            Mapper.CreateMap<With.Message.IStarted, Started>();
            Mapper.CreateMap<With.Message.IDeregister, Deregistered>();
            Mapper.CreateMap<With.Message.IRegister, Registered>();
        }

        public EventData Translate(With.Message.IObservation message)
        {
            Observed value = Mapper.Map<Observed>(message);

            string text = JsonSerializer.SerializeToString<Observed>(value);

            byte[] data = Encoding.GetBytes(text);

            return new EventData(Guid.NewGuid(), typeof(Observed).FullName, true, data, new byte[0]);
        }

        public EventData Translate(With.Message.IStopped message)
        {
            Stopped value = Mapper.Map<Stopped>(message);

            string text = JsonSerializer.SerializeToString<Stopped>(value);

            byte[] data = Encoding.GetBytes(text);

            return new EventData(Guid.NewGuid(), typeof(Stopped).FullName, true, data, new byte[0]);
        }

        public EventData Translate(With.Message.IStarted message)
        {
            Started value = Mapper.Map<Started>(message);

            string text = JsonSerializer.SerializeToString<Started>(value);

            byte[] data = Encoding.GetBytes(text);

            return new EventData(Guid.NewGuid(), typeof(Started).FullName, true, data, new byte[0]);
        }

        public EventData Translate(With.Message.IDeregister message)
        {
            Deregistered value = Mapper.Map<Deregistered>(message);

            string text = JsonSerializer.SerializeToString<Deregistered>(value);

            byte[] data = Encoding.GetBytes(text);

            return new EventData(Guid.NewGuid(), typeof(Deregistered).FullName, true, data, new byte[0]);
        }

        public EventData Translate(With.Message.IRegister message)
        {
            Registered value = Mapper.Map<Registered>(message);

            string text = JsonSerializer.SerializeToString<Registered>(value);

            byte[] data = Encoding.GetBytes(text);

            return new EventData(Guid.NewGuid(), typeof(Registered).FullName, true, data, new byte[0]);
        }
    }
}
