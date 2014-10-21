
namespace Bebbs.Harmonize.With.LightwaveRf.Configuration
{
    public interface ISettings
    {
        string IpAddress { get; }
        
        uint CommandPort { get; }

        uint QueryPort { get; }
    }

    public class Settings : ISettings
    {
        public string IpAddress { get; set; }

        public uint CommandPort { get; set; }

        public uint QueryPort { get; set; }
    }
}
