using System;
using System.Collections.Generic;

namespace Bebbs.Harmonize.Harmony.Hub.Configuration
{
    public interface IValues
    {
        IEnumerable<IActivity> Activities { get; }
        IEnumerable<IDevice> Devices { get; }
    }

    internal class Values : IValues
    {
        public Activity[] activity { get; set; }
        public Device[] device { get; set; }

        IEnumerable<IActivity> IValues.Activities
        {
            get { return activity; }
        }

        IEnumerable<IDevice> IValues.Devices
        {
            get { return device; }
        }
    }
}
