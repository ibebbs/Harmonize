using Bebbs.Harmonize.With.Component;
using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.With.LightwaveRf.Dimmer
{
    internal class On : IActionable
    {
        private readonly Configuration.Dimmer _dimmer;
        private readonly WifiLink.IBridge _bridge;

        public On(Configuration.Dimmer dimmer, WifiLink.IBridge bridge)
        {
            _dimmer = dimmer;
            _bridge = bridge;

            Identity = _dimmer.ToOnIdentity();
            Description = _dimmer.ToOnDescription();
            Parameters = Enumerable.Empty<IParameter>();
        }

        public void Execute(Message.IAction action)
        {

        }

        public IIdentity Identity { get; private set; }

        public IDescription Description { get; private set; }

        public IEnumerable<IParameter> Parameters { get; private set; }
    }
}
