using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Object
{
    public static class DeviceDescriptionExtensions
    {
        public static string BusName(this Component.IDescription description)
        {
            return description.Name.Replace(" ", string.Empty).Replace("/", string.Empty);
        }
    }
}
