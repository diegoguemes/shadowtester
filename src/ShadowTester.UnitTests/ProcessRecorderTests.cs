using NUnit.Framework;
using Rhino.Mocks;
using ShadowTester.Domain;
using ShadowTester.Domain.Captures;
using ShadowTester.Domain.Recorder;

namespace ShadowTester.UnitTests
{
    [TestFixture]
    public class ProcessRecorderTests
    {
        [Test]
        public void RecordSystemInformationOnStart()
        {
            RecordConfiguration configuration = GetDummyRecordConfiguration();
            ISystemCapturer systemCapturerMock = MockRepository.GenerateStrictMock<ISystemCapturer>();
            systemCapturerMock.Expect(m => m.CaptureSystemInformation("")).IgnoreArguments();
            ProcessRecorder processRecorder = new ProcessRecorder(systemCapturerMock);
            processRecorder.Configure(configuration);

            processRecorder.Start();

            systemCapturerMock.VerifyAllExpectations();
        }

        private static RecordConfiguration GetDummyRecordConfiguration()
        {
            return new RecordConfiguration()
                       {
                           CapturesPath = "",
                           Fps = 12
                       };
        }
    }
}
