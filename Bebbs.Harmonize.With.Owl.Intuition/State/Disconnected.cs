using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.State
{
    internal class Disconnected : IState
    {
        private readonly Event.IMediator _eventMediator;
        private readonly ITransition _transition;

        private IDisposable _subscription;

        public Disconnected(Event.IMediator eventMediator, ITransition transition)
        {
            _eventMediator = eventMediator;
            _transition = transition;
        }

        public void OnEnter()
        {
            _subscription = _eventMediator.GetEvent<Event.Connect>().Subscribe(connect => _transition.ToConnecting());
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
