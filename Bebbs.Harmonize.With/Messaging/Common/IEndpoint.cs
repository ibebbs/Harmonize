using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Common
{
    public interface IEndpoint
    {
        void Initialize();

        void Cleanup();
    }
}
