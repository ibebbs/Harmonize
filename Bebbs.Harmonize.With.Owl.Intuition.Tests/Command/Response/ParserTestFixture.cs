using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace Bebbs.Harmonize.With.Owl.Intuition.Tests.Command.Response
{
    [TestClass]
    public class ParserTestFixture
    {
        private static readonly Intuition.Command.Response.Builder.DeviceResponse DeviceResponseBuilder = new Intuition.Command.Response.Builder.DeviceResponse();
        private static readonly Intuition.Command.Response.Builder.RostaResponse RostaResponseBuilder = new Intuition.Command.Response.Builder.RostaResponse();
        private static readonly Intuition.Command.Response.Builder.VersionResponse VersionResponseBuilder = new Intuition.Command.Response.Builder.VersionResponse();
        private static readonly Intuition.Command.Response.Builder.UdpResponse UdpResponseBuilder = new Intuition.Command.Response.Builder.UdpResponse();
        private static readonly Intuition.Command.Response.Builder.SaveResponse SaveResponseBuilder = new Intuition.Command.Response.Builder.SaveResponse();

        private static readonly IEnumerable<Intuition.Command.Response.IBuilder> Builders = new Intuition.Command.Response.IBuilder[]
        {
            DeviceResponseBuilder,
            RostaResponseBuilder,
            VersionResponseBuilder,
            UdpResponseBuilder,
            SaveResponseBuilder
        };

        [TestMethod]
        public void ShouldParseVersionResponse()
        {
            Intuition.Command.Response.Parser subject = new Intuition.Command.Response.Parser(Builders);

            IEnumerable<Intuition.Command.IResponse> responses = subject.GetResponses(Resources.VersionResponse);
            Intuition.Command.Response.Version response = responses.OfType<Intuition.Command.Response.Version>().FirstOrDefault();

            Assert.IsNotNull(response);
            Assert.AreEqual(Intuition.Command.Status.Ok, response.Status);
            Assert.AreEqual("3", response.Firmware);
            Assert.AreEqual("1", response.Revision);
            Assert.AreEqual("4", response.Build);
        }

        [TestMethod]
        public void ShouldParseRostaResponse()
        {
            Intuition.Command.Response.Parser subject = new Intuition.Command.Response.Parser(Builders);

            IEnumerable<Intuition.Command.IResponse> responses = subject.GetResponses(Resources.RostaResponse);
            Intuition.Command.Response.Rosta response = responses.OfType<Intuition.Command.Response.Rosta>().FirstOrDefault();

            Assert.IsNotNull(response);
            Assert.AreEqual(Intuition.Command.Status.Ok, response.Status);
            Assert.IsNotNull(response.Devices);            
            Assert.AreEqual("0=CMR180|1=ROOM_STAT", string.Join("|", response.Devices.Select(tuple => string.Format("{0}={1}", tuple.Item1, tuple.Item2)).ToArray()));
        }

        [TestMethod]
        public void ShouldParseDeviceResponse()
        {
            Intuition.Command.Response.Parser subject = new Intuition.Command.Response.Parser(Builders);

            IEnumerable<Intuition.Command.IResponse> responses = subject.GetResponses(Resources.DeviceResponse);
            Intuition.Command.Response.Device response = responses.OfType<Intuition.Command.Response.Device>().FirstOrDefault();

            Assert.IsNotNull(response);
            Assert.AreEqual(Intuition.Command.Status.Ok, response.Status);
            Assert.AreEqual(0, response.Index);
            Assert.AreEqual("F2", response.DeviceAddress);
            Assert.AreEqual("CMR180", response.DeviceType);
            Assert.AreEqual("0", response.DeviceState);
            Assert.AreEqual(Values.SignalStrength.Parse("-53"), response.SignalStrength);
            Assert.AreEqual(Values.LinkQuality.Parse("20"), response.LinkQuality);
            Assert.AreEqual(Values.BatteryState.Parse("1"), response.BatteryState);
            Assert.AreEqual(TimeSpan.FromSeconds(1), response.TimeSinceLastPacketReceived);
            Assert.AreEqual(178, response.ReceivedPackets);
            Assert.AreEqual(0, response.SentPackets);
        }

        [TestMethod]
        public void ShouldParseUdpResponse()
        {
            Intuition.Command.Response.Parser subject = new Intuition.Command.Response.Parser(Builders);

            IEnumerable<Intuition.Command.IResponse> responses = subject.GetResponses(Resources.UdpResponse);
            Intuition.Command.Response.Udp response = responses.OfType<Intuition.Command.Response.Udp>().FirstOrDefault();

            Assert.IsNotNull(response);
            Assert.AreEqual(Intuition.Command.Status.Ok, response.Status);
            Assert.AreEqual(string.Empty, response.HostName);
            Assert.AreEqual(new IPEndPoint(IPAddress.Parse("192.168.1.27"), 13), response.Endpoint);
        }

        [TestMethod]
        public void ShouldParseSaveResponse()
        {
            Intuition.Command.Response.Parser subject = new Intuition.Command.Response.Parser(Builders);

            IEnumerable<Intuition.Command.IResponse> responses = subject.GetResponses(Resources.SaveResponse);
            Intuition.Command.Response.Save response = responses.OfType<Intuition.Command.Response.Save>().FirstOrDefault();

            Assert.IsNotNull(response);
            Assert.AreEqual(Intuition.Command.Status.Ok, response.Status);
        }
    }
}
