
namespace Bebbs.Harmonize.With.Component
{
    public interface IIdentity
    {
    }

    public class StringIdentity : IIdentity
    {
        private readonly string _identity;

        public StringIdentity(string identity)
        {
            _identity = identity;
        }

        public override string ToString()
        {
            return _identity;
        }

        public override int GetHashCode()
        {
            return _identity.GetHashCode();
        }
    }
}
