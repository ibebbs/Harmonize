using Bebbs.Harmonize.Common;
using Bebbs.Harmonize.Harmony.Messages;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Net.Http;

namespace Bebbs.Harmonize.Harmony.Services
{
    public interface IAuthenticationService : IInitializeAtStartup, ICleanupAtShutdown
    {

    }

    internal class AuthenticationService : IAuthenticationService
    {
        private class AuthenticationRequest
        {
            public string password { get; set; }
            public string email { get; set; }
        }

        private class AuthenticationResult
        {
            public string AccountId { get; set; }
            public string UserAuthToken { get; set; }
        }

        private class AuthenticationResponse
        {
            public AuthenticationResult GetUserAuthTokenResult { get; set; }
        }

        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly Common.Settings.IProvider _settingsProvider;

        private IDisposable _subscription;

        public AuthenticationService(IGlobalEventAggregator eventAggregator, Common.Settings.IProvider settingsProvider)
        {
            _eventAggregator = eventAggregator;
            _settingsProvider = settingsProvider;
        }

        private async void ProcessRequest(IRequestAuthenticationMessage request)
        {
            Common.Settings.IValues values = _settingsProvider.GetValues();
            
            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                password = request.Password,
                email = request.EMail
            };

            string serializedRequest = authenticationRequest.ToJson();

            HttpContent content = new StringContent(serializedRequest, System.Text.Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var response = await client.PostAsync(values.AuthenticationEndpoint, content);

            string result = await response.Content.ReadAsStringAsync();

            AuthenticationResponse authenicationResponse = JsonSerializer.DeserializeFromString<AuthenticationResponse>(result);

            _eventAggregator.Publish(new AuthenticationResponseMessage(authenicationResponse.GetUserAuthTokenResult.AccountId, authenicationResponse.GetUserAuthTokenResult.UserAuthToken));
        }

        public void Initialize()
        {
            _subscription = _eventAggregator.GetEvent<IRequestAuthenticationMessage>().Subscribe(ProcessRequest);
        }

        public void Cleanup()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }
        }
    }
}
