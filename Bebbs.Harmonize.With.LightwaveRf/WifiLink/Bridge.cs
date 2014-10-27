using EventSourceProxy;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.LightwaveRf.WifiLink
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-LightwaveRf-WifiLink-Bridge")]
    public interface IBridge
    {
        Task Initialize();

        Task CleanUp();

        Task TurnDeviceOn(byte roomId, byte deviceId, string displayLine1, string displayLine2);

        Task TurnDeviceOff(byte roomId, byte deviceId, string displayLine1, string displayLine2);

        Task SetDimmerLevel(byte roomId, byte deviceId, byte level, string displayLine1, string displayLine2);
    }

    internal class Bridge : IBridge
    {
        private readonly ICommandEndpoint _commandEndpoint;
        private readonly IQueryEndpoint _queryEndpoint;

        public Bridge(ICommandEndpoint commandEndpoint, IQueryEndpoint queryEndpoint)
        {
            _commandEndpoint = commandEndpoint;
            _queryEndpoint = queryEndpoint;
        }

        public async Task Initialize()
        {
            await _commandEndpoint.Connect();
            await _queryEndpoint.Connect();
        }

        public async Task CleanUp()
        {
            await _queryEndpoint.Disconnect();
            await _commandEndpoint.Disconnect();
        }

        public Task TurnDeviceOn(byte roomId, byte deviceId, string displayLine1, string displayLine2)
        {
            return _commandEndpoint.TurnDeviceOn(roomId, deviceId, displayLine1, displayLine2);
        }

        public Task TurnDeviceOff(byte roomId, byte deviceId, string displayLine1, string displayLine2)
        {
            return _commandEndpoint.TurnDeviceOff(roomId, deviceId, displayLine1, displayLine2);
        }

        public Task SetDimmerLevel(byte roomId, byte deviceId, byte level, string displayLine1, string displayLine2)
        {
            return _commandEndpoint.SetDimmerLevel(roomId, deviceId, level, displayLine1, displayLine2);
        }
    }
}
