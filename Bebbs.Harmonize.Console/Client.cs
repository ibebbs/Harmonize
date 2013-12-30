using Bebbs.Harmonize.Harmony.Command;
using Newtonsoft.Json;

namespace Bebbs.Harmonize.Console
{
    public class Client
    {
        private Harmonizer _harmonizer;

        public async void Start()
        {
            _harmonizer = new Harmonize.Harmonizer();

            var result = await _harmonizer.Start(new Settings.Provider());

            System.Console.WriteLine(JsonConvert.SerializeObject(result));

            _harmonizer.SendCommand(new With.Command.PowerOn("15666630"));
        }

        public async void Stop()
        {
            if (_harmonizer != null)
            {
                await _harmonizer.Stop();

                _harmonizer = null;
            }
        }
    }
}
