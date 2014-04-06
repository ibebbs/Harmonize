using CommandLine;
using CommandLine.Text;
using System;
using System.Configuration;

namespace Bebbs.Harmonize.Service
{
    public class Options
    {
        private static readonly Lazy<string> InternalTraceNames = new Lazy<string>(() => ConfigurationManager.AppSettings["TraceNames"]);

        [HelpOption('h', "help")]
        public string GetUsage()
        {
            ShowingHelp = true;
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        public bool ShowingHelp { get; set; }

        [Option("modulePattern", DefaultValue = "./Modules/*.dll", HelpText = "Gets the directory and pattern used to load modules (i.e. './Modules/*.dll')", Required = false)]
        public string ModulePattern { get; set; }

        [Option("logPath", DefaultValue = "./Logs/Harmonize.log", HelpText = "Gets the path to log to", Required = false)]
        public string LogPath { get; set; }

        [Option("console", DefaultValue = false, HelpText = "True to run the service as a console application", Required = false)]
        public bool Console { get; set; }

        public string TraceNames
        {
            get { return InternalTraceNames.Value; }
        }
    }
}
