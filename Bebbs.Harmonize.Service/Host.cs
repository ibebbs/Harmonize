using System.ServiceProcess;

namespace Bebbs.Harmonize.Service
{
    public partial class Host : ServiceBase
    {
        private Controller _controller;

        public Host()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            Options options = new Options();

            CommandLine.Parser.Default.ParseArguments(args, options);

            _controller = new Controller();

            _controller.Start(options);
        }

        protected override async void OnStop()
        {
            base.OnStop();

            if (_controller != null)
            {
                await _controller.Stop();

                _controller = null;
            }
        }
    }
}
