using System;

namespace Bebbs.Harmonize.Common.Settings
{
    public interface IProvider
    {
        IValues GetValues();
    }
}
