using System.Threading.Tasks;

namespace Bebbs.Harmonize.Service
{
    public class Controller
    {
        private Harmonizer _harmonizer;

        public void Start(Options options)
        {
            Harmonize.Options harmonizeOptions = new Harmonize.Options(new[] { options.ModulePattern });

            Instrumentation.Start(options.Console, options.LogPath, options.TraceNames);

            _harmonizer = new Harmonize.Harmonizer(harmonizeOptions);

            _harmonizer.Start();
        }

        public async Task Stop()
        {
            if (_harmonizer != null)
            {
                await _harmonizer.Stop();
                _harmonizer = null;
            }

            Instrumentation.Stop();
        }
    }
}
