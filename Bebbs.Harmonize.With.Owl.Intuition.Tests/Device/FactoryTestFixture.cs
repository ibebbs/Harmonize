using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bebbs.Harmonize.With.Owl.Intuition.Device;
using FakeItEasy;

namespace Bebbs.Harmonize.With.Owl.Intuition.Tests.Device
{
    [TestClass]
    public class FactoryTestFixture
    {
        [TestMethod]
        public void ShouldBeAbleToCreateADeviceContext()
        {
            Factory factory = new Factory();

            Settings.IProvider settingsProvider = A.Fake<Settings.IProvider>();

            IContext context = factory.CreateDeviceInContext(settingsProvider);

            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Kernel);
            Assert.IsNotNull(context.Instance);

            Assert.AreEqual(settingsProvider, context.SettingsProvider);
        }
    }
}
