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
                    .With<Component.IEntityDescription>()
                        .Trace(description => description.Name).As("Name")
                        .Trace(description => description.Remarks).As("Remarks")
                        .Trace(description => description.Type).As("Type")
                        .Trace(description => description.Manufacturer).As("Manufacturer")
                        .Trace(description => description.Model).As("Model")
                    .With<Component.IValueDescription>()
                        .Trace(description => description.Name).As("Name")
                        .Trace(description => description.Remarks).As("Remarks")
                        .Trace(description => description.Measurement).As("Measurement")
                        .Trace(description => description.Minimum).As("Minimum")
                        .Trace(description => description.Maximum).As("Maximum")
                        .Trace(description => description.Default).As("Default")
                    .With<Component.IParameterDescription>()
                        .Trace(description => description.Name).As("Name")
                        .Trace(description => description.Remarks).As("Remarks")
                        .Trace(description => description.Measurement).As("Measurement")
                        .Trace(description => description.Minimum).As("Minimum")
                        .Trace(description => description.Maximum).As("Maximum")
                        .Trace(description => description.Default).As("Default")
                        .Trace(description => description.Required).As("Required")
                    .With<Component.IDescription>()
                        .Trace(description => description.Name).As("Name")
                        .Trace(description => description.Remarks).As("Remarks")
                    .With<Component.IEntity>()
                        .Trace(device => device.Identity)
                        .Trace(device => device.Description);
        }

        public static readonly EventSource Coordinator = EventSourceProxy.EventSourceImplementer.GetEventSource<Bus.ICoordinator>();
    }
}
