using Bebbs.Harmonize.With.Text;
using System;
using System.Text.RegularExpressions;

namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Response.Builder
{
    internal class VersionResponse : IBuilder
    {
        private const string StatusGroup = "Status";
        private const string FirmwareGroup = "Firmware";
        private const string VersionGroup = "Version";
        private const string BuildGroup = "Build";
        private const string VersionResponseGroup = "VersionResponse";
        private const string VersionResponsePattern = @"(?<Status>OK|ERROR),VERSION,(?<Firmware>.*),(?<Version>.*),(?<Build>.*)";

        public IResponse Build(Match match)
        {
            Status status = match.ReadGroupAs<Status>(StatusGroup, value => (Status)Enum.Parse(typeof(Status), value, true));
            string firmware = match.ReadGroupValue(FirmwareGroup);
            string version = match.ReadGroupValue(VersionGroup);
            string build = match.ReadGroupValue(BuildGroup);

            return new Command.Response.Version(status, firmware, version, build);
        }

        public string Name
        {
            get { return VersionResponseGroup; }
        }

        public string Regex
        {
            get { return VersionResponsePattern; }
        }
    }
}
