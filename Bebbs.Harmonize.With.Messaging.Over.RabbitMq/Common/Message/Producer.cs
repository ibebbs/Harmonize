using RabbitMQ.Client;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common.Message
{
    public static class Producer
    {
        internal static void Publish(string exchangeName, string routingKey, With.Message.ISerializer messageSerializer, RabbitMQ.Client.IModel model, With.Message.IMessage message)
        {
            string content = messageSerializer.Serialize(message);
            byte[] body = Encoding.Encode(content);
            IBasicProperties properties = model.CreateBasicProperties();

            model.BasicPublish(exchangeName, routingKey, properties, body);
        }
    }
}
