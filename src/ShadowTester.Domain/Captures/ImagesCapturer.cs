using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace ShadowTester.Domain.Captures
{
    public class ImagesCapturer : IImagesCapturer
    {
        private const string FONT_FAMILY = "Arial";
        private const int FONT_SIZE = 30;

        public void ScreenShot(string filename, Point origin, Size size)
        {
            ImagesHandler imagesHandler = new ImagesHandler();
            using (Image image = imagesHandler.FromScreen(origin, size))
            {
                image.Save(filename, ImageFormat.Jpeg);
            }
        }
        
        public void FromString(string filename, string text)
        {
            ImagesHandler imagesHandler = new ImagesHandler();
            using (Image image = imagesHandler.Empty())
            {
                AddText(image, text);
                image.Save(filename, ImageFormat.Jpeg);
            }
        }

        private void AddText(Image image, string text)
        {
            using (Graphics graphics = Graphics.FromImage(image))
            {
                Font font = new Font(FONT_FAMILY, FONT_SIZE, FontStyle.Bold, GraphicsUnit.Pixel);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.DrawString(text, font, new SolidBrush(Color.White), 100, 100);
            }
        }
    }
}