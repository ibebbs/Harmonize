using EventSourceProxy;
using System;

namespace Bebbs.Harmonize.With.LightwaveRf.Dimmer
{    
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-LightwaveRf-Dimmer-Builder")]
    public interface IBuilder : LightwaveRf.Entity.IBuilder { }

    internal class Builder : IBuilder
    {
        public ILightwaveEntity Build(Configuration.IDevice device, WifiLink.IBridge bridge, With.Messaging.Client.IEndpoint clientEndpoint)
        {
            Configuration.Dimmer dimmer = device as Configuration.Dimmer;

            if (dimmer != null)
            {
                return new Entity(dimmer, bridge, clientEndpoint);
            }
            else
            {
                throw new InvalidOperationException(string.Format("Could not create dimmer from device '{0}'", device));
            }
        }

        public Configuration.DeviceType DeviceType
        {
            get { return Configuration.DeviceType.Dimmer; }
        }
    }
}
