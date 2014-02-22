
namespace Bebbs.Harmonize.With.Owl.Intuition.Values
{
    public class Version
    {
        public Version(string firmware, string revision, string build)
        {
            Firmware = firmware;
            Revision = revision;
            Build = build;
        }

        public string Firmware { get; private set; }
        public string Revision { get; private set; }
        public string Build { get; private set; }
    }
}
