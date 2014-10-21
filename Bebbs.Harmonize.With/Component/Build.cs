using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Component
{
    public static class Build
    {
        public static IEntityBuilder Entity()
        {
            throw new NotImplementedException();
        }
    }

    public interface IEntityBuilder
    {
        IIdentifiedEntityBuilder IdentifiedBy(string uniqueIdentity);
    }

    public interface IIdentifiedEntityBuilder
    {
        IIdentifiedEntityBuilder WithDescription(string name, string remarks, string type, string model, string manufacturer);

        IEntityObservableBuilder WithObservable();

        IEntityActionableBuilder WithActionable();

        IEntity Now();
    }

    public interface IEntityObservableBuilder
    {
        IEntityIdentifiedObservableBuilder IdentifiedBy(string uniqueIdentity);
    }

    public interface IEntityIdentifiedObservableBuilder : IIdentifiedEntityBuilder
    {
        IEntityIdentifiedObservableBuilder WithDescription(MeasurementType measurementType, string minimumValue, string maximumValue, string defaultValue);
    }

    public interface IEntityActionableBuilder
    {
        IEntityIdentifiedActionableBuilder IdentifiedBy(string uniqueIdentity);
    }

    public interface IEntityIdentifiedActionableBuilder : IIdentifiedEntityBuilder
    {
        IEntityIdentifiedObservableBuilder WithDescription(string name, string remarks);

        IEntityObservableParameterBuilder WithParameter();
    }

    public interface IEntityObservableParameterBuilder
    {
        IEntityObservableIdentifiedParameterBuilder IdentifiedBy(string uniqueIdentity);
    }

    public interface IEntityObservableIdentifiedParameterBuilder : IEntityIdentifiedActionableBuilder
    {
        IEntityIdentifiedObservableBuilder WithDescription(MeasurementType measurementType, string minimumValue, string maximumValue, string defaultValue, bool required);
    }
}
