using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using FakeItEasy;
using System.Linq;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Tests
{
    [TestClass]
    public class ConnectorTestFixture
    {
        private Messaging.Client.IEndpoint _clientEndpoint;
        private Intuition.Configuration.ISettings _configurationSettings;
        private Intuition.Gateway.IFactory _deviceFactory;
        private Connector _subject;

        [TestInitialize]
        public void Initialize()
        {
            _configurationSettings = A.Fake<Intuition.Configuration.ISettings>();
            _deviceFactory = A.Fake<Intuition.Gateway.IFactory>();
            _clientEndpoint = A.Fake<Messaging.Client.IEndpoint>();

            _subject = new Connector(_clientEndpoint, _configurationSettings, _deviceFactory);
        }

        [TestMethod]
        public void ShouldUseDeviceFactoryToConstructDevicesWithCorrectSettings()
        {
            Configuration.Device deviceA = new Configuration.Device
            {
                LocalCommandPort = 5100,
                LocalPacketPort = 5110,
                OwlCommandKey = "6C9DB92A",
                OwlIpAddress = "192.168.1.113",
                OwlCommandPort = 5100,
                OwlCommandResponseTimeout = "00:00:10",
                AutoConfigurePacketPort = true
            };

            Configuration.Device deviceB = new Configuration.Device
            {
                LocalCommandPort = 6100,
                LocalPacketPort = 6110,
                OwlCommandKey = "6C9DB92A",
                OwlIpAddress = "192.168.1.113",
                OwlCommandPort = 6100,
                OwlCommandResponseTimeout = "00:00:10",
                AutoConfigurePacketPort = true
            };

            A.CallTo(() => _configurationSettings.Devices).Returns(new [] { deviceA, deviceB });

            _subject.Initialize();

            A.CallTo(() => _deviceFactory.CreateDeviceInContext(deviceA, _clientEndpoint)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _deviceFactory.CreateDeviceInContext(deviceB, _clientEndpoint)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
