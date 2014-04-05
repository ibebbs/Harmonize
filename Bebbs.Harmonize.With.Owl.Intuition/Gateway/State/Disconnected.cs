using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State
{
    internal class Disconnected : IState
    {
        private readonly Gateway.Event.IMediator _eventMediator;
        private readonly ITransition _transition;

        private IDisposable _subscription;

        public Disconnected(Gateway.Event.IMediator eventMediator, ITransition transition)
        {
            _eventMediator = eventMediator;
            _transition = transition;
        }

        public void OnEnter()
        {
            _subscription = _eventMediator.GetEvent<Gateway.Event.Connect>().Subscribe(connect => _transition.ToConnecting());
        }

        public void OnExit()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }
        }

        public Name Name
        {
            get { return Name.Disconnected; }
        }
    }
}
