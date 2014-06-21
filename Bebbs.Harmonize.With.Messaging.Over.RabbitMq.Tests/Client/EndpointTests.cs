using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using FakeItEasy;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Tests.Client
{
    [TestClass]
    public class EndpointTests
    {
        private static readonly string ExchangeName = "EXCHANGE";

        private async Task RunTest(Func<RabbitMq.Client.Configuration.ISettings, RabbitMq.Common.Connection.IFactory, RabbitMq.Client.Endpoint, Task> test)
        {
            RabbitMq.Client.Configuration.ISettings settings = A.Fake<RabbitMq.Client.Configuration.ISettings>();
            A.CallTo(() => settings.ExchangeName).Returns(ExchangeName);

            RabbitMq.Common.Connection.IFactory connectionFactory = A.Fake<RabbitMq.Common.Connection.IFactory>();

            RabbitMq.Common.Routing.IKey key = new RabbitMq.Common.Routing.Key();
            RabbitMq.Common.Queue.IName name = new RabbitMq.Common.Queue.Name();

            RabbitMq.Client.Endpoint endpoint = new RabbitMq.Client.Endpoint(settings, connectionFactory, key, name);

            await test(settings, connectionFactory, endpoint);
        }

        [TestMethod]
        public async Task ShouldConnectOnInitialize()
        {
            await RunTest(
                async (settings, connectionFactory, endpoint) =>
                {
                    await endpoint.Initialize();

                    A.CallTo(() => connectionFactory.Create(settings)).MustHaveHappened(Repeated.Exactly.Once);
                }
            );
        }

        [TestMethod]
        public async Task ShouldBuildQueueOnRegister()
        {
            await RunTest(
                async (settings, connectionFactory, endpoint) =>
                {
                    RabbitMq.Common.Connection.IInstance connection = A.Fake<RabbitMq.Common.Connection.IInstance>();

                    A.CallTo(() => connectionFactory.Create(settings)).Returns(connection);

                    await endpoint.Initialize();

                    With.Component.IIdentity registrar = new With.Component.Identity("registrar");
                    With.Component.IIdentity entityIdentity = new With.Component.Identity("entity");
                    With.Component.IEntity entity = new With.Component.Entity(entityIdentity, null, null, null);

                    IObserver<With.Message.IMessage> consumer = A.Fake<IObserver<With.Message.IMessage>>();

                    endpoint.Register(registrar, entity, consumer);

                    A.CallTo(() => connection.BuildQueue("entity")).MustHaveHappened(Repeated.Exactly.Once);
                }
            );
        }

        [TestMethod]
        public async Task ShouldBindConsumerOnRegister()
        {
            await RunTest(
                async (settings, connectionFactory, endpoint) =>
                {
                    RabbitMq.Common.Connection.IInstance connection = A.Fake<RabbitMq.Common.Connection.IInstance>();

                    A.CallTo(() => connectionFactory.Create(settings)).Returns(connection);

                    await endpoint.Initialize();

                    With.Component.IIdentity registrar = new With.Component.Identity("registrar");
                    With.Component.IIdentity entityIdentity = new With.Component.Identity("entity");
                    With.Component.IEntity entity = new With.Component.Entity(entityIdentity, null, null, null);

                    IObserver<With.Message.IMessage> consumer = A.Fake<IObserver<With.Message.IMessage>>();

                    endpoint.Register(registrar, entity, consumer);

                    A.CallTo(() => connection.BindConsumer("entity", consumer)).MustHaveHappened(Repeated.Exactly.Once);
                }
            );
        }

        [TestMethod]
        public async Task ShouldPublishRegistrationMessageOnRegister()
        {
            await RunTest(
                async (settings, connectionFactory, endpoint) =>
                {
                    RabbitMq.Common.Connection.IInstance connection = A.Fake<RabbitMq.Common.Connection.IInstance>();

                    A.CallTo(() => connectionFactory.Create(settings)).Returns(connection);

                    await endpoint.Initialize();

                    With.Component.IIdentity registrar = new With.Component.Identity("registrar");
                    With.Component.IIdentity entityIdentity = new With.Component.Identity("entity");
                    With.Component.IEntity entity = new With.Component.Entity(entityIdentity, null, null, null);

                    IObserver<With.Message.IMessage> consumer = A.Fake<IObserver<With.Message.IMessage>>();

                    endpoint.Register(registrar, entity, consumer);

                    A.CallTo(() => connection.Publish(ExchangeName, "REGISTRATION.entity", A<With.Message.IMessage>.That.IsInstanceOf(typeof(With.Message.IRegister)))).MustHaveHappened(Repeated.Exactly.Once);
                }
            );
        }
    }
}
