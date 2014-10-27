using EventSourceProxy;

namespace Bebbs.Harmonize.With.LightwaveRf.Entity
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-LightwaveRf-Entity-Builder")]
    public interface IBuilder
    {
        ILightwaveEntity Build(Configuration.IDevice device, WifiLink.IBridge bridge, With.Messaging.Client.IEndpoint clientEndpoint);

        Configuration.DeviceType DeviceType { get; }
    }
}
