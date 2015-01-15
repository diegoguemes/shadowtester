using System.Collections.Generic;
using ShadowTester.Domain.System;

namespace ShadowTester.Domain.Captures
{
    public class SystemCapturer : ISystemCapturer
    {
        private ISystemMonitor monitor;
        private IImagesCapturer imagesCapturer;

        public SystemCapturer(ISystemMonitor monitor, IImagesCapturer imagesCapturer)
        {
            this.monitor = monitor;
            this.imagesCapturer = imagesCapturer;
        }

        public void CaptureForegroundProcess(string filename, IList<string> expectedProceses)
        {
            WindowData foregroundWindow = monitor.GetForegroundWindow();
            if (expectedProceses.Contains(foregroundWindow.Process))
            {
                imagesCapturer.ScreenShot(filename, foregroundWindow.Position, foregroundWindow.Size);
            }
        }

        public void CaptureSystemInformation(string filename)
        {
            SystemInfo systemInfo = monitor.GetSystemInformation();
            imagesCapturer.FromString(filename, systemInfo.ToString());
        }
    }
}