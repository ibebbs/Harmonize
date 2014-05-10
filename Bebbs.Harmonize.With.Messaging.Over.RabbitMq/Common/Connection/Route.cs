using System;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common.Connection
{
    public interface IRoute
    {
        IRouteFrom From(string exchangeName);
    }

    public interface IRouteFrom
    {
        IRouteFromTo To(string queueName);
    }

    public interface IRouteFromTo
    {
        void Start();
        void Stop();
    }

    internal class Route : IRoute, IRouteFrom, IRouteFromTo
    {
        private readonly Action<string, string, string> _startRoute;
        private readonly Action<string, string, string> _stopRoute;
        private readonly string _routingKey;
        private readonly string _exchangeName;
        private readonly string _queueName;

        public Route(Action<string, string, string> startRoute, Action<string, string, string> stopRoute, string routingKey)
        {
            _startRoute = startRoute;
            _stopRoute = stopRoute;
            _routingKey = routingKey;
        }

        public Route(Action<string, string, string> startRoute, Action<string, string, string> stopRoute, string routingKey, string exchangeName) : this(startRoute, stopRoute, routingKey)
        {
            _exchangeName = exchangeName;
        }

        public Route(Action<string, string, string> startRoute, Action<string, string, string> stopRoute, string routingKey, string exchangeName, string queueName) : this(startRoute, stopRoute, routingKey, exchangeName)
        {
            _queueName = queueName;
        }

        IRouteFrom IRoute.From(string exchangeName)
        {
            return new Route(_startRoute, _stopRoute, _routingKey, exchangeName);
        }

        IRouteFromTo IRouteFrom.To(string queueName)
        {
            return new Route(_startRoute, _stopRoute, _routingKey, _exchangeName, queueName);
        }

        void IRouteFromTo.Start()
        {
            _startRoute(_routingKey, _exchangeName, _queueName);
        }

        void IRouteFromTo.Stop()
        {
            _stopRoute(_routingKey, _exchangeName, _queueName);
        }
    }
}
