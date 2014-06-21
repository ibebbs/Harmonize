using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System.Reactive.Subjects;
using System.Reactive;
using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Tests
{
    [TestClass]
    public class EndpointTests
    {
        private async Task RunTest(Func<Hub.IFactory, Hub.IInstance, Registration.IFactory, Endpoint, Task> test)
        {
            Hub.IInstance hub = A.Fake<Hub.IInstance>();
            A.CallTo(() => hub.Start()).Returns(TaskEx.Done);

            Hub.IFactory hubFactory = A.Fake<Hub.IFactory>();
            A.CallTo(() => hubFactory.Create()).Returns(hub);

            Registration.IFactory registrationFactory = A.Fake<Registration.IFactory>();
            Mapper mapper = new Mapper();

            Endpoint subject = new Endpoint(hubFactory, registrationFactory, mapper);

            await test(hubFactory, hub, registrationFactory, subject);
        }

        [TestMethod]
        public async Task ShouldCreateHubOnInitialize()
        {
            await RunTest(
                async (hubFactory, hub, registrationFactory, subject) =>
                {
                    await subject.Initialize();

                    A.CallTo(() => hubFactory.Create()).MustHaveHappened(Repeated.Exactly.Once);
                }
            );
        }

        [TestMethod]
        public async Task ShouldSubscribeToProcessMessageOnInitialize()
        {
            await RunTest(
                async (hubFactory, hub, registrationFactory, subject) =>
                {
                    IObservable<Tuple<Common.Identity, Common.Identity, Common.Message>> observable = A.Fake<IObservable<Tuple<Common.Identity, Common.Identity, Common.Message>>>();
                    A.CallTo(() => hub.GetEvent<Common.Identity, Common.Identity, Common.Message>("Process")).Returns(observable);
                    await subject.Initialize();

                    A.CallTo(() => observable.Subscribe(A<IObserver<Tuple<Common.Identity, Common.Identity, Common.Message>>>.Ignored))
                     .MustHaveHappened(Repeated.Exactly.Once);
                }
            );
        }

        [TestMethod]
        public async Task ShouldStartHubOnInitialize()
        {
            await RunTest(
                async (hubFactory, hub, registrationFactory, subject) =>
                {
                    await subject.Initialize();

                    A.CallTo(() => hub.Start()).MustHaveHappened(Repeated.Exactly.Once);
                }
            );
        }

        [TestMethod]
        public async Task ShouldRegisterEntity()
        {
            await RunTest(
                async (hubFactory, hub, registrationFactory, subject) =>
                {
                    IObservable<Tuple<Common.Identity, Common.Identity, Common.Message>> observable = A.Fake<IObservable<Tuple<Common.Identity, Common.Identity, Common.Message>>>();
                    A.CallTo(() => hub.GetEvent<Common.Identity, Common.Identity, Common.Message>("Process")).Returns(observable);

                    With.Component.IIdentity registrar = new With.Component.Identity("registrar");
                    With.Component.IEntity entity = new With.Component.Entity(new With.Component.Identity("entity"), null, null, null);

                    IObserver<Message.IMessage> observer = A.Fake<IObserver<Message.IMessage>>();

                    await subject.Initialize();

                    await subject.Register(registrar, entity, observer);

                    A.CallTo(() => hub.Register(A<Common.Entity>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
                }
            );
        }

        [TestMethod]
        public async Task ShouldDeregisterEntity()
        {
            await RunTest(
                async (hubFactory, hub, registrationFactory, subject) =>
                {
                    IObservable<Tuple<Common.Identity, Common.Identity, Common.Message>> observable = A.Fake<IObservable<Tuple<Common.Identity, Common.Identity, Common.Message>>>();
                    A.CallTo(() => hub.GetEvent<Common.Identity, Common.Identity, Common.Message>("Process")).Returns(observable);

                    With.Component.IIdentity componentRegistrar = new With.Component.Identity("registrar");
                    With.Component.IEntity componentEntity = new With.Component.Entity(new With.Component.Identity("entity"), null, null, null);
                    Common.Identity commonRegistrar = new Common.Identity { Value = "registrar" };
                    Common.Identity commonEntityIdentity = new Common.Identity { Value = "entity" };
                    Common.Entity commonEntity = new Common.Entity { Identity = commonEntityIdentity };

                    IObserver<Message.IMessage> process = A.Fake<IObserver<Message.IMessage>>();

                    A.CallTo(() => registrationFactory.For(componentRegistrar, componentEntity, process)).Returns(new Registration.Instance(commonRegistrar, commonEntity, process));

                    await subject.Initialize();

                    await subject.Register(componentRegistrar, componentEntity, process);

                    await subject.Deregister(componentRegistrar, componentEntity);

                    A.CallTo(() => hub.Deregister(A<Common.Entity>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
                }
            );
        }

        [TestMethod]
        public async Task ShouldObserveEntity()
        {
            await RunTest(
                async (hubFactory, hub, registrationFactory, subject) =>
                {
                    IObservable<Tuple<Common.Identity, Common.Identity, Common.Message>> messages = A.Fake<IObservable<Tuple<Common.Identity, Common.Identity, Common.Message>>>();
                    A.CallTo(() => hub.GetEvent<Common.Identity, Common.Identity, Common.Message>("Process")).Returns(messages);

                    With.Component.IIdentity componentRegistrar = new With.Component.Identity("registrar");
                    With.Component.IIdentity componentEntityIdentity = new With.Component.Identity("entity");
                    With.Component.IEntity componentEntity = new With.Component.Entity(componentEntityIdentity, null, null, null);
                    Common.Identity commonRegistrar = new Common.Identity { Value = "registrar" };
                    Common.Identity commonEntityIdentity = new Common.Identity { Value = "entity" };
                    Common.Entity commonEntity = new Common.Entity { Identity = commonEntityIdentity };
                    With.Component.IIdentity observableEntity = new With.Component.Identity("observer");
                    With.Component.IIdentity observable = new With.Component.Identity("observable");

                    IObserver<Message.IMessage> process = A.Fake<IObserver<Message.IMessage>>();

                    A.CallTo(() => registrationFactory.For(componentRegistrar, componentEntity, process)).Returns(new Registration.Instance(commonRegistrar, commonEntity, process));

                    await subject.Initialize();

                    await subject.Register(componentRegistrar, componentEntity, process);

                    await subject.Observe(componentRegistrar, componentEntityIdentity, observableEntity, observable);

                    A.CallTo(() => hub.Observe(A<Common.Identity>.Ignored, A<Common.Identity>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
                }
            );
        }

        [TestMethod]
        public async Task ShouldDispatchMessageToRegistrationWhenReceived()
        {
            await RunTest(
                async (hubFactory, hub, registrationFactory, subject) =>
                {
                    Subject<Tuple<Common.Identity, Common.Identity, Common.Message>> observable = new Subject<Tuple<Common.Identity, Common.Identity, Common.Message>>();
                    A.CallTo(() => hub.GetEvent<Common.Identity, Common.Identity, Common.Message>("Process")).Returns(observable);
                                        
                    With.Component.IIdentity componentRegistrar = new With.Component.Identity("registrar");
                    With.Component.IEntity componentEntity = new With.Component.Entity(new With.Component.Identity("entity"), null, null, null);
                    Common.Identity commonRegistrar = new Common.Identity { Value = "registrar" };
                    Common.Identity commonEntityIdentity = new Common.Identity { Value = "entity" };
                    Common.Entity commonEntity = new Common.Entity { Identity = commonEntityIdentity };
                    
                    List<Message.IMessage> messages = new List<Message.IMessage>();
                    IObserver<Message.IMessage> observer = Observer.Create<Message.IMessage>(messages.Add);

                    A.CallTo(() => registrationFactory.For(componentRegistrar, componentEntity, observer)).Returns(new Registration.Instance(commonRegistrar, commonEntity, observer));

                    await subject.Initialize();

                    await subject.Register(componentRegistrar, componentEntity, observer);

                    observable.OnNext(Tuple.Create<Common.Identity, Common.Identity, Common.Message>(commonRegistrar, commonEntityIdentity, new Common.Message()));

                    Assert.AreEqual<int>(1, messages.Count);
                }
            );
        }

        [TestMethod]
        public async Task ShouldDispatchMessageToCorrectRegistrationWhenReceived()
        {
            await RunTest(
                async (hubFactory, hub, registrationFactory, subject) =>
                {
                    Subject<Tuple<Common.Identity, Common.Identity, Common.Message>> observable = new Subject<Tuple<Common.Identity, Common.Identity, Common.Message>>();
                    A.CallTo(() => hub.GetEvent<Common.Identity, Common.Identity, Common.Message>("Process")).Returns(observable);

                    With.Component.IIdentity componentRegistrar = new With.Component.Identity("registrar");
                    With.Component.IEntity componentEntity = new With.Component.Entity(new With.Component.Identity("entity"), null, null, null);
                    Common.Identity commonRegistrar = new Common.Identity { Value = "registrar" };
                    Common.Identity commonEntityIdentity = new Common.Identity { Value = "entity" };
                    Common.Entity commonEntity = new Common.Entity { Identity = commonEntityIdentity };

                    List<Message.IMessage> messages = new List<Message.IMessage>();
                    IObserver<Message.IMessage> observer = Observer.Create<Message.IMessage>(messages.Add);

                    A.CallTo(() => registrationFactory.For(componentRegistrar, componentEntity, observer)).Returns(new Registration.Instance(commonRegistrar, commonEntity, observer));

                    await subject.Initialize();

                    await subject.Register(componentRegistrar, componentEntity, observer);

                    observable.OnNext(Tuple.Create<Common.Identity, Common.Identity, Common.Message>(new Common.Identity { Value = "registrar2" }, new Common.Identity { Value = "entity2" }, new Common.Message()));

                    Assert.AreEqual<int>(0, messages.Count);
                }
            );
        }

        [TestMethod]
        public void ShouldRemoveRegistrationWhenEntityIsDeregistered()
        {
        }
    }
}
