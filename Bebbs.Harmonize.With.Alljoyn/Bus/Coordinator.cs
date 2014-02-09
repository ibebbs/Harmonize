using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus
{
    public interface ICoordinator
    {
        void Start();

        void Add(Component.IEntity device);

        void Remove(Component.IEntity device);

        void Stop();
    }

    internal class Coordinator : ICoordinator
    {
        private readonly Bus.Attachment.IFactory _attachmentFactory;
        private readonly Bus.Object.IFactory _objectFactory;

        private readonly Dictionary<string, Object.Instance> _objects;

        private Bus.Attachment.IInstance _busAttachment;

        public Coordinator(Bus.Attachment.IFactory attachmentFactory, Bus.Object.IFactory objectFactory)
        {
            _attachmentFactory = attachmentFactory;
            _objectFactory = objectFactory;

            _objects = new Dictionary<string, Object.Instance>();
        }

        private void Add(Object.Description description)
        {
            if (_objects.ContainsKey(description.Path))
            {
                throw new InvalidOperationException("A bus object instance with the same path has already been added");
            }
            else
            {
                Object.Instance instance = _objectFactory.Build(description);

                _busAttachment.Add(instance);

                _objects.Add(description.Path, instance);
            }
        }

        private void Remove(string path)
        {
            Object.Instance instance;

            if (_objects.TryGetValue(path, out instance))
            {
                _busAttachment.Remove(instance);

                _objects.Remove(path);
            }
        }

        public void Start()
        {
            _busAttachment = _attachmentFactory.Create();
            _busAttachment.Start();
        }

        public void Add(Component.IEntity device)
        {
            Object.Description description = _objectFactory.Describe(device);

            Add(description);
        }

        public void Remove(Component.IEntity device)
        {
            Object.Description description = _objectFactory.Describe(device);

            Remove(description.Path);
        }

        public void Stop()
        {
            while (_objects.Any())
            {
                Remove(_objects.First().Key);
            }

            if (_busAttachment != null)
            {
                _busAttachment.Dispose();
                _busAttachment = null;
            }
        }
    }
}
