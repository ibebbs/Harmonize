
namespace Bebbs.Harmonize.With.Harmony.Hub
{
    internal class Identity : With.Component.IIdentity
    {
        public Identity(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public string Value { get; private set; }
    }
}
