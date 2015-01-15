using NUnit.Framework;
using ShadowTester.Domain.Captures;
using ShadowTester.Domain.Recorder;

namespace ShadowTester.UnitTests
{
    [TestFixture]
    public class RecordConfigurationTests
    {
        RecordConfiguration configuration;

        [SetUp]
        public void SetUp()
        {
            configuration = new RecordConfiguration();
        }

        [Test]
        public void ConfigureRecordFrequency()
        {
            RecordConfiguration configuration = new RecordConfiguration()
                                                    {
                                                        Fps = 12
                                                    };


            Assert.AreEqual(1000 / 12, configuration.Period);
        }

        [Test]
        public void InitializeProcesses()
        {    
            var processes = new string[] { "process1", "process2" };

            configuration.InitializeProcesses(processes);

            Assert.AreEqual(processes, configuration.ExpectedProcesses);
            Assert.AreNotSame(processes, configuration.ExpectedProcesses);
        }

        [Test]
        public void AddProcess()
        {
            configuration.InitializeProcesses(new string[] { "process1" });

            configuration.AddProcess("process2");

            Assert.True(configuration.ExpectedProcesses.Contains("process2"));
        }

        [Test]
        public void RemoveProcess()
        {
            configuration.InitializeProcesses(new string[] { "process1" });

            bool result = configuration.RemoveProcess("process1");

            Assert.True(result);
            Assert.False(configuration.ExpectedProcesses.Contains("process1"));
        } 
    }
}