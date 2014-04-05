using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State
{
    public class Connecting : IState
    {
        private readonly ITransition _transition;
        private readonly Command.Endpoint.IFactory _commandEndpointFactory;
        private readonly Context.IConnection _context;

        private Command.Endpoint.IInstance _endpoint;

        public Connecting(ITransition transition, Command.Endpoint.IFactory commandEndpointFactory, Context.IConnection context)
        {
            _transition = transition;
            _commandEndpointFactory = commandEndpointFactory;

            _context = context;
        }

        private void Process(Command.Response.Version response)
        {
            _transition.ToConfiguration(_endpoint, new Values.Version(response.Firmware, response.Revision, response.Build));
        }

        private void Fault(Exception e)
        {
            _transition.ToFaulted(_endpoint, e);
        }

        public void OnEnter()
        {
            _endpoint = _commandEndpointFactory.CreateEndpoint();
            _endpoint.Open();

            Observable.FromAsync(() => _endpoint.Send(new Command.Request.GetVersion())).Take(1)
                      .Subscribe(Process, Fault);
        }

        public void OnExit()
        {
            // Do nothing
        }

        public Name Name
        {
            get { return Name.Connecting; }
        }
    }
}
