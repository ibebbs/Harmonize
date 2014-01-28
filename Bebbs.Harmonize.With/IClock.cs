using System;

namespace Bebbs.Harmonize.With
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
    }
}
