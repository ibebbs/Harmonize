using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Actionable : With.Component.IActionable
    {
        Component.IIdentity Component.IActionable.Identity
        {
            get { return Identity; }
        }

        Component.IDescription Component.IActionable.Description
        {
            get { return Description; }
        }

        IEnumerable<Component.IParameter> Component.IActionable.Parameters
        {
            get { return Parameters; }
        }

        public Identity Identity { get; set; }
        public Description Description { get; set; }
        public Parameter[] Parameters { get; set; }
    }
}
