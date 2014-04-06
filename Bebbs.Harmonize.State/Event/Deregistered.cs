
namespace Bebbs.Harmonize.State.Event
{
    public class Deregistered
    {
        public Component.Identity Registrar { get; set; }
        public Component.Identity Entity { get; set; }
    }
}
