using CommandLine;

namespace Bebbs.Harmonize.Console
{
    public class Options
    {
        [Option("Alljoyn", DefaultValue = true, HelpText = "Harmonize with Alljoyn", Required = false)]
        public bool UseAllJoyn { get; set; }

        [Option("Messaging", DefaultValue = true, HelpText = "Harmonize with Messaging", Required = false)]
        public bool UseMessaging { get; set; }
    }
}
