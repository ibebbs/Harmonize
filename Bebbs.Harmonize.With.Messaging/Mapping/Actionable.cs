using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Actionable : Component.IActionable
    {
        public Component.IIdentity Identity { get; set; }
        public Component.IDescription Description { get; set; }
        public IEnumerable<Component.IParameter> Parameters { get; set; }
    }
}
