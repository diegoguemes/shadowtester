using System.Drawing;
using NUnit.Framework;
using Rhino.Mocks;
using ShadowTester.Domain.Captures;
using ShadowTester.Domain.System;

namespace ShadowTester.UnitTests
{
    [TestFixture]
    public class SystemCapturerTests
    {
        [Test]
        public void CaptureForegroundProcessAsksProcessMonitor()
        {

            var processMonitorMock = MockRepository.GenerateStrictMock<ISystemMonitor>();
            var imagesCapturerStub = MockRepository.GenerateStub<IImagesCapturer>();
            processMonitorMock.Expect(m => m.GetForegroundWindow()).Return(new WindowData());
            SystemCapturer systemCapturer = new SystemCapturer(processMonitorMock, imagesCapturerStub);

            systemCapturer.CaptureForegroundProcess("screen.jpg", new string[] { });

            processMonitorMock.VerifyAllExpectations();
        }

        [Test]
        public void CaptureForegroundProcessTakesScreenShotWhenForegroundProcessIsExpected()
        {
            var position = new Point(100, 100);
            var size = new Size(100, 100);
            var process = "process";
            var processMonitorStub = MockRepository.GenerateStub<ISystemMonitor>();
            processMonitorStub.Stub(s => s.GetForegroundWindow()).Return(new WindowData()
            {
                Position = position,
                Process = process,
                Size = size
            });
            var imagesCapturerMock = MockRepository.GenerateStrictMock<IImagesCapturer>();
            imagesCapturerMock.Expect(m => m.ScreenShot("screen.jpg", position, size)).Repeat.Once();
            SystemCapturer systemCapturer = new SystemCapturer(processMonitorStub, imagesCapturerMock);

            systemCapturer.CaptureForegroundProcess("screen.jpg", new string[] { "process" });

            imagesCapturerMock.VerifyAllExpectations();
        }

        [Test]
        public void CaptureForegroundProcessDoesntTakeScreenShotWhenForegroundProcessIsNotExpected()
        {
            var processMonitorStub = MockRepository.GenerateStub<ISystemMonitor>();
            processMonitorStub.Stub(s => s.GetForegroundWindow()).Return(new WindowData());
            var imagesCapturerMock = MockRepository.GenerateStrictMock<IImagesCapturer>();
            imagesCapturerMock.Expect(m => m.ScreenShot("screen.jpg", Point.Empty, Size.Empty)).IgnoreArguments().Repeat.Never();
            SystemCapturer processCapturer = new SystemCapturer(processMonitorStub, imagesCapturerMock);

            processCapturer.CaptureForegroundProcess("screen.jpg", new string[] { "process" });

            imagesCapturerMock.VerifyAllExpectations();
        }

        [Test]
        public void CaptureSystemInformationAsksProcessMonitor()
        {
            ISystemMonitor monitorMock = MockRepository.GenerateStrictMock<ISystemMonitor>();
            IImagesCapturer imagesCapturerStub = MockRepository.GenerateStub<IImagesCapturer>();
            monitorMock.Expect(m => m.GetSystemInformation()).Return(new SystemInfo());
            SystemCapturer systemCapturer = new SystemCapturer(monitorMock, imagesCapturerStub);

            systemCapturer.CaptureSystemInformation("system_info.jpeg");

            monitorMock.VerifyAllExpectations();
        }

        [Test]
        public void CaptureSystemInformationConvertsInfoToImage()
        {
            SystemInfo systemInfo = new SystemInfo();
            ISystemMonitor monitorStub = MockRepository.GenerateStub<ISystemMonitor>();
            monitorStub.Expect(s => s.GetSystemInformation()).Return(systemInfo);
            IImagesCapturer imagesCapturerMock = MockRepository.GenerateStrictMock<IImagesCapturer>();
            imagesCapturerMock.Expect(m => m.FromString("system_info.jpeg", systemInfo.ToString()));
            SystemCapturer systemCapturer = new SystemCapturer(monitorStub, imagesCapturerMock);

            systemCapturer.CaptureSystemInformation("system_info.jpeg");

            imagesCapturerMock.VerifyAllExpectations();
        }
    }
}