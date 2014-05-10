using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Message.As.Hml.Tests.Schema
{
    [TestClass]
    public class MapperTests
    {
        private Hml.Schema.Mapper _subject;

        private static readonly Hml.Schema.Observable TestObservable = new Hml.Schema.Observable
        {
            Description = new Hml.Schema.ValueDescription
            {
                Name = "Observable.Name",
                Remarks = "Observable.Remarks",
                Measurement = Hml.Schema.MeasurementType.Meter,
                Default = new Hml.Schema.Measurement
                {
                    Type = Hml.Schema.MeasurementType.Meter,
                    Value = "2.5"
                },
                Maximum = new Hml.Schema.Measurement
                {
                    Type = Hml.Schema.MeasurementType.Meter,
                    Value = "5"
                },
                Minimum = new Hml.Schema.Measurement
                {
                    Type = Hml.Schema.MeasurementType.Meter,
                    Value = "0"
                }
            }
        };

        private static readonly Hml.Schema.Actionable TestActionable = new Hml.Schema.Actionable
        {
            Identity = new Hml.Schema.Identity { Value = "Actionable.Identifier" },
            Description = new Hml.Schema.Description
            {
                Name = "Actionable.Name",
                Remarks = "Actionable.Remarks"
            },
            Parameters = new Hml.Schema.Parameter[]
            {
                new Hml.Schema.Parameter
                {
                    Description = new Hml.Schema.ParameterDescription
                    {
                        Name = "Parameter.Name",
                        Remarks = "Parameter.Remarks",
                        Required = true,
                        Measurement = Hml.Schema.MeasurementType.Percent,
                        Default = new Hml.Schema.Measurement
                        {
                            Type = Hml.Schema.MeasurementType.Percent,
                            Value = "0.0"
                        },
                        Minimum = new Hml.Schema.Measurement
                        {
                            Type = Hml.Schema.MeasurementType.Percent,
                            Value = "0.0"
                        },
                        Maximum = new Hml.Schema.Measurement
                        {
                            Type = Hml.Schema.MeasurementType.Percent,
                            Value = "100.0"
                        }
                    }
                }
            }
        };

        private static readonly Hml.Schema.Entity EmptytEntity = new Hml.Schema.Entity
        {
            Identity = new Hml.Schema.Identity { Value = "EntityIdentifier" }
        };

        private static readonly Hml.Schema.Entity FullPopulatedEntity = new Hml.Schema.Entity
        {
            Identity = new Hml.Schema.Identity { Value = "EntityIdentifier" },
            Description = new Hml.Schema.EntityDescription
            {
                Name = "Description.Name",
                Model = "Description.Model",
                Manufacturer = "Description.Manufacturer",
                Remarks = "Description.Remarks"
            },
            Observables = new Hml.Schema.Observable[] { TestObservable },
            Actionables = new Hml.Schema.Actionable[] { TestActionable }
        };

        [TestInitialize]
        public void Setup()
        {
            _subject = new Hml.Schema.Mapper();
        }

        [TestMethod]
        public void MappingIsValid()
        {
            Hml.Schema.Mapper.ValidateMapping();
        }

        [TestMethod]
        public void CanMapUniqueIdentifierFromSchemaToComponent()
        {
            Hml.Schema.Identity identifier = new Hml.Schema.Identity { Value = "Identifier" };

            Component.IIdentity identity = _subject.ToComponent(identifier);

            Assert.IsNotNull(identity);
        }

        [TestMethod]
        public void CanMapRegisterOfEmptyEntityFromSchemaToMessage()
        {
            Hml.Schema.Register schema = new Hml.Schema.Register
            {
                Registrar = new Hml.Schema.Identity { Value = "RegistrarIdentity" },
                Entity = EmptytEntity
            };

            Message.IMessage message = _subject.ToMessage(schema);

            Assert.IsInstanceOfType(message, typeof(Message.IRegister));
            Assert.AreEqual<string>("RegistrarIdentity", ((Message.IRegister)message).Registrar.Value);
        }

        [TestMethod]
        public void CanMapRegisterOfFullyPopulatedEntityFromSchemaToMessage()
        {
            Hml.Schema.Register schema = new Hml.Schema.Register
            {
                Registrar = new Hml.Schema.Identity { Value = "Registrar" },
                Entity = FullPopulatedEntity
            };

            Message.IMessage message = _subject.ToMessage(schema);

            Assert.IsInstanceOfType(message, typeof(Message.IRegister));
            Assert.AreEqual<string>("Registrar", ((Message.IRegister)message).Registrar.Value);
            Assert.IsNotNull(((Message.IRegister)message).Entity);
        }

        [TestMethod]
        public void CanMapDegisterFromSchemaToMessage()
        {
            Hml.Schema.Deregister schema = new Hml.Schema.Deregister
            {
                Registrar = new Hml.Schema.Identity { Value = "Registrar" },
                Entity = new Hml.Schema.Identity { Value = "DeregisteredEntity" }
            };

            Message.IMessage message = _subject.ToMessage(schema);

            Assert.IsInstanceOfType(message, typeof(Message.IDeregister));
            Assert.AreEqual<string>("Registrar", ((Message.IDeregister)message).Registrar.Value);
            Assert.AreEqual<string>("DeregisteredEntity", ((Message.IDeregister)message).Entity.Value);
        }

        [TestMethod]
        public void CanMapObserveFromSchemaToMessage()
        {
            Hml.Schema.Observe schema = new Hml.Schema.Observe
            {
                Entity = new Hml.Schema.Identity { Value = "ObserveEntity" },
                Observable = new Hml.Schema.Identity { Value = "ObservableValue" },
                Observer = new Hml.Schema.Identity { Value = "ObservableObserver" }
            };

            Message.IMessage message = _subject.ToMessage(schema);

            Assert.IsInstanceOfType(message, typeof(Message.IObserve));
            Assert.AreEqual<string>("ObserveEntity", ((Message.IObserve)message).Entity.Value);
            Assert.AreEqual<string>("ObservableValue", ((Message.IObserve)message).Observable.Value);
            Assert.AreEqual<string>("ObservableObserver", ((Message.IObserve)message).Observer.Value);
        }

        [TestMethod]
        public void CanMapActFromSchemaToMessage()
        {
            Hml.Schema.Action schema = new Hml.Schema.Action
            {
                Entity = new Hml.Schema.Identity { Value = "ObserveEntity" },
                Actionable = new Hml.Schema.Identity { Value = "ActionableValue" },
                Actor = new Hml.Schema.Identity { Value = "ActionableActor" },
                ParameterValues = new[]
                {
                    new Hml.Schema.ParameterValue
                    {
                        Measurement = new Hml.Schema.Measurement
                        {
                            Type = Hml.Schema.MeasurementType.Kilogram,
                            Value = "3.0"
                        }
                    }
                }
            };

            Message.IMessage message = _subject.ToMessage(schema);

            Assert.IsInstanceOfType(message, typeof(Message.IAction));
            Assert.AreEqual<string>("ObserveEntity", ((Message.IAction)message).Entity.Value);
            Assert.AreEqual<string>("ActionableValue", ((Message.IAction)message).Actionable.Value);
            Assert.AreEqual<string>("ActionableActor", ((Message.IAction)message).Actor.Value);
        }

        [TestMethod]
        public void CanMapRegisterFromMessageToSchema()
        {
            Message.IRegister message = A.Fake<Message.IRegister>();
            A.CallTo(() => message.Registrar).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Entity).Returns(A.Fake<Component.IEntity>());

            Hml.Schema.Message schema = _subject.ToSchema(message);

            Assert.IsNotNull(schema);
            Assert.IsInstanceOfType(schema, typeof(Hml.Schema.Register));
        }

        [TestMethod]
        public void CanMapDeregisterFromMessageToSchema()
        {
            Message.IDeregister message = A.Fake<Message.IDeregister>();
            A.CallTo(() => message.Registrar).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Entity).Returns(A.Fake<Component.IIdentity>());

            Hml.Schema.Message schema = _subject.ToSchema(message);

            Assert.IsNotNull(schema);
            Assert.IsInstanceOfType(schema, typeof(Hml.Schema.Deregister));
        }

        [TestMethod]
        public void CanMapObservationFromMessageToSchema()
        {
            Message.IObservation message = A.Fake<Message.IObservation>();
            A.CallTo(() => message.Entity).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Observable).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Date).Returns(DateTimeOffset.Now);
            A.CallTo(() => message.Measurement).Returns(A.Fake<Component.IMeasurement>());

            Hml.Schema.Message schema = _subject.ToSchema(message);

            Assert.IsNotNull(schema);
            Assert.IsInstanceOfType(schema, typeof(Hml.Schema.Observation));
        }

        [TestMethod]
        public void CanMapActFromMessageToSchema()
        {
            Message.IAction message = A.Fake<Message.IAction>();
            A.CallTo(() => message.Entity).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Actionable).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Actor).Returns(A.Fake<Component.IIdentity>());

            Hml.Schema.Message schema = _subject.ToSchema(message);

            Assert.IsNotNull(schema);
            Assert.IsInstanceOfType(schema, typeof(Hml.Schema.Action));
        }
    }
}
