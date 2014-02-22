using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Harmony.Hub.Configuration
{
    public interface IValues
    {
        IEnumerable<IActivity> Activities { get; }
        IEnumerable<IEntity> Devices { get; }
    }

    internal class Values : IValues
    {
        public Activity[] activity { get; set; }
        public Entity[] device { get; set; }

        IEnumerable<IActivity> IValues.Activities
        {
            get { return activity; }
        }

        IEnumerable<IEntity> IValues.Devices
        {
            get { return device; }
        }
    }
}
