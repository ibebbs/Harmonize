
namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Endpoint
{
    public interface IFactory
    {
        IInstance CreateEndpoint();
    }

    internal class Factory : IFactory
    {
        private readonly Gateway.Settings.IProvider _settingsProvider;
        private readonly Response.IParser _responseParser;

        public Factory(Gateway.Settings.IProvider settingsProvider, Response.IParser responseParser)
        {
            _settingsProvider = settingsProvider;
            _responseParser = responseParser;
        }

        public IInstance CreateEndpoint()
        {
            Gateway.Settings.IValues settings = _settingsProvider.GetValues();

            Instance endpoint = new Instance(_responseParser, settings.LocalCommandEndpoint, settings.OwlCommandEndpoint, settings.OwlCommandResponseTimeout, settings.OwlCommandKey);

            Instrumentation.Command.Endpoint.CreatedAt(settings.LocalCommandEndpoint.Address.ToString(), settings.LocalCommandEndpoint.Port);

            return endpoint;
        }
    }
}
