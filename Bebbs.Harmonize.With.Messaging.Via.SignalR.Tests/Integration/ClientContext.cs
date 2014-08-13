using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using FakeItEasy;
using Bebbs.Harmonize.With.Component;
using Bebbs.Harmonize.With.Message;
using Bebbs.Harmonize.With.Messaging.Via.SignalR.Common;
using System.Reactive;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Tests.Integration
{
    public class ClientContext
    {
        public ClientContext()
        {
            RegistrationFactory = A.Fake<SignalR.Client.Registration.IFactory>();
            A.CallTo(() => RegistrationFactory.For(A<IIdentity>.Ignored, A<IEntity>.Ignored, A<IObserver<IMessage>>.Ignored))
             .ReturnsLazily(
                call =>
                {
                    IIdentity identity = call.GetArgument<IIdentity>(0);
                    IEntity entity = call.GetArgument<IEntity>(1);
                    IObserver<IMessage> observer = call.GetArgument<IObserver<IMessage>>(2);

                    return new SignalR.Client.Registration.Instance(identity.AsDto(), entity.AsDto(), observer);
                }
            );

            Kernel = new Ninject.StandardKernel();
            Kernel.Load(new[] { new SignalR.Client.Module() });
            Kernel.Bind<SignalR.Client.Registration.IFactory>().ToConstant(RegistrationFactory).InSingletonScope();
        }

        public Ninject.IKernel Kernel { get; private set; }

        public SignalR.Client.Registration.IFactory RegistrationFactory { get; private set; }

        public T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
