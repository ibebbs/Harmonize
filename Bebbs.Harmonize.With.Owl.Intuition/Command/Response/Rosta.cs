using System;
using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Response
{
    public class Rosta : IResponse
    {
        public Rosta(Status status, IEnumerable<Tuple<int, string>> devices)
        {
            Status = status;
            Devices = (devices ?? Enumerable.Empty<Tuple<int, string>>()).ToArray();
        }

        public Status Status { get; private set; }
        public IEnumerable<Tuple<int, string>> Devices { get; private set; }
    }
}
