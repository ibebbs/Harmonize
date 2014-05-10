using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapping = AutoMapper.Mapper;

namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public interface IMapper
    {
        Message ToSchema(IMessage message);

        IMessage ToMessage(Message schema);
    }

    internal class Mapper : IMapper
    {
        static Mapper()
        {
            Mapping.CreateMap<With.Component.IActionable, Actionable>();
            Mapping.CreateMap<With.Component.IDescription, Description>();
            Mapping.CreateMap<With.Component.IEntity, Entity>();
            Mapping.CreateMap<With.Component.IEntityDescription, EntityDescription>();
            Mapping.CreateMap<With.Component.IIdentity, Identity>();
            Mapping.CreateMap<With.Component.ILocation, Location>();
            Mapping.CreateMap<With.Component.IMeasurement, Measurement>();
            Mapping.CreateMap<With.Component.IObservable, Observable>();
            Mapping.CreateMap<With.Component.IParameter, Parameter>();
            Mapping.CreateMap<With.Component.IParameterDescription, ParameterDescription>();
            Mapping.CreateMap<With.Component.IParameterValue, ParameterValue>();
            Mapping.CreateMap<With.Component.IValueDescription, ValueDescription>();

            Mapping.CreateMap<With.Message.IAction, Action>();
            Mapping.CreateMap<With.Message.IDeregister, Deregister>();
            Mapping.CreateMap<With.Message.IIgnore, Ignore>();
            Mapping.CreateMap<With.Message.IObservation, Observation>();
            Mapping.CreateMap<With.Message.IObserve, Observe>();
            Mapping.CreateMap<With.Message.IRegister, Register>();
        }

        internal static void ValidateMapping()
        {
            Mapping.AssertConfigurationIsValid();
        }

        private Message PerformMapping(IAction source)
        {
            return Mapping.Map<IAction, Action>(source);
        }

        private Message PerformMapping(IDeregister source)
        {
            return Mapping.Map<IDeregister, Deregister>(source);
        }

        private Message PerformMapping(IIgnore source)
        {
            return Mapping.Map<IIgnore, Ignore>(source);
        }

        private Message PerformMapping(IObservation source)
        {
            return Mapping.Map<IObservation, Observation>(source);
        }

        private Message PerformMapping(IObserve source)
        {
            return Mapping.Map<IObserve, Observe>(source);
        }

        private Message PerformMapping(IRegister source)
        {
            return Mapping.Map<IRegister, Register>(source);
        }

        private Message PerformMapping(IMessage source)
        {
            throw new InvalidOperationException(string.Format("Unknown message type: '{0}'", source.GetType().Name));
        }

        public Message ToSchema(IMessage message)
        {
            dynamic dynamicMessage = message;

            return PerformMapping(dynamicMessage);
        }

        public IMessage ToMessage(Message schema)
        {
            return schema;
        }

        public Component.IIdentity ToComponent(Identity identity)
        {
            return identity;
        }
    }
}
