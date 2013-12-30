using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.Hub.Configuration
{
    public interface IParser
    {
        IValues FromJson(string json);
    }

    internal class Parser : IParser
    {
        public IValues FromJson(string json)
        {
            IValues values = JsonSerializer.DeserializeFromString<Values>(json);

            return values;
        }
    }
}
