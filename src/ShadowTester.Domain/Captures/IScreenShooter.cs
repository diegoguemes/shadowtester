using System.Drawing;

namespace ShadowTester.Domain.Captures
{
    public interface IScreenShooter
    {
        void Capture(string filename, Point origin, Size size);
    }
}