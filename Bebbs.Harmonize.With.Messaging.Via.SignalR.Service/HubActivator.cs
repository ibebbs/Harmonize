using Microsoft.AspNet.SignalR.Hubs;
using Ninject;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    public class HubActivator : IHubActivator
    {
        private readonly IKernel _kernel;

        public HubActivator(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub) _kernel.Get(descriptor.HubType);
        }
    }
}
