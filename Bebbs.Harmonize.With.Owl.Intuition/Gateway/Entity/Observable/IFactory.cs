using Bebbs.Harmonize.With.Component;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity.Observable
{
    public interface IFactory
    {
        IGatewayObservable ForEntity(IInstance instance);

        string DeviceType { get; }
    }
}
