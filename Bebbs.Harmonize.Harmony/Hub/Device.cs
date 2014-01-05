using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.Hub
{
    public interface IDevice : With.Component.IDevice
    {
        int Id { get; }
        string Icon { get; }
        bool IsManualPower { get; }
        string DeviceProfileUri { get; }
        string DeviceTypeDisplayName { get; }
        int Transport { get; }
        int ControlPort { get; }
        string SuggestedDisplay { get; }
        
        string HubName { get; set; }
    }

    internal class Device : IDevice
    {
        private readonly Lazy<With.Component.IIdentity> _identity;
        private readonly Lazy<With.Component.ILocation> _location;
        private readonly Lazy<With.Component.IDescription> _description;

        public Device()
        {
            _identity = new Lazy<With.Component.IIdentity>(() => new Identity(id));
            _location = new Lazy<With.Component.ILocation>(() => new Location(HubName));
            _description = new Lazy<With.Component.IDescription>(() => new Description(label, type, manufacturer, model));
        }

        With.Component.IIdentity With.Component.IDevice.Identity 
        {
            get { return _identity.Value; }
        }

        With.Component.ILocation With.Component.IDevice.Location
        {
            get { return _location.Value; }
        }

        With.Component.IDescription With.Component.IDevice.Description
        {
            get { return _description.Value; }
        }

        IEnumerable<With.Component.IControl> With.Component.IDevice.Controls
        {
            get { return controlGroup; }
        }

        string IDevice.Icon
        {
            get { return icon; }
        }

        int IDevice.Id
        {
            get { return id; }
        }

        bool IDevice.IsManualPower
        {
            get { return isManualPower; }
        }

        string IDevice.DeviceProfileUri
        {
            get { return deviceProfileUri; }
        }

        string IDevice.DeviceTypeDisplayName
        {
            get { return deviceTypeDisplayName; }
        }

        int IDevice.Transport
        {
            get { return transport; }
        }

        int IDevice.ControlPort
        {
            get { return controlPort; }
        }

        string IDevice.SuggestedDisplay
        {
            get { return suggestedDisplay; }
        }

        public int id { get; set; }
        public string type { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string label { get; set; }
        public string icon { get; set; }
        public bool isManualPower { get; set; }
        public string deviceProfileUri { get; set; }
        public string deviceTypeDisplayName { get; set; }
        public int transport { get; set; }
        public int controlPort { get; set; }
        public string suggestedDisplay { get; set; }
        public Control[] controlGroup { get; set; }

        public string HubName { get; set; }
    }
}
