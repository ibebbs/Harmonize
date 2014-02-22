using ServiceStack.Text;

namespace Bebbs.Harmonize.With.Harmony.Hub.Configuration
{
    public interface IParser
    {
        IValues FromJson(string location, string json);
    }

    internal class Parser : IParser
    {
        public IValues FromJson(string hubName, string json)
        {
            IValues values = JsonSerializer.DeserializeFromString<Values>(json);

            SetDeviceLocation(hubName, values);

            return values;
        }

        private void SetDeviceLocation(string hubName, IValues values)
        {
            values.Devices.ForEach(device => device.HubName = hubName);
        }
    }
}
