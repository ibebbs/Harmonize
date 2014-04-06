
namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity.Observable
{
    internal class CurrentElectricityConsumptionFactory : IFactory
    {
        private Event.IMediator _eventMediator;
        private IClock _clock;

        public CurrentElectricityConsumptionFactory(Event.IMediator eventMediator, IClock clock)
        {
            _eventMediator = eventMediator;
            _clock = clock;
        }

        public IGatewayObservable ForEntity(IInstance instance)
        {
            return new CurrentElectricityConsumption(_eventMediator, _clock, instance);
        }

        public string DeviceType
        {
            get { return Gateway.DeviceType.IntuitionC; }
        }
    }
}
