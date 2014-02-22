namespace Bebbs.Harmonize.With.Harmony.Hub
{
    internal class Command : With.Command.ICommand
    {
        public string command { get; set; }
        public string type { get; set; }
        public string deviceId { get; set; }
    }
}
