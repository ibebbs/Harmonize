using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.Harmony.Hub
{
    public interface IAction : With.Component.IActionable, With.Component.IDescription
    {
        string Label { get; }
        string Action { get; }
    }

    internal class Action : IAction
    {
        string With.Component.IDescription.Name
        {
            get { return label; }
        }

        string With.Component.IDescription.Remarks
        {
            get { return string.Empty; }
        }

        With.Component.IIdentity With.Component.IActionable.Identity
        {
            get { return new Identity(name); }
        }

        With.Component.IDescription With.Component.IActionable.Description
        {
            get { return this; }
        }

        IEnumerable<With.Component.IParameter> With.Component.IActionable.Parameters
        {
            get { return Enumerable.Empty<With.Component.IParameter>(); }
        }

        string IAction.Label
        {
            get { return label; }
        }

        string IAction.Action
        {
            get { return action; }
        }

        public string name { get; set; }
        public string label { get; set; }
        public string action { get; set; }
    }
}
