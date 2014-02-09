using AutoMapper;
using System;

namespace Bebbs.Harmonize.With.Messaging
{
    public interface IMapping
    {
        Message.IRegister ToComponent(Schema.Register message);

        Message.IDeregister ToComponent(Schema.Deregister message);

        Message.IObserve ToComponent(Schema.Observe message);

        Message.ISubscribe ToComponent(Schema.Subscribe message);

        Message.IAct ToComponent(Schema.Act message);

        Schema.Register ToMessage(Message.IRegister message);

        Schema.Deregister ToMessage(Message.IDeregister message);

        Schema.Observation ToMessage(Message.IObservation message);

        Schema.Act ToMessage(Message.IAct message);
    }

    internal class Mapping : IMapping
    {
        static Mapping()
        {
            Mapper.CreateMap<Schema.MeasurementType, Component.MeasurementType>();
            Mapper.CreateMap<Schema.Measurement, Component.IMeasurement>();

            Mapper.CreateMap<Schema.SimpleDescription, Component.IDescription>();
            Mapper.CreateMap<Schema.EntityDescription, Component.IEntityDescription>();
            Mapper.CreateMap<Schema.ValueDescription, Component.IValueDescription>();
            Mapper.CreateMap<Schema.ParameterDescription, Component.IParameterDescription>();

            Mapper.CreateMap<Schema.Entity, Component.IEntity>();
            Mapper.CreateMap<Schema.Observable, Component.IObservable>();
            Mapper.CreateMap<Schema.Actionable, Component.IActionable>();
            Mapper.CreateMap<Schema.Parameter, Component.IParameter>();
            Mapper.CreateMap<Schema.ParameterValue, Component.IParameterValue>();

            Mapper.CreateMap<Schema.Register, Message.IRegister>();
            Mapper.CreateMap<Schema.Deregister, Message.IDeregister>();
            Mapper.CreateMap<Schema.Observe, Message.IObserve>();
            Mapper.CreateMap<Schema.Subscribe, Message.ISubscribe>();
            Mapper.CreateMap<Schema.Act, Message.IAct>();

            Mapper.CreateMap<Message.IRegister, Schema.Register>();
            Mapper.CreateMap<Message.IDeregister, Schema.Deregister>();
            Mapper.CreateMap<Message.IObservation, Schema.Observation>();
            Mapper.CreateMap<Message.IAct, Schema.Act>();
        }

        public Message.IRegister ToComponent(Schema.Register message)
        {
            return Mapper.Map<Schema.Register, Message.IRegister>(message);
        }

        public Message.IDeregister ToComponent(Schema.Deregister message)
        {
            return Mapper.Map<Schema.Deregister, Message.IDeregister>(message);
        }

        public Message.IObserve ToComponent(Schema.Observe message)
        {
            return Mapper.Map<Schema.Observe, Message.IObserve>(message);
        }

        public Message.ISubscribe ToComponent(Schema.Subscribe message)
        {
            return Mapper.Map<Schema.Subscribe, Message.ISubscribe>(message);
        }

        public Message.IAct ToComponent(Schema.Act message)
        {
            return Mapper.Map<Schema.Act, Message.IAct>(message);
        }

        public Schema.Register ToMessage(Message.IRegister message)
        {
            return Mapper.Map<Message.IRegister, Schema.Register>(message);
        }

        public Schema.Deregister ToMessage(Message.IDeregister message)
        {
            return Mapper.Map<Message.IDeregister, Schema.Deregister>(message);
        }

        public Schema.Observation ToMessage(Message.IObservation message)
        {
            return Mapper.Map<Message.IObservation, Schema.Observation>(message);
        }

        public Schema.Act ToMessage(Message.IAct message)
        {
            return Mapper.Map<Message.IAct, Schema.Act>(message);
        }
    }
}
