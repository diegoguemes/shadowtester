using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NUnit.Framework;
using ShadowTester.Domain.Captures;

namespace ShadowTester.IntegrationTests
{
    [TestFixture]
    public class ImagesCapturerTests
    {
        private Image image;
        private ImagesCapturer imagesCapturer;

        [SetUp]
        public void SetUp()
        {
            imagesCapturer = new ImagesCapturer();
        }

        [TearDown]
        public void TearDown()
        {
            if (image != null)
            {
                image.Dispose();
                File.Delete("screen.jpg");
            }
            File.Delete("image.jpg");
        }

        [Test]
        public void CreateScreenShot()
        {
            string filename = "screen.jpg";

            imagesCapturer.ScreenShot(filename, Point.Empty, new Size(100, 100));
            image = Image.FromFile(filename);

            Assert.IsNotNull(image);
        }

        [Test]
        public void ScreenShotHasSameSizeThatScreen()
        {
            string filename = "screen.jpg";

            imagesCapturer.ScreenShot(filename, Point.Empty, new Size(100, 100));
            image = Image.FromFile(filename);
            Size screenSize = Screen.PrimaryScreen.Bounds.Size;

            Assert.AreEqual(screenSize, image.Size);
        }

        [Test]
        public void TryCreateScreenShotWithWrongPath()
        {
            string notFoundPath = "/not_found_path/screen.jpg";
            try
            {
                imagesCapturer.ScreenShot(notFoundPath, Point.Empty, new Size(100, 100));
                Assert.Fail("Exception should have been caught.");
            }
            catch (ExternalException)
            {

            }
        }

        [Test]
        public void ConvertTextToImage()
        {
            imagesCapturer.FromString("image.jpg", "Message");

            Assert.True(File.Exists("image.jpg"));
        }
    }
}