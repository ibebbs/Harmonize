using System.Linq;
using System;
using Bebbs.Harmonize.With;

namespace Bebbs.Harmonize.Console
{
    public class Client
    {
        private Harmonizer _harmonizer;

        public async void Start()
        {
            _harmonizer = new Harmonize.Harmonizer();

            var result = await _harmonizer.Start(new Settings.Provider());

            var device = result.Devices.Where(d => string.Equals(d.Type, "Amplifier", StringComparison.CurrentCultureIgnoreCase) && string.Equals(d.Model, "DSP-A5", StringComparison.CurrentCultureIgnoreCase)).First();
            var control = device.Controls.Where(c => string.Equals(c.Name, "Power", StringComparison.CurrentCultureIgnoreCase)).First();
            var action = control.Actions.Where(a => string.Equals(a.Name, "PowerOn", StringComparison.CurrentCultureIgnoreCase)).First();
            var command = action.Command;

            _harmonizer.SendCommand(command);
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
