using System.Drawing;
using NUnit.Framework;
using ShadowTester.Domain;
using ShadowTester.Domain.System;

namespace ShadowTester.IntegrationTests
{
    [TestFixture]
    public class SystemMonitorTests
    {
        private SystemMonitor systemMonitor;

        [SetUp]
        public void SetUp()
        {
            systemMonitor = new SystemMonitor();
        }

        [Test]
        public void ForegroundWindowExists()
        {
            WindowData foregroundWindow = systemMonitor.GetForegroundWindow();
            Assert.IsNotNullOrEmpty(foregroundWindow.Process);
            Assert.IsNotNull(foregroundWindow.Size);
            Assert.IsNotNull(foregroundWindow.Position);
        }

        [Test]
        public void GetSystemData()
        {
            SystemInfo systemInfo = systemMonitor.GetSystemInformation();
            Assert.IsNotNullOrEmpty(systemInfo.Processor);
            Assert.IsNotNullOrEmpty(systemInfo.HardDisk);
            Assert.IsNotNullOrEmpty(systemInfo.Ram);
            Assert.IsNotNullOrEmpty(systemInfo.Os);
            Assert.IsNotNullOrEmpty(systemInfo.User);
        }
    }
}