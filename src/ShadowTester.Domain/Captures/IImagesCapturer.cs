using System.Drawing;

namespace ShadowTester.Domain.Captures
{
    public interface IImagesCapturer
    {
        void ScreenShot(string filename, Point origin, Size size);
        void FromString(string filename, string text);
    }
}