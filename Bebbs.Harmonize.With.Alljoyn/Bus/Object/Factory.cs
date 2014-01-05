using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Object
{
    internal interface IFactory
    {
        Description Describe(Component.IDevice device);

        Instance Build(Description description);
    }

    internal class Factory : IFactory
    {
        private readonly IGlobalEventAggregator _eventAggregator;

        public Factory(IGlobalEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public Description Describe(Component.IDevice device)
        {
            string path = string.Format("{0}/{1}", device.Location.Name, device.Description.Name);

            IEnumerable<Facet> facets = device.Controls.Select(
                control =>
                {
                    string name = string.Format("{0}-{1}-{2}", device.Description.Manufacturer, device.Description.Model, control.Name);

                    IEnumerable<MethodHandler> methodHandlers = control.Actions.Select(action => new MethodHandler(action.Name, (member, message) => _eventAggregator.Publish(action.Command))).ToArray();

                    return new Facet(name, methodHandlers);
                }
            ).ToArray();

            return new Description(path, facets);
        }

        public Instance Build(Description description)
        {
            return new Instance(description);
        }
    }
}
