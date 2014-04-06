using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.State.Tests.Event
{
    [TestClass]
    public class MapperTestFixture
    {
        private static With.Component.IIdentity Identity;
        private static With.Component.IDescription Description;
        private static With.Component.IEntityDescription EntityDescription;
        private static With.Component.IObservable Observable;
        private static With.Component.IMeasurement Measurement;
        private static With.Component.IValueDescription ValueDescription;
        private static With.Component.IParameter Parameter;
        private static With.Component.IParameterDescription ParameterDescription;
        private static With.Component.IActionable Actionable;
        private static With.Component.IEntity Entity;

        [ClassInitialize]
        public static void TestInitialize(TestContext context)
        {
            Identity = A.Fake<With.Component.IIdentity>();
            A.CallTo(() => Identity.ToString()).Returns("Identity");

            Description = A.Fake<With.Component.IDescription>();
            A.CallTo(() => Description.Name).Returns("Name");
            A.CallTo(() => Description.Remarks).Returns("Remarks");

            EntityDescription = A.Fake<With.Component.IEntityDescription>();
            A.CallTo(() => EntityDescription.Type).Returns("Type");
            A.CallTo(() => EntityDescription.Manufacturer).Returns("Manufacturer");
            A.CallTo(() => EntityDescription.Model).Returns("Model");
            A.CallTo(() => EntityDescription.Name).Returns("Name");
            A.CallTo(() => EntityDescription.Remarks).Returns("Remarks");

            Measurement = A.Fake<With.Component.IMeasurement>();
            A.CallTo(() => Measurement.Type).Returns(With.Component.MeasurementType.Seconds);
            A.CallTo(() => Measurement.Value).Returns("3.14");
            
            ValueDescription = A.Fake<With.Component.IValueDescription>();
            A.CallTo(() => ValueDescription.Name).Returns("Name");
            A.CallTo(() => ValueDescription.Remarks).Returns("Remarks");
            A.CallTo(() => ValueDescription.Measurement).Returns(With.Component.MeasurementType.Percent);
            A.CallTo(() => ValueDescription.Minimum).Returns(Measurement);
            A.CallTo(() => ValueDescription.Maximum).Returns(Measurement);
            A.CallTo(() => ValueDescription.Default).Returns(Measurement);

            Observable = A.Fake<With.Component.IObservable>();
            A.CallTo(() => Observable.Identity).Returns(Identity);
            A.CallTo(() => Observable.Description).Returns(ValueDescription);

            ParameterDescription = A.Fake<With.Component.IParameterDescription>();
            A.CallTo(() => ParameterDescription.Name).Returns("Name");
            A.CallTo(() => ParameterDescription.Remarks).Returns("Remarks");
            A.CallTo(() => ParameterDescription.Measurement).Returns(With.Component.MeasurementType.Percent);
            A.CallTo(() => ParameterDescription.Minimum).Returns(Measurement);
            A.CallTo(() => ParameterDescription.Maximum).Returns(Measurement);
            A.CallTo(() => ParameterDescription.Default).Returns(Measurement);
            A.CallTo(() => ParameterDescription.Required).Returns(true);

            Parameter = A.Fake<With.Component.IParameter>();
            A.CallTo(() => Parameter.Identity).Returns(Identity);
            A.CallTo(() => Parameter.Description).Returns(ParameterDescription);

            Actionable = A.Fake<With.Component.IActionable>();
            A.CallTo(() => Actionable.Identity).Returns(Identity);
            A.CallTo(() => Actionable.Description).Returns(Description);
            A.CallTo(() => Actionable.Parameters).Returns(new[] { Parameter });

            Entity = A.Fake<With.Component.IEntity>();
            A.CallTo(() => Entity.Identity).Returns(Identity);
            A.CallTo(() => Entity.Description).Returns(EntityDescription);
            A.CallTo(() => Entity.Observables).Returns(new[] { Observable });
            A.CallTo(() => Entity.Actionables).Returns(new[] { Actionable });
        }

        [TestMethod]
        public void ShouldBeAbleToMapIdentity()
        {
            State.Event.Mapper subject = new State.Event.Mapper();

            var identity = subject.Map<With.Component.IIdentity, State.Component.Identity>(Identity);

            Assert.IsNotNull(identity);
            Assert.AreEqual(Identity.ToString(), identity.Value);
        }

        [TestMethod]
        public void ShouldBeAbleToMapMeasurement()
        {
            State.Event.Mapper subject = new State.Event.Mapper();

            var measurement = subject.Map<With.Component.IMeasurement, State.Component.Measurement>(Measurement);

            Assert.IsNotNull(measurement);
            Assert.AreEqual(Measurement.Type, measurement.Type);
            Assert.AreEqual(Measurement.Value, measurement.Value);
        }

        [TestMethod]
        public void ShouldBeAbleToMapValueDescription()
        {
            State.Event.Mapper subject = new State.Event.Mapper();

            var description = subject.Map<With.Component.IValueDescription, State.Component.ValueDescription>(ValueDescription);

            Assert.IsNotNull(description);
            Assert.AreEqual(ValueDescription.Name, description.Name);
            Assert.AreEqual(ValueDescription.Remarks, description.Remarks);
            Assert.IsNotNull(description.Measurement);
            Assert.IsNotNull(description.Default);
            Assert.IsNotNull(description.Minimum);
            Assert.IsNotNull(description.Maximum);
        }

        [TestMethod]
        public void ShouldBeAbleToMapObservable()
        {
            State.Event.Mapper subject = new State.Event.Mapper();

            var observable = subject.Map<With.Component.IObservable, State.Component.Observable>(Observable);

            Assert.IsNotNull(observable);
            Assert.IsNotNull(observable.Identity);
            Assert.IsNotNull(observable.Description);
        }

        [TestMethod]
        public void ShouldBeAbleToMapParameterDescription()
        {
            State.Event.Mapper subject = new State.Event.Mapper();

            var description = subject.Map<With.Component.IParameterDescription, State.Component.ParameterDescription>(ParameterDescription);

            Assert.IsNotNull(description);
            Assert.AreEqual(ParameterDescription.Name, description.Name);
            Assert.AreEqual(ParameterDescription.Remarks, description.Remarks);
            Assert.AreEqual(ParameterDescription.Required, description.Required);
            Assert.IsNotNull(description.Measurement);
            Assert.IsNotNull(description.Default);
            Assert.IsNotNull(description.Minimum);
            Assert.IsNotNull(description.Maximum);
        }

        [TestMethod]
        public void ShouldBeAbleToMapParameter()
        {
            State.Event.Mapper subject = new State.Event.Mapper();

            var parameter = subject.Map<With.Component.IParameter, State.Component.Parameter>(Parameter);

            Assert.IsNotNull(parameter);
            Assert.IsNotNull(parameter.Identity);
            Assert.IsNotNull(parameter.Description);
        }

        [TestMethod]
        public void ShouldBeAbleToMapActionable()
        {
            State.Event.Mapper subject = new State.Event.Mapper();

            var actionable = subject.Map<With.Component.IActionable, State.Component.Actionable>(Actionable);

            Assert.IsNotNull(actionable);
            Assert.IsNotNull(actionable.Identity);
            Assert.IsNotNull(actionable.Description);
            Assert.IsNotNull(actionable.Parameters);
            Assert.AreEqual(1, actionable.Parameters.Count());
        } 

        [TestMethod]
        public void ShouldBeAbleToMapEntityDescription()
        {
            State.Event.Mapper subject = new State.Event.Mapper();

            var entityDescription = subject.Map<With.Component.IEntityDescription, State.Component.EntityDescription>(EntityDescription);

            Assert.IsNotNull(entityDescription);
            Assert.AreEqual(EntityDescription.Name, entityDescription.Name);
            Assert.AreEqual(EntityDescription.Remarks, entityDescription.Remarks);
            Assert.AreEqual(EntityDescription.Type, entityDescription.Type);
            Assert.AreEqual(EntityDescription.Manufacturer, entityDescription.Manufacturer);
            Assert.AreEqual(EntityDescription.Model, entityDescription.Model);
        }

        [TestMethod]
        public void ShouldBeAbleToMapEntity()
        {
            State.Event.Mapper subject = new State.Event.Mapper();

            var entity = subject.Map<With.Component.IEntity, State.Component.Entity>(Entity);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.Identity);
            Assert.IsNotNull(entity.Description);
            Assert.IsNotNull(entity.Observables);
            Assert.AreEqual(1, entity.Observables.Count());
            Assert.AreEqual(1, entity.Actionables.Count());
        }

        [TestMethod]
        public void ShouldBeAbleToMapRegistrationMessage()
        {
            With.Message.IRegister registration = A.Fake<With.Message.IRegister>();
            A.CallTo(() => registration.Registrar).Returns(Identity);
            A.CallTo(() => registration.Entity).Returns(Entity);

            State.Event.Mapper subject = new State.Event.Mapper();

            State.Event.Registered actual = subject.Map(registration);

            Assert.AreEqual(registration.Registrar.ToString(), actual.Registrar.Value);
            Assert.IsNotNull(registration.Entity);
        }

        [TestMethod]
        public void ShouldBeAbleToMapDeregistrationMessage()
        {
            With.Message.IDeregister deregistration = A.Fake<With.Message.IDeregister>();
            A.CallTo(() => deregistration.Registrar).Returns(Identity);
            A.CallTo(() => deregistration.Entity).Returns(Identity);

            State.Event.Mapper subject = new State.Event.Mapper();

            State.Event.Deregistered actual = subject.Map(deregistration);

            Assert.AreEqual(deregistration.Registrar.ToString(), actual.Registrar.Value);
            Assert.AreEqual(deregistration.Entity.ToString(), actual.Entity.Value);
        }

        [TestMethod]
        public void ShouldBeAbleToMapObservationMessage()
        {
            DateTimeOffset date = new DateTimeOffset(2014, 04, 06, 10, 20, 00, TimeSpan.Zero);

            With.Message.IObservation observation = A.Fake<With.Message.IObservation>();
            A.CallTo(() => observation.Entity).Returns(Identity);
            A.CallTo(() => observation.Observable).Returns(Identity);
            A.CallTo(() => observation.Date).Returns(date);
            A.CallTo(() => observation.Measurement).Returns(Measurement);

            State.Event.Mapper subject = new State.Event.Mapper();

            State.Event.Observed actual = subject.Map(observation);

            Assert.AreEqual(observation.Entity.ToString(), actual.Entity.Value);
            Assert.AreEqual(observation.Observable.ToString(), actual.Observable.Value);
            Assert.AreEqual(observation.Date, actual.Date);
            Assert.IsNotNull(actual.Measurement);
        }
    }
}
