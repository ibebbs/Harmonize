using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.State
{
    public class Faulted : IState<IFaultedContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly Common.IAsyncHelper _asyncHelper;

        public Faulted(IGlobalEventAggregator eventAggregator, Common.IAsyncHelper asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        public void OnEnter(IFaultedContext context)
        {
            _eventAggregator.Publish(new Messages.ErrorMessage(context.Exception));
        }

        public void OnExit(IFaultedContext context)
        {
            // Do nothing
        }
    }
}
