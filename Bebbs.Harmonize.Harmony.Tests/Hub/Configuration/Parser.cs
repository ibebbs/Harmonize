using System.Linq;
using Bebbs.Harmonize.With.Harmony.Hub.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bebbs.Harmonize.With.Harmony.Tests.Hub.Configuration
{
    [TestClass]
    public class Parser
    {
        [TestMethod]
        public void ShouldCorrectlyDeserializeJsonConfigurationValues()
        {
            IValues values = With.Harmony.Hub.Configuration.Parser.FromJson("Test", Resources.SimpleConfiguration);

            Assert.IsNotNull(values);
            Assert.AreEqual<int>(1, values.Activities.Count(), "Did not correctly deserialize Activities");
            Assert.AreEqual<int>(1, values.Devices.Count(), "Did not correctly deserialize Devices");

            var device = values.Devices.First();

            Assert.AreEqual<string>("Test", device.HubName, "Did not correctly deserialize device hub name");
            Assert.AreEqual<int>(6, device.Controls.Count(), "Did not correctly deserialize Controls");
        }
    }
}
