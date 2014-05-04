using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Config = SimpleConfig.Configuration;

namespace Bebbs.Harmonize.Host.Configuration
{
    public interface IProvider
    {
        ISettings GetSettings();
    }

    internal class Provider : IProvider
    {
        public ISettings GetSettings()
        {
            return Config.Load<Settings>(sectionName: "harmonize");
        }
    }
}
