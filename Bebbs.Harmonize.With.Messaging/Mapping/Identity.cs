
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Identity : Bebbs.Harmonize.With.Component.IIdentity
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
