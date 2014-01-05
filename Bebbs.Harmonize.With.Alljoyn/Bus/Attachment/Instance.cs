using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Attachment
{
    internal interface IInstance : IDisposable
    {
        void Start();

        void Add(Object.Instance instance);

        void Remove(Object.Instance instance);

        void Stop();
    }

    internal class Instance : IInstance
    {
        private readonly IDescription _description;
        private readonly Dictionary<string, AllJoynUnity.AllJoyn.InterfaceDescription> _interfaces;

        private Listener _busListener;
        private SessionListener _sessionListener;
        private AllJoynUnity.AllJoyn.BusAttachment _busAttachment;

        public Instance(IDescription description)
        {
            _description = description;
            _interfaces = new Dictionary<string,AllJoynUnity.AllJoyn.InterfaceDescription>();
        }

        private void PrepareInterfaces(Object.Description description)
        {
            foreach (Object.Facet facet in description.Facets)
            {
                AllJoynUnity.AllJoyn.InterfaceDescription facetInterface;

                if (!_interfaces.TryGetValue(facet.Name, out facetInterface))
                {
                    var status = _busAttachment.CreateInterface(facet.Name, new AllJoynUnity.AllJoyn.InterfaceDescription.SecurityPolicy(), out facetInterface);

                    if (status)
                    {
                        foreach (var methodHandler in facet.MethodHandlers)
                        {
                            facetInterface.AddMember(AllJoynUnity.AllJoyn.Message.Type.MethodCall, methodHandler.Name, string.Empty, string.Empty, string.Empty);
                        }

                        facetInterface.Activate();

                        _interfaces.Add(facet.Name, facetInterface);
                    }
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }

        public void Start()
        {
            _busListener = new Listener();
            _sessionListener = new SessionListener();

            _busAttachment = new AllJoynUnity.AllJoyn.BusAttachment(_description.ApplicationName, true);
            _busAttachment.RegisterBusListener(_busListener);

            if (_busAttachment.Start())
            {
                if (_busAttachment.Connect(_description.ConnectionSpecification))
                {
                    var status = _busAttachment.RequestName(_description.ServiceName, AllJoynUnity.AllJoyn.DBus.NameFlags.DoNotQueue);

                    if (status)
                    {
                        AllJoynUnity.AllJoyn.SessionOpts options = new AllJoynUnity.AllJoyn.SessionOpts(AllJoynUnity.AllJoyn.SessionOpts.TrafficType.Messages, false, AllJoynUnity.AllJoyn.SessionOpts.ProximityType.Any, AllJoynUnity.AllJoyn.TransportMask.Any);

                        ushort sessionPort = _description.ServicePort;

                        status = _busAttachment.BindSessionPort(ref sessionPort, options, _sessionListener);

                        if (status)
                        {
                            status = _busAttachment.AdvertiseName(_description.ServiceName, options.Transports);

                            if (!status)
                            {
                                throw new ApplicationException(string.Format("Could not advertise service name '{0}'. Error: '{1}'", _description.ServiceName, status));
                            }
                        }
                        else
                        {
                            throw new ApplicationException(string.Format("Could not bind AllJoyn session port. Error: '{0}'", status));
                        }
                    }
                    else
                    {
                        throw new ApplicationException(string.Format("Could not secure requested name '{0}'. Error: '{1}'", _description.ServiceName, status));
                    }
                }
                else
                {
                    throw new ApplicationException("Could not connect the AllJoyn BusAttachment");
                }
            }
            else
            {
                throw new ApplicationException("Could not start AllJoyn BusAttachment");
            }
        }

        public void Add(Object.Instance instance)
        {
            PrepareInterfaces(instance.Description);

            instance.AttachTo(_busAttachment);

            _busAttachment.RegisterBusObject(instance);
        }

        public void Remove(Object.Instance instance)
        {
            _busAttachment.UnregisterBusObject(instance);
        }

        public void Stop()
        {
            if (_busAttachment != null)
            {
                _busAttachment.Disconnect();
                _busAttachment.Stop();
                _busAttachment.Dispose();
                _busAttachment = null;
            }
        }
    }
}
