using Ninject;
using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway
{
    public interface IContext : IDisposable
    {
        void Initialize();
        void CleanUp();

        IKernel Kernel { get; }
        IInstance Instance { get; }
        IBridge Bridge { get; }
        Settings.IProvider SettingsProvider { get; }

    }

    internal class Context : IContext
    {
        public Context(IKernel kernel, IInstance instance, IBridge bridge, Settings.IProvider settingsProvider)
        {
            Kernel = kernel;
            Instance = instance;
            Bridge = bridge;
            SettingsProvider = settingsProvider;
        }

        public void Dispose()
        {
            CleanUp();

            if (Kernel != null)
            {
                Kernel.Dispose();
                Kernel = null;
            }
        }

        public void Initialize()
        {
            Bridge.Initialize();
            Instance.Initialize();
        }

        public void CleanUp()
        {
            Instance.Cleanup();
            Bridge.Cleanup();
        }

        public IKernel Kernel { get; private set; }
        public IInstance Instance { get; private set; }
        public IBridge Bridge { get; private set; }
        public Settings.IProvider SettingsProvider { get; private set; }
    }
}
