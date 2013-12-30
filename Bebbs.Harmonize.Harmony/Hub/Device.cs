using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.Hub
{
    public interface IDevice
    {
        int Id { get; }
        string Type { get; }
        string Manufacturer { get; }
        string Model { get; }
        string Label { get; }
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
        int IDevice.Id
        {
            get { return id; }
        }

        string IDevice.Type
        {
            get { return type; }
        }

        string IDevice.Manufacturer
        {
            get { return manufacturer; }
        }

        string IDevice.Model
        {
            get { return model; }
        }

        string IDevice.Label
        {
            get { return label; }
        }

        string IDevice.Icon
        {
            get { return icon; }
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
    }
}
