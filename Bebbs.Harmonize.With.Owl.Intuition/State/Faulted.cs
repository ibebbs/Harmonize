using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.State
{
    internal class Faulted : IState
    {
        private Event.IMediator _eventMediator;
        private ITransition _transition;
        private Context.IFault context;

        public Faulted(Event.IMediator _eventMediator, ITransition _transition, Context.IFault context)
        {
            // TODO: Complete member initialization
            this._eventMediator = _eventMediator;
            this._transition = _transition;
            this.context = context;
        }
        public void OnEnter()
        {
            throw new NotImplementedException();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public Name Name
        {
            get { return Name.Faulted; }
        }
    }
}
