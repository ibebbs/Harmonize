
namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Event
{
    public class Reading
    {
        public Reading(Packet.IReading value)
        {
            Value = value;
        }

        public Packet.IReading Value { get; private set; }
    }
}
