using CommandLine;

namespace Bebbs.Harmonize.Console
{
    public class Options
    {
        [Option("Harmony", DefaultValue = false, HelpText = "Harmonize with Harmony Remote", Required = false)]
        public bool WithHarmony { get; set; }

        [Option("Alljoyn", DefaultValue = true, HelpText = "Harmonize with Alljoyn", Required = false)]
        public bool UseAllJoyn { get; set; }

        [Option("Messaging", DefaultValue = true, HelpText = "Harmonize with Messaging", Required = false)]
        public bool UseMessaging { get; set; }

        [Option("Rabbit", DefaultValue = true, HelpText = "Harmonize with Messaging over RabbitMq", Required = false)]
        public bool UseRabbitMq { get; set; }
    }
}
