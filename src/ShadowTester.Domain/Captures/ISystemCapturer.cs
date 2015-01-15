using System.Collections.Generic;

namespace ShadowTester.Domain.Captures
{
    public interface ISystemCapturer
    {
        void CaptureForegroundProcess(string filename, IList<string> expectedProceses);
        void CaptureSystemInformation(string filename);
    }
}