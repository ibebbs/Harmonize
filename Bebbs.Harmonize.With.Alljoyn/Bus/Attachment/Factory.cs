using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Attachment
{
    internal interface IFactory
    {
        IInstance Create();
    }

    internal class Factory : IFactory
    {
        private readonly IDescription _description;

        public Factory(IDescription description)
        {
            _description = description;
        }

        public IInstance Create()
        {
            return new Instance(_description);
        }
    }
}
