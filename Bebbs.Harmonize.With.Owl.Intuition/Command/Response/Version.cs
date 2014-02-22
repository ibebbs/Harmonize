
namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Response
{
    public class Version : IResponse
    {
        public Version(Status status, string firmware, string revision, string build)
        {
            Status = status;
            Firmware = firmware;
            Revision = revision;
            Build = build;
        }

        public Status Status { get; private set; }
        public string Firmware { get; private set; }
        public string Revision { get; private set; }
        public string Build { get; private set; }

    }
}
