using System;

namespace Bebbs.Harmonize.With.Component
{
    public interface IIdentity
    {
        string Value { get; }
    }

    public class Identity : IIdentity
    {
        public static IIdentity Unique()
        {
            return new Identity(Guid.NewGuid().ToString());
        }

        private readonly string _identity;

        public Identity(string identity)
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

        public override bool Equals(object obj)
        {
            IIdentity other = obj as IIdentity;

            if (other != null && string.Equals(this.Value, other.Value, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public string Value
        {
            get { return _identity; }
        }
    }
}
