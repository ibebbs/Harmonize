using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Ninject;
using System.Reactive;
using Bebbs.Harmonize.With.Component;
using Bebbs.Harmonize.With.Message;
using FakeItEasy;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Tests.Integration
{
    [Binding]
    public class Steps
    {
        private const string RegistrarId = "4F2934B2-467F-47D0-AF05-333B7B1E428D";

        private readonly ServiceContext _serviceContext;
        private readonly ClientContext _clientContext;

        private Service.Bootstrapper _bootstrapper;
        private Client.IEndpoint _client;

        ReplaySubject<Tuple<string, IMessage>> _messages;

        public Steps(ServiceContext serviceContext, ClientContext clientContext)
        {
            _serviceContext = serviceContext;
            _clientContext = clientContext;

            _messages = new ReplaySubject<Tuple<string, IMessage>>();
        }

        [BeforeScenario]
        public void BeforeFeature()
        {
            _bootstrapper = new Service.Bootstrapper(_serviceContext.Kernel);
            _bootstrapper.Initialize().Wait();
            _bootstrapper.Start().Wait();
        }

        [AfterScenario]
        public void AfterFeature()
        {
            _bootstrapper.Stop().Wait();
            _bootstrapper.Cleanup().Wait();
            _bootstrapper = null;
        }

        [Given(@"I have started the SignalR endpoint")]
        public void GivenIHaveStartedTheSignalREndpoint()
        {
        }

        [Given(@"a client has connected")]
        public void GivenIAClientHasConnected()
        {
            _client = _clientContext.Get<SignalR.Client.IEndpoint>();
            _client.Initialize().Wait();
        }

        [Given(@"the client has registered an entity identified as '(.*)'")]
        [When(@"the client registers an entity identified as '(.*)'")]
        public void WhenTheClientRegistersAnEntityIdentifiedAs(string entityId)
        {
            _client.Register(new Identity(RegistrarId), new Entity(new Identity(entityId), null, null, null), Observer.Create<IMessage>(message => _messages.OnNext(Tuple.Create(entityId, message)))).Wait();
        }

        [When(@"the client deregisters the entity identified as '(.*)'")]
        public void WhenTheClientDeregistersTheEntityIdentifiedAs(string entityId)
        {
            _client.Deregister(new Identity(RegistrarId), new Entity(new Identity(entityId), null, null, null)).Wait();
        }

        [Given(@"the entity identified as '(.*)' observes the observable identified as '(.*)' on the entity identified as '(.*)'")]
        [When(@"the entity identified as '(.*)' observes the observable identified as '(.*)' on the entity identified as '(.*)'")]
        public void WhenTheEntityIdentifiedAsObservesTheObservableIdentifiedAsOnTheEntityIdentifiedAs(string observerId, string observableId, string observeeId)
        {
            _client.Observe(new Identity(RegistrarId), new Identity(observerId), new Identity(observeeId), new Identity(observableId)).Wait();
        }

        [When(@"the following observation is emitted")]
        public void WhenTheObservableIdentifiedAsOfTheEntityIdentifiedAsEmitsTheFollowingObservation(Table table)
        {
            SignalR.Common.Dto.Observation observation = table.Rows.Select(
                row => new SignalR.Common.Dto.Observation
                {
                    Entity = new SignalR.Common.Dto.Identity { Value = row.Read<string>("Entity") },
                    Observable = new SignalR.Common.Dto.Identity { Value = row.Read<string>("Observable") },
                    Date = row.ReadDate("Date"),
                    Measurement = new SignalR.Common.Dto.Measurement
                    {
                        Type = row.ReadEnum<SignalR.Common.Dto.MeasurementType>("MeasurementType"),
                        Value = row.Read<string>("Measurement")
                    }
                }
            ).SingleOrDefault();

            _serviceContext.PublishObservation(observation);
        }

        [Then(@"the following observation should have been received by the client for the identity identified as '(.*)' within '(.*)' seconds")]
        public void ThenTheFollowingObservationShouldHaveBeenReceivedByTheClientForTheIdentityIdentifiedAs(string observerId, int timeout, Table table)
        {
            SignalR.Common.Dto.Observation expected = table.Rows.Select(
                row => new SignalR.Common.Dto.Observation
                {
                    Entity = new SignalR.Common.Dto.Identity { Value = row.Read<string>("Entity") },
                    Observable = new SignalR.Common.Dto.Identity { Value = row.Read<string>("Observable") },
                    Date = row.ReadDate("Date"),
                    Measurement = new SignalR.Common.Dto.Measurement
                    {
                        Type = row.ReadEnum<SignalR.Common.Dto.MeasurementType>("MeasurementType"),
                        Value = row.Read<string>("Measurement")
                    }
                }
            ).SingleOrDefault();

            Assert.IsTrue(
                _messages.Where(tuple => string.Equals(tuple.Item1, observerId, StringComparison.CurrentCultureIgnoreCase))
                         .Select(tuple => tuple.Item2)
                         .OfType<With.Message.IObservation>()
                         .Any(actual => string.Equals(actual.Entity.Value, expected.Entity.Value, StringComparison.CurrentCultureIgnoreCase) &&
                                        string.Equals(actual.Observable.Value, expected.Observable.Value, StringComparison.CurrentCultureIgnoreCase) &&
                                        actual.Date == expected.Date && actual.Measurement.Value == expected.Measurement.Value)
                         .Timeout(TimeSpan.FromSeconds(timeout))
                         .FirstAsync().Wait()
            );
        }


        [Then(@"the entity identified as '(.*)' should have been registered with Harmonize")]
        public void ThenTheEntityShouldHaveBeenRegisteredWithHarmonize(string entityId)
        {
            A.CallTo(() => _serviceContext.HarmonizeClientEndpoint.Register(A<IIdentity>.Ignored, A<IEntity>.That.Matches(entity => string.Equals(entity.Identity.Value, entityId, StringComparison.CurrentCultureIgnoreCase)), A<IObserver<IMessage>>.Ignored)).MustHaveHappened();
        }

        [Then(@"the entity identified as '(.*)' should have been deregistered with Harmonize")]
        public void ThenTheEntityIdentifiedAsShouldHaveBeenDeregisteredWithHarmonize(string entityId)
        {
            A.CallTo(() => _serviceContext.HarmonizeClientEndpoint.Deregister(A<IIdentity>.Ignored, A<IEntity>.That.Matches(entity => string.Equals(entity.Identity.Value, entityId, StringComparison.CurrentCultureIgnoreCase)))).MustHaveHappened();
        }

        [Then(@"an observation of the observable identified as '(.*)' on the entity identified as '(.*)' should have been registered with Harmonize for the entity identified as '(.*)'")]
        public void ThenAnObservationOfTheObservableIdentifiedAsOnTheEntityIdentifiedAsShouldHaveBeenRegisteredWithHarmonizeForTheEntityIdentifiedAs(string observableId, string observeeId, string observerId)
        {
            A.CallTo(() => _serviceContext.HarmonizeClientEndpoint.Observe(
                A<IIdentity>.That.Matches(id => string.Equals(id.Value, observerId, StringComparison.CurrentCultureIgnoreCase)),
                A<IIdentity>.That.Matches(id => string.Equals(id.Value, observeeId, StringComparison.CurrentCultureIgnoreCase)),
                A<IIdentity>.That.Matches(id => string.Equals(id.Value, observableId, StringComparison.CurrentCultureIgnoreCase))
            )).MustHaveHappened();
        }

        [Then(@"a binding from '(.*)' to '(.*)' should have been made with the topic '(.*)'")]
        public void ThenABindingFromToShouldHaveBeenMadeWithTheTopic(string p0, string p1, string p2)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
