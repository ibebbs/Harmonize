using System.ComponentModel;
using System.ServiceProcess;

namespace Bebbs.Harmonize.Service
{
    [RunInstaller(true)]
    public class Installer : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;

        public Installer()
        {
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = "Harmonize";
            serviceInstaller.DisplayName = "Bebbs Harmonizer Service";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}
