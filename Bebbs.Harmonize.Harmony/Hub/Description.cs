using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.Hub
{
    internal class Description : With.Component.IDescription
    {
        public Description(string name, string type, string manufacturer, string model)
        {
            Name = name;
            Type = type;
            Manufacturer = manufacturer;
            Model = model;
        }

        public string Name { get; private set; }

        public string Type { get; private set; }

        public string Manufacturer { get; private set; }

        public string Model { get; private set; }
    }
}
