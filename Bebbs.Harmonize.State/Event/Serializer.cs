using ServiceStack.Text;

namespace Bebbs.Harmonize.State.Event
{
    public interface ISerializer
    {
        string Serialize(Observed message);
        string Serialize(Stopped message);
        string Serialize(Started message);
        string Serialize(Deregistered message);
        string Serialize(Registered message);
    }

    internal class Serializer : ISerializer
    {
        public string Serialize(Observed message)
        {
            return JsonSerializer.SerializeToString<Observed>(message);
        }

        public string Serialize(Stopped message)
        {
            return JsonSerializer.SerializeToString<Stopped>(message);
        }

        public string Serialize(Started message)
        {
            return JsonSerializer.SerializeToString<Started>(message);
        }

        public string Serialize(Deregistered message)
        {
            return JsonSerializer.SerializeToString<Deregistered>(message);
        }

        public string Serialize(Registered message)
        {
            return JsonSerializer.SerializeToString<Registered>(message);
        }
    }
}
