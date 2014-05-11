using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Actionable : With.Component.IActionable
    {
        With.Component.IIdentity With.Component.IActionable.Identity
        {
            get { return Identity; }
        }

        With.Component.IDescription With.Component.IActionable.Description
        {
            get { return Description; }
        }

        IEnumerable<With.Component.IParameter> With.Component.IActionable.Parameters
        {
            get { return Parameters; }
        }

        public Identity Identity { get; set; }
        public Description Description { get; set; }
        public Parameter[] Parameters { get; set; }
    }
}
