using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State
{
    internal class Registering : IState
    {
        private readonly Gateway.Event.IMediator _mediator;
        private readonly ITransition _transition;
        private readonly Context.IRegistration _context;

        public Registering(Gateway.Event.IMediator mediator, ITransition transition, Context.IRegistration context)
        {
            _mediator = mediator;
            _transition = transition;
            _context = context;
        }

        private void ToListenting()
        {
            _transition.ToListening(_context.CommandEndpoint, _context.Version);
        }

        private void ToFault(Exception exception)
        {
            _transition.ToFaulted(_context.CommandEndpoint, exception);
        }

        public async void OnEnter()
        {
            Command.Response.Rosta rosta = await _context.CommandEndpoint.Send(new Command.Request.GetRosta());

            List<Command.Response.Device> devices = new List<Command.Response.Device>();

            try
            {
                foreach (Tuple<int, string> device in rosta.Devices)
                {
                    devices.Add(await _context.CommandEndpoint.Send(new Command.Request.GetDevice(device.Item1)));
                }

                _mediator.Publish(new Gateway.Event.Connected(_context.Name, _context.Remarks, _context.MacAddress, devices));

                ToListenting();
            }
            catch (Exception exception)
            {
                ToFault(exception);
            }
        }

        public void OnExit()
        {
            // Do nothing
        }

        public Name Name
        {
            get { return Name.Registering; }
        }
    }
}
