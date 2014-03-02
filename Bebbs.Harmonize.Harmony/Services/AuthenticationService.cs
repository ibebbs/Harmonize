using System;
using System.Net.Http;
using Bebbs.Harmonize.With.Harmony.Messages;
using ServiceStack;
using ServiceStack.Text;

namespace Bebbs.Harmonize.With.Harmony.Services
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

        private readonly Messages.IMediator _messageMediator;
        private readonly Settings.IProvider _settingsProvider;

        private IDisposable _subscription;

        public AuthenticationService(Messages.IMediator messageMediator, Settings.IProvider settingsProvider)
        {
            _messageMediator = messageMediator;
            _settingsProvider = settingsProvider;
        }

        private async void ProcessRequest(IRequestAuthenticationMessage request)
        {
            Settings.IValues values = _settingsProvider.GetValues();
            
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
                _messageMediator.Publish(new AuthenticationResponseMessage(authenticationResponse.GetUserAuthTokenResult.AccountId, authenticationResponse.GetUserAuthTokenResult.UserAuthToken));
            }
            else
            {
                _messageMediator.Publish(new AuthenticationResponseMessage(authenticationResponse.ErrorCode, authenticationResponse.Message, authenticationResponse.Source));
            }
        }

        public void Initialize()
        {
            _subscription = _messageMediator.GetEvent<IRequestAuthenticationMessage>().Subscribe(ProcessRequest);
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
