using Bebbs.Harmonize.With.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.LightwaveRf.Configuration
{
    public static class Extensions
    {
        private static string Identity(this IDevice device)
        {
            return string.Format("{0}:R{1}D{2}", typeof(Device).Name, device.RoomNumber, device.DeviceNumber);
        }

        public static IEntity AsEntity(this IDevice device)
        {
            return Component.Build.Entity().IdentifiedBy(device.Identity()).Now();
        }
    }
}
