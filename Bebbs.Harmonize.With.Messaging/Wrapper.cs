using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging
{
    public interface IWrapper
    {
        Schema.Message Wrap(Schema.Register item);

        Schema.Message Wrap(Schema.Deregister item);

        Schema.Message Wrap(Schema.Observation item);

        Schema.Message Wrap(Schema.Act item);
    }

    internal class Wrapper : IWrapper
    {
        public Schema.Message Wrap(Schema.Register item)
        {
            return new Schema.Message { Item = item };
        }

        public Schema.Message Wrap(Schema.Deregister item)
        {
            return new Schema.Message { Item = item };
        }

        public Schema.Message Wrap(Schema.Observation item)
        {
            return new Schema.Message { Item = item };
        }

        public Schema.Message Wrap(Schema.Act item)
        {
            return new Schema.Message { Item = item };
        }
    }
}
