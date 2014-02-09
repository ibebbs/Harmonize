using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Object
{
    internal interface IFactory
    {
        Description Describe(Component.IEntity device);

        Instance Build(Description description);
    }

    internal class Factory : IFactory
    {
        private readonly IGlobalEventAggregator _eventAggregator;

        public Factory(IGlobalEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public Description Describe(Component.IEntity device)
        {
            string path = string.Format("/{0}/{1}", device.Identity.ToString(), device.Description.BusName());

            // TODO: Reimplement the following code
            /*
            IEnumerable<Facet> facets = device.Actionables.Select(
                actionable =>
                {
                    string name = string.Format("{0}-{1}-{2}", device.Description.Manufacturer, device.Description.Model, actionable.Identity.ToString());

                    IEnumerable<MethodHandler> methodHandlers = actionable.Actions.Select(action => new MethodHandler(action.Name, (member, message) => _eventAggregator.Publish(action.Command))).ToArray();

                    return new Facet(name, methodHandlers);
                }
            ).ToArray();
            */
            return new Description(path, Enumerable.Empty<Facet>());
        }

        public Instance Build(Description description)
        {
            return new Instance(description);
        }
    }
}
