using Bebbs.Harmonize.With.Text;
using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Response.Builder
{
    internal class SaveResponse : IBuilder
    {
        private const string StatusGroup = "Status";
        private const string SaveResponseName = "SaveResponse";
        private const string SaveResponsePattern = @"(?<Status>OK|ERROR),SAVE(?:,)?";

        public IResponse Build(System.Text.RegularExpressions.Match match)
        {
            Status status = match.ReadGroupAs<Status>(StatusGroup, value =>(Status)Enum.Parse(typeof(Status), value));

            return new Command.Response.Save(status);
        }

        public string Name
        {
            get { return SaveResponseName; }
        }

        public string Regex
        {
            get { return SaveResponsePattern; }
        }
    }
}
