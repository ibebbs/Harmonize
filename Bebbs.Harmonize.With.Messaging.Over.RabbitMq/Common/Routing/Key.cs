using Bebbs.Harmonize.With.Component;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common.Routing
{
    public interface IKey
    {
        string ForRegistration();

        string ForObserve();

        string ForRegistrationOf(IIdentity entity);

        string ForDeregistrationOf(IIdentity entity);

        string ForObservationBy(IIdentity entity, IIdentity observable);

        string ForObservationOf(IIdentity entity, IIdentity observable);

        string ForActionBy(IIdentity entity, IIdentity actionable);

        string ForActionOf(IIdentity entity);
    }

    internal class Key : IKey
    {
        private const string RegistrationRoot = "REGISTRATION";
        private const string DeregistrationRoot = "REGISTRATION";
        private const string ObservationRoot = "OBSERVATION";
        private const string ActionRoot = "ACTION";
        private const string SingleWildCard = "*";
        private const string MultiWildCard = "#";

        public string ForRegistration()
        {
            return string.Format("{0}.{1}", RegistrationRoot, SingleWildCard);
        }

        public string ForObserve()
        {
            return string.Format("{0}.{1}", DeregistrationRoot, SingleWildCard);
        }

        public string ForRegistrationOf(IIdentity entity)
        {
            return string.Format("{0}.{1}", RegistrationRoot, entity.Value);
        }

        public string ForDeregistrationOf(IIdentity entity)
        {
            return string.Format("{0}.{1}", DeregistrationRoot, entity.Value);
        }

        public string ForObservationBy(IIdentity entity, IIdentity observable)
        {
            return string.Format("{0}.{1}.{2}", ObservationRoot, entity.Value, observable.Value);
        }

        public string ForObservationOf(IIdentity entity, IIdentity observable)
        {
            return string.Format("{0}.{1}.{2}", ObservationRoot, entity.Value, observable.Value);
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
