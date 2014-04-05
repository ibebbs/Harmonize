using Bebbs.Harmonize.State.Component;

namespace Bebbs.Harmonize.State.Event
{
    public class Registered
    {
        public Identity Registrar { get; set; }

        public Entity Entity { get; set; }
    }
}
