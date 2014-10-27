using Bebbs.Harmonize.With.Component;
using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.With.LightwaveRf.Dimmer
{
    internal class Off : IActionable
    {
        private readonly Configuration.Dimmer _dimmer;
        private readonly WifiLink.IBridge _bridge;

        public Off(Configuration.Dimmer dimmer, WifiLink.IBridge bridge)
        {
            _dimmer = dimmer;
            _bridge = bridge;

            Identity = _dimmer.ToOffIdentity();
            Description = _dimmer.ToOffDescription();
            Parameters = Enumerable.Empty<IParameter>();
        }

        public void Execute(Message.IAction action)
        {
            throw new System.NotImplementedException();
        }

        public IIdentity Identity { get; private set; }

        public IDescription Description { get; private set; }

        public IEnumerable<IParameter> Parameters { get; private set; }
    }
}
