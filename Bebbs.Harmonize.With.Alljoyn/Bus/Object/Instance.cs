using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Object
{
    internal class Instance : AllJoynUnity.AllJoyn.BusObject
    {
        private readonly Description _description;

        public Instance(Description description) : base(description.Path, false)
        {
            _description = description;
        }

        public void AttachTo(AllJoynUnity.AllJoyn.BusAttachment bus)
        {
            foreach (Facet facet in _description.Facets)
            {
                AllJoynUnity.AllJoyn.InterfaceDescription interfaceDescription = bus.GetInterface(facet.Name);

                if (interfaceDescription != null)
                {
                    AddInterface(interfaceDescription);

                    foreach (Object.MethodHandler methodHandler in facet.MethodHandlers)
                    {
                        AllJoynUnity.AllJoyn.InterfaceDescription.Member interfaceMember = interfaceDescription.GetMember(methodHandler.Name);

                        if (interfaceMember != null)
                        {
                            AddMethodHandler(interfaceMember, (member, message) => methodHandler.Action(member, message));
                        }
                    }
                }
            }
        }

        public Description Description
        {
            get { return _description; }
        }
    }
}
