using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeMapper = AutoMapper.Mapper;

namespace Bebbs.Harmonize.State.Event
{
    public interface IMapper
    {
        Observed Map(With.Message.IObservation message);
        Stopped Map(With.Message.IStopped message);
        Started Map(With.Message.IStarted message);
        Deregistered Map(With.Message.IDeregister message);
        Registered Map(With.Message.IRegister message);
    }

    internal class Mapper : IMapper
    {
        private static readonly DateTimeOffset Base = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        private static void CreateMapping<TSource, TDest, TImplementation>()
        {
            TypeMapper.CreateMap<TSource, TImplementation>();
            TypeMapper.CreateMap<TSource, TDest>().As<TImplementation>();
        }

        private static void CreateMapping<TSource, TDest, TImplementation>(Action<IMappingExpression<TSource, TImplementation>> mapping)
        {
            mapping(TypeMapper.CreateMap<TSource, TImplementation>());
            TypeMapper.CreateMap<TSource, TDest>().As<TImplementation>();
        }

        static Mapper()
        {
            TypeMapper.CreateMap<With.Message.IObservation, Observed>();
            TypeMapper.CreateMap<With.Message.IStopped, Stopped>();
            TypeMapper.CreateMap<With.Message.IStarted, Started>();
            TypeMapper.CreateMap<With.Message.IDeregister, Deregistered>();
            TypeMapper.CreateMap<With.Message.IRegister, Registered>();

            //TypeMapper.CreateMap<With.Component.MeasurementType, Schema.MeasurementType>();
            TypeMapper.CreateMap<With.Component.IIdentity, Component.Identity>().ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ToString()));
            TypeMapper.CreateMap<With.Component.IDescription, Component.Description>();
            TypeMapper.CreateMap<With.Component.IEntity, Component.Entity>().ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.Identity));
            TypeMapper.CreateMap<With.Component.IEntityDescription, Component.EntityDescription>();
            TypeMapper.CreateMap<With.Component.IMeasurement, Component.Measurement>();
            TypeMapper.CreateMap<With.Component.IValueDescription, Component.ValueDescription>();
            TypeMapper.CreateMap<With.Component.IObservable, Component.Observable>().ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.Identity));
            TypeMapper.CreateMap<With.Component.IParameter, Component.Parameter>().ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.Identity));
            TypeMapper.CreateMap<With.Component.IParameterDescription, Component.ParameterDescription>();
            TypeMapper.CreateMap<With.Component.IActionable, Component.Actionable>().ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.Identity));

            /*            
            TypeMapper.CreateMap<With.Component.IParameterValue, Schema.ParameterValue>()
                .ForMember(dest => dest.UniqueIdentifier, opt => opt.MapFrom(src => src.Identity));
            */

            TypeMapper.CreateMap<With.Message.IRegister, Event.Registered>();
            TypeMapper.CreateMap<With.Message.IDeregister, Event.Deregistered>();
            TypeMapper.CreateMap<With.Message.IObservation, Event.Observed>();
            TypeMapper.CreateMap<With.Message.IAct, Event.Action>();

            TypeMapper.AssertConfigurationIsValid();
        }

        internal TTarget Map<TSource,TTarget>(TSource source)
        {
            return TypeMapper.Map<TTarget>(source);
        }

        public Observed Map(With.Message.IObservation message)
        {
            return TypeMapper.Map<Observed>(message);
        }

        public Stopped Map(With.Message.IStopped message)
        {
            return TypeMapper.Map<Stopped>(message);
        }

        public Started Map(With.Message.IStarted message)
        {
            return TypeMapper.Map<Started>(message);
        }

        public Deregistered Map(With.Message.IDeregister message)
        {
            return TypeMapper.Map<Deregistered>(message);
        }

        public Registered Map(With.Message.IRegister message)
        {
            return TypeMapper.Map<Registered>(message);
        }
    }
}
