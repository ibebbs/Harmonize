using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.Values
{
    public class LinkQuality
    {
        internal static LinkQuality Parse(string value)
        {
            return new LinkQuality(Int32.Parse(value));
        }

        private LinkQuality(int value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            LinkQuality other = (obj as LinkQuality);

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
