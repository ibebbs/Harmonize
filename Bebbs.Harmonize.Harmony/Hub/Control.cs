using System;

namespace Bebbs.Harmonize.Harmony.Hub
{
    public interface IControl
    {
        string Name { get; }

        IAction[] Actions { get; }
    }

    internal class Control : IControl
    {
        string IControl.Name
        {
            get { return name; }
        }

        IAction[] IControl.Actions
        {
            get { return function; }
        }

        public string name { get; set; }

        public Action[] function { get; set; }
    }
}
