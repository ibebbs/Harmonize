
namespace Bebbs.Harmonize.Harmony.Command
{
    public class PowerOnCommand : ICommand
    {
        public PowerOnCommand(string deviceId)
        {
            DeviceId = deviceId;
        }

        public string DeviceId { get; private set; }
    }
}
