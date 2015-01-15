using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ShadowTester.Domain.Captures
{
    public class ImagesHandler
    {
        public Image FromScreen()
        {
            Rectangle screenBounds = Screen.GetBounds(Point.Empty);
            return FromScreen(Point.Empty, screenBounds.Size);
        }

        public Image FromScreen(Point origin, Size size)
        {
            Rectangle screenBounds = Screen.GetBounds(Point.Empty);
            Image image = new Bitmap(screenBounds.Width, screenBounds.Height);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.CopyFromScreen(origin, origin, size);
                CaptureMouse(graphics);
            }
            return image;
        }

        public Image Empty()
        {
            Rectangle screenBounds = Screen.GetBounds(Point.Empty);
            return new Bitmap(screenBounds.Width, screenBounds.Height);
        }

        private void CaptureMouse(Graphics graphics)
        {
            if (Cursor.Current != null)
            {
                Rectangle cursorBounds = new Rectangle(Cursor.Position, Cursor.Current.Size);
                Cursors.Default.Draw(graphics, cursorBounds);
            }
        }
    }
}