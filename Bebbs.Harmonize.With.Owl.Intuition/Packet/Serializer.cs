using Bebbs.Harmonize.With.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet
{
    public interface ISerializer
    {
        Electricity DeserializeElectricity(string xml);
    }

    internal class Serializer : ISerializer
    {
        private static readonly XmlSerializer<Electricity> ElectricitySerializer = new XmlSerializer<Electricity>();

        public Electricity DeserializeElectricity(string xml)
        {
            return ElectricitySerializer.Deserialize(xml);
        }
    }
}
