using Bebbs.Harmonize.With;
using System;

namespace Bebbs.Harmonize
{
    internal class Clock : IClock
    {
        public DateTimeOffset Now
        {
            get { return DateTimeOffset.Now; }
        }
    }
}
