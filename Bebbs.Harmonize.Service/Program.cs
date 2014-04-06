using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Options options = new Options();

            CommandLine.Parser.Default.ParseArguments(args, options);

            if (options.Console)
            {
                Controller controller = new Controller();
                controller.Start(options);

                Console.WriteLine("Hit enter to exit...");
                Console.ReadLine();
            }
            else
            {
                ServiceBase[] ServicesToRun = new ServiceBase[] { new Host() };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
