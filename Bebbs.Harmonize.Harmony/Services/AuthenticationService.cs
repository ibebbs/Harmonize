using Bebbs.Harmonize.With;
using Bebbs.Harmonize.Harmony.Messages;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Net.Http;

namespace Bebbs.Harmonize.Harmony.Services
{
    public interface IAuthenticationService : IInitialize, ICleanup
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
            public string Id { get; set; }

            public AuthenticationResult GetUserAuthTokenResult { get; set; }

            public int ErrorCode { get; set; }

            public string Message { get; set; }

            public string Source { get; set; }
        }

        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly With.Settings.IProvider _settingsProvider;

        private IDisposable _subscription;

        public AuthenticationService(IGlobalEventAggregator eventAggregator, With.Settings.IProvider settingsProvider)
        {
            _eventAggregator = eventAggregator;
            _settingsProvider = settingsProvider;
        }

        private async void ProcessRequest(IRequestAuthenticationMessage request)
        {
            With.Settings.IValues values = _settingsProvider.GetValues();
            
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

            AuthenticationResponse authenticationResponse = JsonSerializer.DeserializeFromString<AuthenticationResponse>(result);

            if (authenticationResponse.GetUserAuthTokenResult != null)
            {
                _eventAggregator.Publish(new AuthenticationResponseMessage(authenticationResponse.GetUserAuthTokenResult.AccountId, authenticationResponse.GetUserAuthTokenResult.UserAuthToken));
            }
            else
            {
                _eventAggregator.Publish(new AuthenticationResponseMessage(authenticationResponse.ErrorCode, authenticationResponse.Message, authenticationResponse.Source));
            }
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
