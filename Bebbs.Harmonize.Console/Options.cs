using CommandLine;
using CommandLine.Text;

namespace Bebbs.Harmonize.Console
{
    public class Options
    {
        [HelpOption('h', "help")]
        public string GetUsage()
        {
            ShowingHelp = true;
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        public bool ShowingHelp { get; set; }

        [Option("harmony", DefaultValue = false, HelpText = "Harmonize with Harmony Remote", Required = false)]
        public bool WithHarmony { get; set; }

        [Option('a', "alljoyn", DefaultValue = true, HelpText = "Harmonize with Alljoyn", Required = false)]
        public bool UseAllJoyn { get; set; }

        [Option('m', "messaging", DefaultValue = true, HelpText = "Harmonize with Messaging", Required = false)]
        public bool UseMessaging { get; set; }

        [Option('r', "rabbit", DefaultValue = true, HelpText = "Harmonize with Messaging over RabbitMq", Required = false)]
        public bool UseRabbitMq { get; set; }
    }
}
