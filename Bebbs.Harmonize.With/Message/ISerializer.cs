
namespace Bebbs.Harmonize.With.Message
{
    public interface ISerializer
    {
        string Serialize(IMessage message);
        IMessage Deserialize(string message);
    }
}
