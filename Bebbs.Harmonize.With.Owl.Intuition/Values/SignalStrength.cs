using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.Values
{
    public class SignalStrength
    {
        internal static SignalStrength Parse(string value)
        {
            return new SignalStrength(Int32.Parse(value));
        }

        private SignalStrength(int value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            SignalStrength other = (obj as SignalStrength);

            if (other != null)
            {
                return other.Value == this.Value;
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

        public int Value { get; private set; }
    }
}
