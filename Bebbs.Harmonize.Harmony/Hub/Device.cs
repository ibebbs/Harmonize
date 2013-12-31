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
    }

    internal class Device : IDevice
    {
        With.Component.IIdentity With.Component.IDevice.Identity 
        {
            get { return null; }
        }

        IEnumerable<With.Component.IControl> With.Component.IDevice.Controls
        {
            get { return controlGroup; }
        }

        string With.Component.IDevice.Type
        {
            get { return type; }
        }

        string With.Component.IDevice.Manufacturer
        {
            get { return manufacturer; }
        }

        string With.Component.IDevice.Model
        {
            get { return model; }
        }

        string With.Component.IDevice.Name
        {
            get { return label; }
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
    }
}
