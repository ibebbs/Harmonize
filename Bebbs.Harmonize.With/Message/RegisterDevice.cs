using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Message
{
    public interface IRegisterDevice
    {
        Component.IDevice Device { get; }
    }

    public class RegisterDevice : IRegisterDevice
    {
        public RegisterDevice(Component.IDevice device)
        {
            Device = device;
        }

        public Component.IDevice Device { get; private set; }
    }
}
