using Bebbs.Harmonize.With;
using System;

namespace Bebbs.Harmonize.Host
{
    internal class Clock : IClock
    {
        public DateTimeOffset Now
        {
            get { return DateTimeOffset.Now; }
        }
    }
}
