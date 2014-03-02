using ServiceStack.Text;

namespace Bebbs.Harmonize.With.Harmony.Hub.Configuration
{
    internal static class Parser
    {
        private static void SetDeviceLocation(string hubName, IValues values)
        {
            values.Devices.ForEach(device => device.HubName = hubName);
        }

        public static IValues FromJson(string hubName, string json)
        {
            IValues values = JsonSerializer.DeserializeFromString<Values>(json);

            SetDeviceLocation(hubName, values);

            return values;
        }
    }
}
