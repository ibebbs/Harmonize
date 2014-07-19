using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Common
{
    public static class Mapping
    {
        static Mapping()
        {
            Mapper.CreateMap<Dto.Identity, With.Component.Identity>().ConstructUsing(identity => new With.Component.Identity(identity.Value));
            Mapper.CreateMap<Dto.Entity, With.Component.Entity>().ConstructUsing(entity => new With.Component.Entity(
                    Mapper.Map<With.Component.Identity>(entity.Identity),
                    null,
                    null,
                    null
                )
            ).IgnoreAllPropertiesWithAnInaccessibleSetter();

            Mapper.CreateMap<Dto.MeasurementType, With.Component.MeasurementType>();
            Mapper.CreateMap<Dto.Measurement, With.Component.Measurement>();

            Mapper.CreateMap<Dto.Observation, With.Message.Observation>()
                  .ConstructUsing(observation => new With.Message.Observation(
                      Mapper.Map<With.Component.Identity>(observation.Entity),
                      Mapper.Map<With.Component.Identity>(observation.Observable),
                      observation.Date,
                      Mapper.Map<With.Component.Measurement>(observation.Measurement)
                    )
            ).IgnoreAllPropertiesWithAnInaccessibleSetter();


            Mapper.CreateMap<With.Component.IIdentity, Dto.Identity>();
            Mapper.CreateMap<With.Component.Entity, Dto.Entity>();

            Mapper.CreateMap<With.Component.MeasurementType, Dto.MeasurementType>();
            Mapper.CreateMap<With.Component.IMeasurement, Dto.Measurement>();
            Mapper.CreateMap<With.Message.IObservation, Dto.Observation>();

            Mapper.AssertConfigurationIsValid();
        }

        public static With.Component.IIdentity AsComponent(this Dto.Identity identity)
        {
            return Mapper.Map<With.Component.Identity>(identity);
        }

        public static Dto.Identity AsDto(this With.Component.IIdentity identity)
        {
            return Mapper.Map<Dto.Identity>(identity);
        }

        public static With.Component.IEntity AsComponent(this Dto.Entity entity)
        {
            return Mapper.Map<With.Component.Entity>(entity);
        }

        public static Dto.Entity AsDto(this With.Component.IEntity entity)
        {
            return Mapper.Map<Dto.Entity>(entity);
        }

        public static With.Message.IObservation AsMessage(this Common.Dto.Observation observation)
        {
            return Mapper.Map<With.Message.Observation>(observation);
        }

        public static Common.Dto.Observation AsDto(this With.Message.IObservation observation)
        {
            return Mapper.Map<Common.Dto.Observation>(observation);
        }

        public static With.Message.IMessage AsMessage(this Common.Dto.Message message)
        {
            throw new ArgumentException(string.Format("Unable to map message of type: '{0}'", message.GetType().Name), "message");
        }

        public static Common.Dto.Message AsDto(this With.Message.IMessage message)
        {
            throw new ArgumentException(string.Format("Unable to map message of type: '{0}'", message.GetType().Name), "message");
        }

        public static With.Message.IMessage AsDynamicMessage(this Common.Dto.Message message)
        {
            dynamic dynamicMessage = message;

            var result = AsMessage(dynamicMessage);

            return result;
        }

        public static Common.Dto.Message AsDynamicDto(this With.Message.IMessage message)
        {
            dynamic dynamicMessage = message;

            var result = AsDto(dynamicMessage);

            return result;
        }
    }
}
