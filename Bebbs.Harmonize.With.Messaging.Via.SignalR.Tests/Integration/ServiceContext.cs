using Bebbs.Harmonize.With.Component;
using Bebbs.Harmonize.With.Message;
using Bebbs.Harmonize.With.Messaging.Via.SignalR.Common;
using FakeItEasy;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Tests.Integration
{
    public class ServiceContext
    {
        private class Observe
        {
            public string ObserveeId { get; set; }
            public string ObservableId { get; set; }
            public Action<IMessage> Process { get; set; }
        }

        private Dictionary<string, IObserver<IMessage>> _entities;
        private List<Observe> _observers;

        public ServiceContext()
        {
            _entities = new Dictionary<string,IObserver<IMessage>>();
            _observers = new List<Observe>();

            Kernel = new StandardKernel();

            HarmonizeClientEndpoint = A.Fake<Messaging.Client.IEndpoint>();

            A.CallTo(() => HarmonizeClientEndpoint.Register(A<IIdentity>.Ignored, A<IEntity>.Ignored, A<IObserver<IMessage>>.Ignored))
             .Invokes(call => _entities.Add(call.GetArgument<IEntity>(1).Identity.Value, call.GetArgument<IObserver<IMessage>>(2)));

            A.CallTo(() => HarmonizeClientEndpoint.Observe(A<IIdentity>.Ignored, A<IIdentity>.Ignored, A<IIdentity>.Ignored))
             .Invokes(
                call =>
                {
                    IObserver<IMessage> observer;

                    if (_entities.TryGetValue(call.GetArgument<IIdentity>(0).Value, out observer))
                    {
                        _observers.Add(new Observe { ObserveeId = call.GetArgument<IIdentity>(1).Value, ObservableId = call.GetArgument<IIdentity>(2).Value, Process = message => observer.OnNext(message) });
                    }
                }
            );

            Kernel.Bind<Messaging.Client.IEndpoint>().ToConstant(HarmonizeClientEndpoint);
        }

        public void PublishObservation(SignalR.Common.Dto.Observation observation)
        {
            _observers.Where(observer => string.Equals(observer.ObserveeId, observation.Entity.Value, StringComparison.CurrentCultureIgnoreCase) && string.Equals(observer.ObservableId, observation.Observable.Value, StringComparison.CurrentCultureIgnoreCase))
                      .ForEach(observer => observer.Process(observation.AsDynamicMessage()));
        }

        public Ninject.IKernel Kernel { get; private set; }

        public Messaging.Client.IEndpoint HarmonizeClientEndpoint { get; private set; }
    }
}
