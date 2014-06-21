using Bebbs.Harmonize.With.Component;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common.Routing
{
    public interface IKey
    {
        string ForRegistration();

        string ForComponent();

        string ForObserve();

        string ForObserving(IIdentity entity, IIdentity observable);

        string ForObservation();

        string ForRegistrationOf(IIdentity entity);

        string ForDeregistrationOf(IIdentity entity);

        string ForAdditionOf(IIdentity component);

        string ForRemovalOf(IIdentity component);

        string ForObservation(IIdentity entity, IIdentity observable);

        string ForObservationOf(IIdentity entity, IIdentity observable);

        string ForAllActions();

        string ForActionBy(IIdentity entity, IIdentity actionable);

        string ForActionOf(IIdentity entity);
    }

    internal class Key : IKey
    {
        private const string RegistrationRoot = "REGISTRATION";
        private const string DeregistrationRoot = "REGISTRATION";
        private const string ComponentRoot = "COMPONENT";
        private const string ObservationRoot = "OBSERVATION";
        private const string ObserveRoot = "OBSERVE";
        private const string ActionRoot = "ACTION";
        private const string SingleWildCard = "*";
        private const string MultiWildCard = "#";

        public string ForRegistration()
        {
            return string.Format("{0}.{1}", RegistrationRoot, MultiWildCard);
        }

        public string ForComponent()
        {
            return string.Format("{0}.{1}", ComponentRoot, MultiWildCard);
        }

        public string ForObserve()
        {
            return string.Format("{0}.{1}", ObserveRoot, MultiWildCard);
        }

        public string ForObservation()
        {
            return string.Format("{0}.{1}", ObservationRoot, MultiWildCard);
        }

        public string ForObserving(IIdentity entity, IIdentity observable)
        {
            return string.Format("{0}.{1}.{2}", ObserveRoot, entity.Value, observable.Value);
        }

        public string ForRegistrationOf(IIdentity entity)
        {
            return string.Format("{0}.{1}", RegistrationRoot, entity.Value);
        }

        public string ForDeregistrationOf(IIdentity entity)
        {
            return string.Format("{0}.{1}", DeregistrationRoot, entity.Value);
        }

        public string ForAdditionOf(IIdentity component)
        {
            return string.Format("{0}.{1}", ComponentRoot, component.Value);
        }

        public string ForRemovalOf(IIdentity component)
        {
            return string.Format("{0}.{1}", ComponentRoot, component.Value);
        }

        public string ForObservation(IIdentity entity, IIdentity observable)
        {
            return string.Format("{0}.{1}.{2}", ObservationRoot, entity.Value, observable.Value);
        }

        public string ForObservationOf(IIdentity entity, IIdentity observable)
        {
            return string.Format("{0}.{1}.{2}", ObservationRoot, entity.Value, observable.Value);
        }

        public string ForAllActions()
        {
            return string.Format("{0}.#", ActionRoot);
        }

        public string ForActionBy(IIdentity entity, IIdentity actionable)
        {
            return string.Format("{0}.{1}.{2}", ActionRoot, entity.Value, actionable.Value);
        }

        public string ForActionOf(IIdentity entity)
        {
            return string.Format("{0}.{1}.{2}", ActionRoot, entity.Value, SingleWildCard);
        }
    }
}
