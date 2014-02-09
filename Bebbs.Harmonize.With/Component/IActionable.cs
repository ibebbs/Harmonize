using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Component
{
    public interface IActionable
    {
        IIdentity Identity { get; }
        IDescription Description { get; }
        IEnumerable<IParameter> Parameters { get; }
    }
}
