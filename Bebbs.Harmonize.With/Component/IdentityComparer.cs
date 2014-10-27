using System;
using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Component
{
    public class IdentityComparer : IEqualityComparer<IIdentity>
    {
        public static readonly IEqualityComparer<IIdentity> Default = new IdentityComparer();

        public bool Equals(IIdentity x, IIdentity y)
        {
            return string.Equals(x.Value, y.Value, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(IIdentity obj)
        {
            string value = obj.Value ?? string.Empty;

            return value.GetHashCode();
        }
    }
}
