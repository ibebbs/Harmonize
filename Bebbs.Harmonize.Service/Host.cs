using Bebbs.Harmonize.With;
using System.Collections.Generic;
using System.ServiceProcess;

namespace Bebbs.Harmonize.Service
{
    public class Host : ServiceBase
    {
        private Harmonizer _harmonizer;

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            IEnumerable<HarmonizedModule> modules = new HarmonizedModule[]
            {
                new With.Owl.Intuition.Module(),
                new Harmonize.With.Messaging.Module(),
                new Harmonize.With.Messaging.Over.RabbitMq.Module()
            };

            Harmonize.Options harmonizeOptions = new Harmonize.Options(modules);

            _harmonizer = new Harmonize.Harmonizer(harmonizeOptions);

            _harmonizer.Start();
        }

        protected override async void OnStop()
        {
            base.OnStop();

            if (_harmonizer != null)
            {
                await _harmonizer.Stop();
                _harmonizer = null;
            }
        }

        public static void Main()
        {
            System.ServiceProcess.ServiceBase.Run(new Host());
        }
    }
}
