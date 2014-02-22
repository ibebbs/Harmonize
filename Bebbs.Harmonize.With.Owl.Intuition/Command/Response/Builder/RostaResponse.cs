using Bebbs.Harmonize.With.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Response.Builder
{
    internal class RostaResponse : IBuilder
    {
        private const string StatusGroup = "Status";
        private const string DeviceGroup = "Device";
        private const string NoDevice = "NONE";
        private const string RostaResponseGroup = "RostaResponse";
        private const string RostaResponsePattern = @"(?<Status>OK|ERROR),DEVICE(?:,(?<Device>\w+))*";

        public IResponse Build(Match match)
        {
            Status status = match.ReadGroupAs<Status>(StatusGroup, value => (Status)Enum.Parse(typeof(Status), value, true));
            IEnumerable<Tuple<int, string>> devices = 
                match.Groups[DeviceGroup].Captures.OfType<Capture>()
                     .Where(capture => !string.Equals(capture.Value, NoDevice, StringComparison.CurrentCultureIgnoreCase))
                     .Select((capture, index) => Tuple.Create<int, string>(index, capture.Value))
                     .ToArray(); 

            return new Command.Response.Rosta(status, devices);
        }

        public string Name
        {
            get { return RostaResponseGroup; }
        }

        public string Regex
        {
            get { return RostaResponsePattern; }
        }
    }
}
