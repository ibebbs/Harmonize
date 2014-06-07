using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client
{
    internal class Endpoint : With.Messaging.Client.IEndpoint
    {
        private readonly Configuration.ISettings _settings;
        private readonly Registration.IFactory _registrationFactory;
        private readonly IMapper _mapper;

        private readonly Registration.Collection _registrations;

        private HubConnection _hubConnection;
        private IHubProxy _hubProxy;

        public Endpoint(Configuration.ISettings settings, Registration.IFactory registrationFactory, IMapper mapper)
        {
            _settings = settings;
            _registrationFactory = registrationFactory;

            _mapper = mapper;

            _registrations = new Registration.Collection();
        }

        public async Task Initialize()
        {
            _hubConnection = new HubConnection(_settings.HarmonizeSignalRUrl);
            _hubProxy = _hubConnection.CreateHubProxy(_settings.HarmonizeHubName);

            _hubProxy.On<Common.Identity, Common.Identity, Common.Message>("Process", Process);

            await _hubConnection.Start();
        }

        private void Process(Common.Identity registrar, Common.Identity entity, Common.Message message)
        {
            string registrationKey = Registration.Key.For(registrar, entity);

            Registration.IInstance registration;

            if (_registrations.TryGetValue(registrationKey, out registration))
            {
                registration.Consumer.OnNext(_mapper.Map(message));
            }
        }

        public Task Cleanup()
        {
            if (_hubProxy != null)
            {
                _hubProxy = null;
            }

            if (_hubConnection != null)
            {
                _hubConnection.Stop();
                _hubConnection.Dispose();
                _hubConnection = null;
            }

            return TaskEx.Done;
        }

        public void Register(With.Component.IIdentity registrar, With.Component.IEntity entity, IObserver<Message.IMessage> consumer)
        {
            Registration.IInstance registration = _registrationFactory.For(registrar, entity, consumer);

            _registrations.Add(registration);

            _hubProxy.Invoke("Register", new object[] { registration.Entity });
        }

        public void Deregister(With.Component.IIdentity registrar, With.Component.IEntity entity)
        {
            string registrationKey = Registration.Key.For(registrar, entity.Identity);

            Registration.IInstance registration;

            if (_registrations.TryGetValue(registrationKey, out registration))
            {
                _hubProxy.Invoke("Deregister", new object[] { registration.Entity });
            }
        }

        public void Publish(With.Component.IIdentity entity, With.Component.IIdentity observable, DateTimeOffset date, With.Component.IMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public void Perform(With.Component.IIdentity actor, With.Component.IIdentity entity, With.Component.IIdentity actionable, IEnumerable<With.Component.IParameterValue> parameterValues)
        {
            throw new NotImplementedException();
        }
    }
}
