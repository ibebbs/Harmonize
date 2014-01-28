using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn
{
    public static class Instrumentation
    {
        static Instrumentation()
        {
            EventSourceProxy.TraceParameterProvider.Default.
                ForAnything()
                    .With<Component.IIdentity>()
                        .Trace(identity => identity.ToString()).As("Value")
                    .With<Component.ILocation>()
                        .Trace(location => location.Name).As("Name")
                    .With<Component.IDescription>()
                        .Trace(description => description.Name).As("Name")
                        .Trace(description => description.Type).As("Type")
                        .Trace(description => description.Manufacturer).As("Manufacturer")
                        .Trace(description => description.Model).As("Model")
                    .With<Component.IDevice>()
                        .Trace(device => device.Identity)
                        .Trace(device => device.Location)
                        .Trace(device => device.Description);
        }

        public static readonly EventSource Coordinator = EventSourceProxy.EventSourceImplementer.GetEventSource<Bus.ICoordinator>();
    }
}
