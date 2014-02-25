using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.State
{
    internal class Configuring : IState
    {
        private readonly Event.IMediator _eventMediator;
        private readonly ITransition _transition;
        private readonly Context.IConfiguration _context;

        public Configuring(Event.IMediator eventMediator, ITransition transition, Context.IConfiguration context)
        {
            _eventMediator = eventMediator;
            _transition = transition;
            _context = context;
        }

        private void ToFault(Exception exception)
        {
            _transition.ToFaulted(_context.CommandEndpoint, exception);
        }

        private void ToRegistration()
        {
            _transition.ToRegistration(_context.CommandEndpoint, _context.Version);
        }

        public async void OnEnter()
        {
            try
            {
                if (_context.AutoConfigurePacketEndpoint)
                {
                    Command.Response.Udp udp = await _context.CommandEndpoint.Send(new Command.Request.GetUpdPushPort());

                    if (!udp.Endpoint.Equals(_context.LocalPacketEndpoint))
                    {
                        udp = await _context.CommandEndpoint.Send(new Command.Request.SetUdpPushPort(_context.LocalPacketEndpoint));

                        if (udp.Endpoint.Equals(_context.LocalPacketEndpoint))
                        {
                            await _context.CommandEndpoint.Send(new Command.Request.Save());
                        }
                        else
                        {
                            ToFault(new ArgumentException(string.Format("Unable to set UdpPushPort to specified endpoint. Trying to change port from '{0}' to '{1}'", udp.Endpoint.ToString(), _context.LocalPacketEndpoint.ToString())));
                        }
                    }
                }

                ToRegistration();
            }
            catch (Exception e)
            {
                ToFault(e);
            }
        }

        public void OnExit()
        {
            // Do nothing
        }

        public Name Name
        {
            get { return Name.Configuring; }
        }
    }
}
