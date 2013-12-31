using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.Hub
{
    internal class Action : With.Component.IAction
    {
        private Command _command;

        private void CommandFromAction(string value)
        {
            string json = value.Trim('"');

            _command = JsonSerializer.DeserializeFromString<Command>(json);
        }

        private string CommandAsAction()
        {
            if (_command != null)
            {
                return JsonSerializer.SerializeToString<Command>(_command);
            }
            else
            {
                return string.Empty;
            }
        }

        With.Command.ICommand With.Component.IAction.Command 
        {
            get { return _command; } 
        }

        string With.Component.IAction.Name
        {
            get { return name; }
        }

        public string name { get; set; }
        public string label { get; set; }
        public string action 
        {
            get { return CommandAsAction(); }
            set { CommandFromAction(value); }
        }
    }
}
