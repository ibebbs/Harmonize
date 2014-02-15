using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;

namespace Bebbs.Harmonize.With.Messaging.Tests.Mapping
{
    [TestClass]
    public class HelperTestFixture
    {
        private static readonly Schema.Observable TestObservable = new Schema.Observable
        {
            Description = new Schema.ValueDescription
            {
                Name = "Observable.Name",
                Remarks = "Observable.Remarks",
                Measurement = Schema.MeasurementType.meter,
                Default = new Schema.Measurement
                {
                    Type = Schema.MeasurementType.meter,
                    Value = "2.5"
                },
                Maximum = new Schema.Measurement
                {
                    Type = Schema.MeasurementType.meter,
                    Value = "5"
                },
                Minimum = new Schema.Measurement
                {
                    Type = Schema.MeasurementType.meter,
                    Value = "0"
                }
            }
        };

        private static readonly Schema.Actionable TestActionable = new Schema.Actionable
        {
            UniqueIdentifier = new Schema.UniqueIdentifier { Value = "Actionable.Identifier" },
            Description = new Schema.SimpleDescription
            {
                Name = "Actionable.Name",
                Remarks = "Actionable.Remarks"
            },
            Parameters = new Schema.Parameter[]
            {
                new Schema.Parameter
                {
                    Description = new Schema.ParameterDescription
                    {
                        Name = "Parameter.Name",
                        Remarks = "Parameter.Remarks",
                        Required = true,
                        Measurement = Schema.MeasurementType.percent,
                        Default = new Schema.Measurement
                        {
                            Type = Schema.MeasurementType.percent,
                            Value = "0.0"
                        },
                        Minimum = new Schema.Measurement
                        {
                            Type = Schema.MeasurementType.percent,
                            Value = "0.0"
                        },
                        Maximum = new Schema.Measurement
                        {
                            Type = Schema.MeasurementType.percent,
                            Value = "100.0"
                        }
                    }
                }
            }
        };

        private static readonly Schema.Entity EmptytEntity = new Schema.Entity
        {
            UniqueIdentifier = new Schema.UniqueIdentifier { Value = "EntityIdentifier" }
        };

        private static readonly Schema.Entity FullPopulatedEntity = new Schema.Entity
        {
            UniqueIdentifier = new Schema.UniqueIdentifier { Value = "EntityIdentifier" },
            Description = new Schema.EntityDescription
            {
                Name = "Description.Name",
                Model = "Description.Model",
                Manufacturer = "Description.Manufacturer",
                Remarks = "Description.Remarks"
            },
            Observables = new Schema.Observable[] { TestObservable },
            Actionables = new[] { TestActionable }
        };

        [TestMethod]
        public void CanMapUniqueIdentifierFromSchemaToComponent()
        {
            Schema.UniqueIdentifier identifier = new Schema.UniqueIdentifier { Value = "Identifier" };

            Messaging.Mapping.Helper subject = new Messaging.Mapping.Helper();

            Component.IIdentity identity = subject.ToComponent(identifier);

            Assert.IsNotNull(identity);
        }

        [TestMethod]
        public void CanMapRegisterOfEmptyEntityFromSchemaToMessage()
        {
            Messaging.Mapping.Helper mapping = new Messaging.Mapping.Helper();

            Schema.Register schema = new Schema.Register
            {
                Registrar = new Schema.UniqueIdentifier { Value = "RegistrarIdentity" },
                Entity = EmptytEntity
            };

            Message.IRegister message = mapping.ToComponent(schema);

            Assert.AreEqual<string>("RegistrarIdentity", message.Registrar.ToString());
        }

        [TestMethod]
        public void CanMapRegisterOfFullyPopulatedEntityFromSchemaToMessage()
        {
            Messaging.Mapping.Helper mapping = new Messaging.Mapping.Helper();

            Schema.Register schema = new Schema.Register
            {
                Registrar = new Schema.UniqueIdentifier { Value = "Registrar" },
                Entity = FullPopulatedEntity
            };

            Message.IRegister message = mapping.ToComponent(schema);
        }

        [TestMethod]
        public void CanMapDegisterFromSchemaToMessage()
        {
            Messaging.Mapping.Helper mapping = new Messaging.Mapping.Helper();

            Schema.Deregister schema = new Schema.Deregister
            {
                Registrar = new Schema.UniqueIdentifier { Value = "Registrar" },
                Entity = new Schema.UniqueIdentifier {  Value = "DeregisteredEntity" }
            };

            Message.IDeregister message = mapping.ToComponent(schema);
        }

        [TestMethod]
        public void CanMapObserveFromSchemaToMessage()
        {
            Messaging.Mapping.Helper mapping = new Messaging.Mapping.Helper();

            Schema.Observe schema = new Schema.Observe
            {
                Entity = new Schema.UniqueIdentifier { Value = "ObserveEntity" },
                Observable = new Schema.UniqueIdentifier {  Value = "ObservableValue"},
                Observer = new Schema.UniqueIdentifier {  Value = "ObservableObserver" }
            };

            Message.IObserve message = mapping.ToComponent(schema);
        }

        [TestMethod]
        public void CanMapSubscribeFromSchemaToMessage()
        {
            Messaging.Mapping.Helper mapping = new Messaging.Mapping.Helper();

            Schema.Subscribe schema = new Schema.Subscribe
            {
                Entity = new Schema.UniqueIdentifier { Value = "ObserveEntity" },
                Observable = new Schema.UniqueIdentifier { Value = "ObservableValue" },
                Subscriber = new Schema.UniqueIdentifier { Value = "ObservableSubscriber" }
            };

            Message.ISubscribe message = mapping.ToComponent(schema);
        }

        [TestMethod]
        public void CanMapActFromSchemaToMessage()
        {
            Messaging.Mapping.Helper mapping = new Messaging.Mapping.Helper();

            Schema.Act schema = new Schema.Act
            {
                Entity = new Schema.UniqueIdentifier { Value = "ObserveEntity" },
                Actionable = new Schema.UniqueIdentifier { Value = "ActionableValue" },
                Actor = new Schema.UniqueIdentifier { Value = "ActionableActor" },
                ParameterValues = new []
                {
                    new Schema.ParameterValue
                    {
                        Measurement = new Schema.Measurement
                        {
                            Type = Schema.MeasurementType.kilogram,
                            Value = "3.0"
                        }
                    }
                }
            };

            Message.IAct message = mapping.ToComponent(schema);
        }

        [TestMethod]
        public void CanMapRegisterFromMessageToSchema()
        {
            Messaging.Mapping.Helper mapping = new Messaging.Mapping.Helper();

            Message.IRegister message = A.Fake<Message.IRegister>();
            A.CallTo(() => message.Registrar).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Entity).Returns(A.Fake<Component.IEntity>());

            Schema.Register schema = mapping.ToSchema(message);

            Assert.IsNotNull(schema);
        }

        [TestMethod]
        public void CanMapDeregisterFromMessageToSchema()
        {
            Messaging.Mapping.Helper mapping = new Messaging.Mapping.Helper();

            Message.IDeregister message = A.Fake<Message.IDeregister>();
            A.CallTo(() => message.Registrar).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Entity).Returns(A.Fake<Component.IIdentity>());

            Schema.Deregister schema = mapping.ToSchema(message);

            Assert.IsNotNull(schema);
        }

        [TestMethod]
        public void CanMapObservationFromMessageToSchema()
        {
            Messaging.Mapping.Helper mapping = new Messaging.Mapping.Helper();

            Message.IObservation message = A.Fake<Message.IObservation>();
            A.CallTo(() => message.Entity).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Observable).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Date).Returns(DateTimeOffset.Now);
            A.CallTo(() => message.Measurement).Returns(A.Fake<Component.IMeasurement>());

            Schema.Observation schema = mapping.ToSchema(message);

            Assert.IsNotNull(schema);
        }

        [TestMethod]
        public void CanMapActFromMessageToSchema()
        {
            Messaging.Mapping.Helper mapping = new Messaging.Mapping.Helper();

            Message.IAct message = A.Fake<Message.IAct>();
            A.CallTo(() => message.Entity).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Actionable).Returns(A.Fake<Component.IIdentity>());
            A.CallTo(() => message.Actor).Returns(A.Fake<Component.IIdentity>());

            Schema.Act schema = mapping.ToSchema(message);

            Assert.IsNotNull(schema);
        }
    }
}
