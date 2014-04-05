using System.Collections.Generic;

namespace Bebbs.Harmonize.State.Component
{
    public class Actionable
    {
        public Identity Identity { get; set; }
        public Description Description { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
    }
}
