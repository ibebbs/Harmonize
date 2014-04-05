using Bebbs.Harmonize.With.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity
{
    internal class Description : IEntityDescription
    {
        public Description(string name, string remarks)
        {
            Name = name;
            Remarks = remarks;
        }

        public string Name { get; private set; }

        public string Remarks { get; private set; }

        public string Type
        {
            get { return "Energy Monitor"; }
        }

        public string Manufacturer
        {
            get { return "Owl"; }
        }

        public string Model
        {
            get { return "Intuition"; }
        }
    }
}
