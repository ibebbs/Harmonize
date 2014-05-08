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
        Messaging.Client.IEndpoint Endpoint { get; }
        Settings.IProvider SettingsProvider { get; }

    }

    internal class Context : IContext
    {
        public Context(IKernel kernel, IInstance instance, Messaging.Client.IEndpoint clientEndpoint, Settings.IProvider settingsProvider)
        {
            Kernel = kernel;
            Instance = instance;
            Endpoint = clientEndpoint;
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
            Instance.Initialize();
        }

        public void CleanUp()
        {
            Instance.Cleanup();
        }

        public IKernel Kernel { get; private set; }
        public IInstance Instance { get; private set; }
        public Messaging.Client.IEndpoint Endpoint { get; private set; }
        public Settings.IProvider SettingsProvider { get; private set; }
    }
}
