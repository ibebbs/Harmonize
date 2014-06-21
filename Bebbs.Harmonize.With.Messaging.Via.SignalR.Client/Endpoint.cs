using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client
{
    public interface IEndpoint
    {
        Task Initialize();
        Task Cleanup();

        Task Register(With.Component.IIdentity client, With.Component.IEntity entity, IObserver<With.Message.IMessage> consumer);
        Task Deregister(With.Component.IIdentity client, With.Component.IEntity entity);
        Task Observe(With.Component.IIdentity client, With.Component.IIdentity observer, With.Component.IIdentity entity, With.Component.IIdentity observable);
        Task Perform(With.Component.IIdentity actor, With.Component.IIdentity entity, With.Component.IIdentity actionable, System.Collections.Generic.IEnumerable<With.Component.IParameterValue> parameterValues);
        Task Publish(With.Component.IIdentity entity, With.Component.IIdentity observable, DateTimeOffset date, With.Component.IMeasurement measurement);
    }

    public class Endpoint : IEndpoint
    {
        private readonly Hub.IFactory _hubFactory;
        private readonly Registration.IFactory _registrationFactory;
        private readonly IMapper _mapper;

        private readonly Registration.Collection _registrations;

        private Hub.IInstance _hub;

        public Endpoint(Hub.IFactory hubFactory, Registration.IFactory registrationFactory, IMapper mapper)
        {
            _hubFactory = hubFactory;
            _registrationFactory = registrationFactory;

            _mapper = mapper;

            _registrations = new Registration.Collection();
        }

        private void Process(Common.Identity client, Common.Identity entity, Common.Message message)
        {
            string registrationKey = Registration.Key.For(client, entity);

            Registration.IInstance registration;

            if (_registrations.TryGetValue(registrationKey, out registration))
            {
                registration.Consumer.OnNext(_mapper.Map(message));
            }
        }

        public async Task Initialize()
        {
            _hub = _hubFactory.Create();

            _hub.GetEvent<Common.Identity, Common.Identity, Common.Message>("Process").Subscribe(tuple => Process(tuple.Item1, tuple.Item2, tuple.Item3));
            
            await _hub.Start();
        }

        public async Task Cleanup()
        {
            if (_hub != null)
            {
                await _hub.Stop();
                _hub.Dispose();
                _hub = null;
            }
        }

        public async Task Register(With.Component.IIdentity client, With.Component.IEntity entity, IObserver<Message.IMessage> consumer)
        {
            Registration.IInstance registration = _registrationFactory.For(client, entity, consumer);

            _registrations.Add(registration);

            await _hub.Register(registration.Entity);
        }

        public async Task Deregister(With.Component.IIdentity client, With.Component.IEntity entity)
        {
            string registrationKey = Registration.Key.For(client, entity.Identity);

            Registration.IInstance registration;

            if (_registrations.TryGetValue(registrationKey, out registration))
            {
                await _hub.Deregister(registration.Entity);
            }
        }

        public async Task Observe(With.Component.IIdentity client, With.Component.IIdentity entity, With.Component.IIdentity source, With.Component.IIdentity observable)
        {
            string registrationKey = Registration.Key.For(client, entity);

            Registration.IInstance registration;

            if (_registrations.TryGetValue(registrationKey, out registration))
            {
                await _hub.Observe(_mapper.Map(entity), _mapper.Map(source), _mapper.Map(observable));
            }
        }

        public Task Publish(With.Component.IIdentity entity, With.Component.IIdentity observable, DateTimeOffset date, With.Component.IMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public Task Perform(With.Component.IIdentity actor, With.Component.IIdentity entity, With.Component.IIdentity actionable, IEnumerable<With.Component.IParameterValue> parameterValues)
        {
            throw new NotImplementedException();
        }
    }
}
