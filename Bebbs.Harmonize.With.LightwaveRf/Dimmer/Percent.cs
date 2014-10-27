using Bebbs.Harmonize.With.Component;

namespace Bebbs.Harmonize.With.LightwaveRf.Dimmer
{
    internal class Percent : IParameter
    {
        private readonly Configuration.Dimmer _dimmer;

        public Percent(Configuration.Dimmer dimmer)
        {
            _dimmer = dimmer;

            Identity = _dimmer.ToPercentIdentity();
            Description = _dimmer.ToPercentDescription();
        }

        public IIdentity Identity { get; private set; }

        public IParameterDescription Description { get; private set; }
    }
}
