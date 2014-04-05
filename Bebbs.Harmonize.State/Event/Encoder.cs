using System.Text;

namespace Bebbs.Harmonize.State.Event
{
    public interface IEncoder
    {
        byte[] Encode(string text);
    }

    internal class Encoder : IEncoder
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        public byte[] Encode(string text)
        {
            return Encoding.GetBytes(text);
        }
    }
}
