using Bebbs.Harmonize.With.Component;
using System.Collections.Generic;

namespace Bebbs.Harmonize.With.LightwaveRf.Dimmer
{
    internal class Level : IActionable
    {
        private readonly Configuration.Dimmer _dimmer;
        private readonly WifiLink.IBridge _bridge;

        private readonly Percent _percent;

        public Level(Configuration.Dimmer dimmer, WifiLink.IBridge bridge)
        {
            _dimmer = dimmer;
            _bridge = bridge;

            _percent = new Percent(dimmer);

            Identity = _dimmer.ToLevelIdentity();
            Description = _dimmer.ToLevelDescription();
            Parameters = new IParameter[] { _percent };
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
