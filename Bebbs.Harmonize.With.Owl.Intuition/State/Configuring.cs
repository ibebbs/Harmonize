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

        private void SaveOrFault(Command.Response.Udp response)
        {
            if (response.Endpoint == _context.LocalPacketEndpoint)
            {
                Observable.FromAsync(() => _context.CommandEndpoint.Send(new Command.Request.Save())).Take(1).Subscribe(_ => ToRegistration(), ToFault);
            }
            else
            {
                ToFault(new ArgumentException(string.Format("Unable to set UdpPushPort to specified endpoint. Trying to change port from '{0}' to '{1}'", response.Endpoint.ToString(), _context.LocalPacketEndpoint.ToString())));
            }
        }

        private void ChangeOrTransition(Command.Response.Udp response)
        {
            if (response.Endpoint != _context.LocalPacketEndpoint)
            {
                Observable.FromAsync(() => _context.CommandEndpoint.Send(new Command.Request.SetUdpPushPort(_context.LocalPacketEndpoint))).Take(1).Subscribe(SaveOrFault, ToFault);
            }
            else
            {
                ToRegistration();
            }
        }

        private void ToFault(Exception exception)
        {
            _transition.ToFaulted(_context.CommandEndpoint, exception);
        }

        private void ToRegistration()
        {
            _transition.ToRegistration(_context.CommandEndpoint, _context.Version);
        }

        public void OnEnter()
        {
            if (_context.AutoConfigurePacketEndpoint)
            {
                Observable.FromAsync(() => _context.CommandEndpoint.Send(new Command.Request.GetUpdPushPort())).Take(1).Subscribe(ChangeOrTransition, ToFault);
            }
            else
            {
                ToRegistration();
            }
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public Name Name
        {
            get { return Name.Configuring; }
        }
    }
}
