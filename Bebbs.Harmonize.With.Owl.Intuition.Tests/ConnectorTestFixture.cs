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
        private Intuition.Configuration.IProvider _configurationProvider;
        private Intuition.Gateway.IFactory _deviceFactory;
        private Connector _subject;

        [TestInitialize]
        public void Initialize()
        {
            _configurationProvider = A.Fake<Intuition.Configuration.IProvider>();
            _deviceFactory = A.Fake<Intuition.Gateway.IFactory>();

            _subject = new Connector(_configurationProvider, _deviceFactory);
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

            A.CallTo(() => _configurationProvider.GetConfiguration()).Returns(
                new Configuration.Details
                {
                    Devices = new [] { deviceA, deviceB }
                }
            );

            _subject.Initialize();

            A.CallTo(() => _deviceFactory.CreateDeviceInContext(deviceA)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _deviceFactory.CreateDeviceInContext(deviceB)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
