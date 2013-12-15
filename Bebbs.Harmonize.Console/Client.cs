using Bebbs.Harmonize.Harmony.Command;

namespace Bebbs.Harmonize.Console
{
    public class Client
    {
        private Harmonizer _harmonizer;

        public async void Start()
        {
            _harmonizer = new Harmonize.Harmonizer();

            await _harmonizer.Start(new Settings.Provider());

            _harmonizer.SendCommand(new PowerOnCommand("15666630"));
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
