
namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common.Message
{
    public static class Encoding
    {
        public static byte[] Encode(string content)
        {
            return System.Text.Encoding.UTF8.GetBytes(content);
        }

        public static string Decode(byte[] content)
        {
            return System.Text.Encoding.UTF8.GetString(content);
        }
    }
}
