using Bebbs.Harmonize.With.Owl.Intuition.Gateway;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Bebbs.Harmonize.With.Owl.Intuition.Tests.Device
{
    [TestClass]
    public class FactoryTestFixture
    {
        [TestMethod]
        public void ShouldBeAbleToCreateADeviceContext()
        {
            With.Messaging.Client.IEndpoint clientEndpoint = A.Fake<With.Messaging.Client.IEndpoint>();
            IClock clock = A.Fake<IClock>();

            StandardKernel kernel = new StandardKernel();
            kernel.Bind<IClock>().ToConstant(clock).InSingletonScope();

            Factory factory = new Factory(kernel);

            Gateway.Settings.IProvider settingsProvider = A.Fake<Gateway.Settings.IProvider>();

            IContext context = factory.CreateDeviceInContext(settingsProvider, clientEndpoint);

            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Kernel);
            Assert.IsNotNull(context.Instance);

            Assert.AreEqual(settingsProvider, context.SettingsProvider);
        }
    }
}
