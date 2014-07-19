using Bebbs.Harmonize.With.Messaging.Via.SignalR.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Tests.Common
{
    [TestClass]
    public class MappingTestFixture
    {
        [TestMethod]
        public void ShouldBeAbleToMapIdentityToComponent()
        {
            SignalR.Common.Dto.Identity source = new SignalR.Common.Dto.Identity { Value = "TEST" };
            With.Component.IIdentity actual = source.AsComponent();

            Assert.AreEqual(actual.Value, "TEST");
        }

        [TestMethod]
        public void ShouldBeAbleToMapIdentityToDto()
        {
            With.Component.IIdentity source = new With.Component.Identity("TEST");
            SignalR.Common.Dto.Identity actual = source.AsDto();

            Assert.AreEqual(actual.Value, "TEST");
        }

        [TestMethod]
        public void ShouldBeAbleToMapEntityToComponent()
        {
            SignalR.Common.Dto.Entity source = new SignalR.Common.Dto.Entity { Identity = new SignalR.Common.Dto.Identity { Value = "TEST" } };
            With.Component.IEntity actual = source.AsComponent();

            Assert.AreEqual(actual.Identity.Value, "TEST");
        }

        [TestMethod]
        public void ShouldBeAbleToMapEntityToDto()
        {
            With.Component.IEntity source = new With.Component.Entity(new With.Component.Identity("TEST"), null, null, null);
            SignalR.Common.Dto.Entity actual = source.AsDto();

            Assert.AreEqual(actual.Identity.Value, "TEST");
        }

        [TestMethod]
        public void ShouldBeAbleToMapObservationToMessage()
        {
            SignalR.Common.Dto.Observation source = new SignalR.Common.Dto.Observation
            {
                Entity = new SignalR.Common.Dto.Identity { Value = "TESTENTITY" },
                Observable = new SignalR.Common.Dto.Identity { Value = "TESTOBSERVABLE" },
                Date = DateTimeOffset.Now,
                Measurement = new SignalR.Common.Dto.Measurement
                {
                    Type = SignalR.Common.Dto.MeasurementType.Units,
                    Value = "3.14"
                }
            };
            With.Message.IObservation actual = source.AsMessage();

            Assert.AreEqual(actual.Entity.Value, source.Entity.Value);
            Assert.AreEqual(actual.Observable.Value, source.Observable.Value);
            Assert.AreEqual(actual.Date, source.Date);
            Assert.AreEqual(actual.Measurement.Value, source.Measurement.Value);
            Assert.AreEqual(actual.Measurement.Type.ToString(), source.Measurement.Type.ToString());
        }

        [TestMethod]
        public void ShouldBeAbleToMapObservationToDto()
        {
            With.Message.Observation source = new Message.Observation(
                new With.Component.Identity("TESTENTITY"),
                new With.Component.Identity("TESTOBSERVABLE"),
                DateTimeOffset.Now,
                new With.Component.Measurement(
                    With.Component.MeasurementType.Units,
                    "3.14"
                )
            );

            SignalR.Common.Dto.Observation actual = source.AsDto();

            Assert.AreEqual(actual.Entity.Value, source.Entity.Value);
            Assert.AreEqual(actual.Observable.Value, source.Observable.Value);
            Assert.AreEqual(actual.Date, source.Date);
            Assert.AreEqual(actual.Measurement.Value, source.Measurement.Value);
            Assert.AreEqual(actual.Measurement.Type.ToString(), source.Measurement.Type.ToString());
        }


        [TestMethod]
        public void ShouldBeAbleToDynamicallyMapObservationToMessage()
        {
            SignalR.Common.Dto.Message source = new SignalR.Common.Dto.Observation
            {
                Entity = new SignalR.Common.Dto.Identity { Value = "TESTENTITY" },
                Observable = new SignalR.Common.Dto.Identity { Value = "TESTOBSERVABLE" },
                Date = DateTimeOffset.Now,
                Measurement = new SignalR.Common.Dto.Measurement
                {
                    Type = SignalR.Common.Dto.MeasurementType.Units,
                    Value = "3.14"
                }
            };
            
            With.Message.IMessage actual = source.AsDynamicMessage();

            Assert.IsInstanceOfType(actual, typeof(With.Message.IObservation));
        }

        [TestMethod]
        public void ShouldBeAbleToDynamicallyMapObservationToDto()
        {
            With.Message.IMessage source = new Message.Observation(
                new With.Component.Identity("TESTENTITY"),
                new With.Component.Identity("TESTOBSERVABLE"),
                DateTimeOffset.Now,
                new With.Component.Measurement(
                    With.Component.MeasurementType.Units,
                    "3.14"
                )
            );

            SignalR.Common.Dto.Message actual = source.AsDynamicDto();

            Assert.IsInstanceOfType(actual, typeof(SignalR.Common.Dto.Observation));
        }
    }
}
