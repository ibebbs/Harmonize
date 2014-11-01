using Bebbs.Harmonize.With;
using Bebbs.Harmonize.With.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Tool
{
    internal class Instance : Host.IService
    {
        private static readonly With.Component.Identity Identity = new With.Component.Identity("Bebbs.Harmonize.Tool");

        private readonly With.Messaging.Client.IEndpoint _clientEndpoint;
        private readonly Subject<With.Message.IMessage> _messages;

        public Instance(With.Messaging.Client.IEndpoint clientEndpoint)
        {
            _clientEndpoint = clientEndpoint;
            _messages = new Subject<With.Message.IMessage>();
        }

        public Task Initialize()
        {
            return _clientEndpoint.Initialize();
        }

        public Task Start()
        {
            return TaskEx.Done;
        }

        public Task Stop()
        {
            return TaskEx.Done;
        }

        public Task Cleanup()
        {
            return _clientEndpoint.Cleanup();
        }

        public Task Register(IEntity entity)
        {
            _clientEndpoint.Register(Identity, entity, _messages);

            return TaskEx.Done;
        }

        internal Task Action(IIdentity entity, IIdentity actionable, IEnumerable<IParameterValue> parameters)
        {
            _clientEndpoint.Perform(Identity, entity, actionable, parameters);

            return TaskEx.Done;
        }
    }
}
