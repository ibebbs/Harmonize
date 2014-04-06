using Bebbs.Harmonize.With.Serialization;
using System;
using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet
{
    public interface IParser
    {
        IEnumerable<IReading> GetReadings(string packets);
    }

    internal class Parser : IParser
    {
        public static readonly XmlSerializer<Wrapper> Serializer = new XmlSerializer<Wrapper>();

        private Wrapper GetWrapper(string packets)
        {
            try
            {
                string wrapped = Wrapper.Wrap(packets);

                Wrapper wrapper = Serializer.Deserialize(wrapped);

                return wrapper;
            }
            catch (Exception e)
            {
                Instrumentation.Packet.Parser.Error(e);

                return new Wrapper();
            }
        }

        public IEnumerable<IReading> GetReadings(string packets)
        {
            Wrapper wrapper = GetWrapper(packets);

            if (wrapper.Electricity != null)
            {
                yield return wrapper.Electricity;
            }
        }
    }
}
