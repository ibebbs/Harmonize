
namespace Bebbs.Harmonize.With.Owl.Intuition.Values
{
    public class BatteryState
    {
        internal static BatteryState Parse(string value)
        {
            return new BatteryState(value);
        }

        private BatteryState(string value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            BatteryState other = (obj as BatteryState);

            if (other != null)
            {
                return string.Equals(other.Value, this.Value, System.StringComparison.CurrentCultureIgnoreCase);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public string Value { get; private set; }
    }
}
