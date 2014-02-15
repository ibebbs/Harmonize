using AutoMapper;
using System;

namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    public interface IHelper
    {
        Component.IIdentity ToComponent(Schema.UniqueIdentifier identifier);

        Message.IRegister ToComponent(Schema.Register message);

        Message.IDeregister ToComponent(Schema.Deregister message);

        Message.IObserve ToComponent(Schema.Observe message);

        Message.ISubscribe ToComponent(Schema.Subscribe message);

        Message.IAct ToComponent(Schema.Act message);

        Schema.Register ToSchema(Message.IRegister message);

        Schema.Deregister ToSchema(Message.IDeregister message);

        Schema.Observation ToSchema(Message.IObservation message);

        Schema.Act ToSchema(Message.IAct message);
    }

    internal class Helper : IHelper
    {
        private static readonly DateTimeOffset Base = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        private static void CreateMapping<TSource, TDest, TImplementation>()
        {
            Mapper.CreateMap<TSource, TImplementation>();
            Mapper.CreateMap<TSource, TDest>().As<TImplementation>();
        }

        private static void CreateMapping<TSource, TDest, TImplementation>(Action<IMappingExpression<TSource, TImplementation>> mapping)
        {
            mapping(Mapper.CreateMap<TSource, TImplementation>());
            Mapper.CreateMap<TSource, TDest>().As<TImplementation>();
        }

        private class DateResolver : ValueResolver<Message.IObservation, long>
        {
            protected override long ResolveCore(Message.IObservation source)
            {
                return Convert.ToInt64(source.Date.Subtract(Base).TotalMilliseconds);
            }
        }

        static Helper()
        {
            Mapper.CreateMap<Schema.MeasurementType, Component.MeasurementType>();

            CreateMapping<Schema.UniqueIdentifier, Component.IIdentity, Identity>();
            CreateMapping<Schema.Measurement, Component.IMeasurement, Measurement>();
            CreateMapping<Schema.SimpleDescription, Component.IDescription, Description>();
            CreateMapping<Schema.EntityDescription, Component.IEntityDescription, EntityDescription>();
            CreateMapping<Schema.ValueDescription, Component.IValueDescription, ValueDescription>();
            CreateMapping<Schema.ParameterDescription, Component.IParameterDescription, ParameterDescription>();
            CreateMapping<Schema.Entity, Component.IEntity, Entity>(mapping => mapping.ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.UniqueIdentifier)));
            CreateMapping<Schema.Observable, Component.IObservable, Observable>(mapping => mapping.ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.UniqueIdentifier)));
            CreateMapping<Schema.Actionable, Component.IActionable, Actionable>(mapping => mapping.ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.UniqueIdentifier)));
            CreateMapping<Schema.Parameter, Component.IParameter, Parameter>(mapping => mapping.ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.UniqueIdentifier)));
            CreateMapping<Schema.ParameterValue, Component.IParameterValue, ParameterValue>(mapping => mapping.ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.UniqueIdentifier)));

            CreateMapping<Schema.Register, Message.IRegister, Register>();
            CreateMapping<Schema.Deregister, Message.IDeregister, Deregister>();
            CreateMapping<Schema.Observe, Message.IObserve, Observe>();
            CreateMapping<Schema.Subscribe, Message.ISubscribe, Subscribe>();
            CreateMapping<Schema.Act, Message.IAct, Act>();

            Mapper.CreateMap<Component.MeasurementType, Schema.MeasurementType>();
            Mapper.CreateMap<Component.IIdentity, Schema.UniqueIdentifier>().ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ToString()));
            Mapper.CreateMap<Component.IMeasurement, Schema.Measurement>()
                .ForMember(dest => dest.TypeSpecified, opt => opt.UseValue(true));
            Mapper.CreateMap<Component.IDescription, Schema.SimpleDescription>();
            Mapper.CreateMap<Component.IEntityDescription, Schema.EntityDescription>();
            Mapper.CreateMap<Component.IValueDescription, Schema.ValueDescription>();
            Mapper.CreateMap<Component.IParameterDescription, Schema.ParameterDescription>();
            Mapper.CreateMap<Component.IEntity, Schema.Entity>()
                .ForMember(dest => dest.UniqueIdentifier, opt => opt.MapFrom(src => src.Identity));
            Mapper.CreateMap<Component.IObservable, Schema.Observable>()
                .ForMember(dest => dest.UniqueIdentifier, opt => opt.MapFrom(src => src.Identity));
            Mapper.CreateMap<Component.IActionable, Schema.Actionable>()
                .ForMember(dest => dest.UniqueIdentifier, opt => opt.MapFrom(src => src.Identity));
            Mapper.CreateMap<Component.IParameter, Schema.Parameter>()
                .ForMember(dest => dest.UniqueIdentifier, opt => opt.MapFrom(src => src.Identity));
            Mapper.CreateMap<Component.IParameterValue, Schema.ParameterValue>()
                .ForMember(dest => dest.UniqueIdentifier, opt => opt.MapFrom(src => src.Identity));

            Mapper.CreateMap<Message.IRegister, Schema.Register>();
            Mapper.CreateMap<Message.IDeregister, Schema.Deregister>();
            Mapper.CreateMap<Message.IObservation, Schema.Observation>()
                .ForMember(dest => dest.Date, opt => opt.ResolveUsing<DateResolver>());
            Mapper.CreateMap<Message.IAct, Schema.Act>();

            Mapper.AssertConfigurationIsValid();
        }

        public Component.IIdentity ToComponent(Schema.UniqueIdentifier identifier)
        {
            return Mapper.Map<Schema.UniqueIdentifier, Component.IIdentity>(identifier);
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

        public Schema.Register ToSchema(Message.IRegister message)
        {
            return Mapper.Map<Message.IRegister, Schema.Register>(message);
        }

        public Schema.Deregister ToSchema(Message.IDeregister message)
        {
            return Mapper.Map<Message.IDeregister, Schema.Deregister>(message);
        }

        public Schema.Observation ToSchema(Message.IObservation message)
        {
            return Mapper.Map<Message.IObservation, Schema.Observation>(message);
        }

        public Schema.Act ToSchema(Message.IAct message)
        {
            return Mapper.Map<Message.IAct, Schema.Act>(message);
        }
    }
}
