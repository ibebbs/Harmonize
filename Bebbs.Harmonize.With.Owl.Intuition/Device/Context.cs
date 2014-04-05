using Ninject;
using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.Device
{
    public interface IContext : IDisposable
    {
        IKernel Kernel { get; }
        IInstance Instance { get; }
        Settings.IProvider SettingsProvider { get; }
    }

    internal class Context : IContext
    {
        public Context(IKernel kernel, IInstance instance, Settings.IProvider settingsProvider)
        {
            Kernel = kernel;
            Instance = instance;
            SettingsProvider = settingsProvider;
        }

        public void Dispose()
        {
            if (Kernel != null)
            {
                Kernel.Dispose();
                Kernel = null;
            }
        }

        public IKernel Kernel { get; private set; }
        public IInstance Instance { get; private set; }
        public Settings.IProvider SettingsProvider { get; private set; }
    }
}
