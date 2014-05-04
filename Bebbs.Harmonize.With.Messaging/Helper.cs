using Bebbs.Harmonize.With.Message;
using System;
using System.Text;

namespace Bebbs.Harmonize.With.Messaging
{
    public interface IHelper
    {
        IMessage Deserialize(byte[] source);

        byte[] Serialize(IMessage message);
    }

    internal class Helper : IHelper
    {
        private readonly Mapping.IHelper _mappingHelper;
        private readonly Serialization.IHelper _serializationHelper;
        private readonly Serialization.IWrapper _serializationWrapper;

        public Helper(Mapping.IHelper mappingHelper, Serialization.IHelper serializationHelper, Serialization.IWrapper serializationWrapper)
        {
            _mappingHelper = mappingHelper;
            _serializationHelper = serializationHelper;
            _serializationWrapper = serializationWrapper;
        }

        private IAct MapItem(Schema.Act act)
        {
            return _mappingHelper.ToComponent(act);
        }

        private IObservation MapItem(Schema.Observation observation)
        {
            return _mappingHelper.ToComponent(observation);
        }

        private IRegister MapItem(Schema.Register registration)
        {
            return _mappingHelper.ToComponent(registration);
        }

        private IDeregister MapItem(Schema.Deregister deregistration)
        {
            return _mappingHelper.ToComponent(deregistration);
        }

        private IMessage MapItem(object item)
        {
            throw new InvalidOperationException("Could not map item to known message type");
        }

        public IMessage Deserialize(byte[] source)
        {
            string text = Encoding.UTF8.GetString(source);

            Schema.Message message = _serializationHelper.Deserialize(text);

            dynamic item = message.Item;

            return MapItem(item);
        }

        public byte[] Serialize(IMessage message)
        {
            dynamic item = message;
            dynamic schema = _mappingHelper.ToSchema(item);

            Schema.Message wrapper = _serializationWrapper.Wrap(schema);

            string serialized = _serializationHelper.Serialize(wrapper);

            return Encoding.UTF8.GetBytes(serialized);
        }
    }
}
