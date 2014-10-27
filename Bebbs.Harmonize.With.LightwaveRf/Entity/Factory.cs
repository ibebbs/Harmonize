using EventSourceProxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.With.LightwaveRf.Entity
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-LightwaveRf-Entity-Factory")]
    public interface IFactory
    {
        ILightwaveEntity Create(Configuration.IDevice device, WifiLink.IBridge bridge, With.Messaging.Client.IEndpoint clientEndpoint);
    }

    internal class Factory : IFactory
    {
        private readonly IDictionary<Configuration.DeviceType, IBuilder> _builders;

        public Factory(IEnumerable<IBuilder> builders)
        {
            _builders = builders.ToDictionary(builder => builder.DeviceType);
        }

        public ILightwaveEntity Create(Configuration.IDevice device, WifiLink.IBridge bridge, With.Messaging.Client.IEndpoint clientEndpoint)
        {
            IBuilder builder;

            if (_builders.TryGetValue(device.Type, out builder))
            {
                return builder.Build(device, bridge, clientEndpoint);
            }
            else
            {
                throw new InvalidOperationException(string.Format("No builder for device type '{0}' found", device.Type));
            }
        }
    }
}
