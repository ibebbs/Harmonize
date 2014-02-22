﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.State
{
    internal class Registering : IState
    {
        private readonly Event.IMediator _eventMediator;
        private readonly ITransition _transition;
        private readonly Context.IRegistration _context;

        public Registering(Event.IMediator eventMediator, ITransition transition, Context.IRegistration context)
        {
            _eventMediator = eventMediator;
            _transition = transition;
            _context = context;
        }

        private async void Register(Command.Response.Rosta rosta)
        {
            List<Command.Response.Device> devices = new List<Command.Response.Device>();

            try
            {
                foreach (Tuple<int, string> device in rosta.Devices)
                {
                    devices.Add(await _context.CommandEndpoint.Send(new Command.Request.GetDevice(device.Item1)));
                }

                ToListenting();
            }
            catch (Exception exception)
            {
                ToFault(exception);
            }
        }

        private void ToListenting()
        {
            _transition.ToListening(_context.CommandEndpoint, _context.Version);
        }

        private void ToFault(Exception exception)
        {
            _transition.ToFaulted(_context.CommandEndpoint, exception);
        }

        public void OnEnter()
        {
            Observable.FromAsync(() => _context.CommandEndpoint.Send(new Command.Request.GetRosta())).Take(1).Subscribe(Register, ToFault);
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public Name Name
        {
            get { return Name.Registering; }
        }
    }
}
