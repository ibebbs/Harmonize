using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.Tests.Hub.Configuration
{
    [TestClass]
    public class Parser
    {
        [TestMethod]
        public void ShouldCorrectlyDeserializeJsonConfigurationValues()
        {
            Harmony.Hub.Configuration.Parser subject = new Harmony.Hub.Configuration.Parser();

            Harmony.Hub.Configuration.IValues values = subject.FromJson(Resources.SimpleConfiguration);

            Assert.IsNotNull(values);
            Assert.AreEqual<int>(1, values.Activities.Count(), "Did not correctly deserialize Activities");
            Assert.AreEqual<int>(1, values.Devices.Count(), "Did not correctly deserialize Devices");

            var device = values.Devices.First();

            Assert.AreEqual<int>(6, device.Controls.Count());
        }
    }
}
