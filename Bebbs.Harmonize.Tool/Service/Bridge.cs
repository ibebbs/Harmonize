using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Tool.Service
{
    public interface IBridge
    {
        Task Start();

        Task Stop();

        Task Register();

        Task Action();
    }

    internal class Bridge : IBridge
    {
        private Host.Container<Instance> _container;
        private Entity _entity;

        public Bridge()
        {
            _container = new Host.Container<Instance>(new Module());
            _entity = new Entity("Bridge");
        }

        public Task Start()
        {
            return _container.Start();
        }

        public Task Stop()
        {
            return _container.Stop();
        }

        public Task Register()
        {
            return _container.Service.Register(_entity);
        }

        public Task Action()
        {
            return _container.Service.Action(new With.Component.Identity("R1-D1-DIMMER-Roof Light"), new With.Component.Identity("R1-D1-DIMMER-Roof Light:ON"), Enumerable.Empty<With.Component.IParameterValue>());
        }
    }
}
