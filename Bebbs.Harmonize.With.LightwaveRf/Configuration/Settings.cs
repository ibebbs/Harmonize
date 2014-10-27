
namespace Bebbs.Harmonize.With.LightwaveRf.Configuration
{
    public interface ISettings
    {
        string LocalIpAddress { get; }

        string WifiLinkIpAddress { get; }
        
        int CommandPort { get; }

        int QueryPort { get; }
    }

    public class Settings : ISettings
    {
        public string LocalIpAddress { get; set; }

        public string WifiLinkIpAddress { get; set; }

        public int CommandPort { get; set; }

        public int QueryPort { get; set; }
    }
}
