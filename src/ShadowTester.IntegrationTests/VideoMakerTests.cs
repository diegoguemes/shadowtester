using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using ShadowTester.Domain.Captures;
using ShadowTester.Domain.Video;

namespace ShadowTester.IntegrationTests
{
    [TestFixture]
    public class VideoMakerTests
    {
        private VideoMaker videoMaker;

        [SetUp]
        public void SetUp()
        {
            videoMaker = new VideoMaker(new VideoMakerValidator(), new ImagesSequenceRenamer());
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete("0000000001.jpg");
            File.Delete("0000000002.jpg");
            File.Delete("000000002.jpg");
            File.Delete("0000000003.jpg");
            File.Delete("video.avi");
        }

        [Test]
        public void CreateVideoFromImages()
        {
            ImagesCapturer imagesCapturer = new ImagesCapturer();
            imagesCapturer.ScreenShot("0000000001.jpg", Point.Empty, Size.Empty);
            imagesCapturer.ScreenShot("0000000002.jpg", Point.Empty, Size.Empty);
            videoMaker.FromImages("video.avi", "./", 12);
            Assert.True(File.Exists("video.avi"));
        }

        [Test]
        public void ThrowExceptionWhenThereAreNotImages()
        {
            try
            {
                videoMaker.FromImages("video.avi", "./", 12);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {

            }
        }

        [Test]
        public void ThrowExceptionWhenThereAreDifferentImageNamesLength()
        {
            try
            {
                ImagesCapturer imagesCapturer = new ImagesCapturer();
                imagesCapturer.ScreenShot("0000000001.jpg", Point.Empty, Size.Empty);
                imagesCapturer.ScreenShot("000000002.jpg", Point.Empty, Size.Empty);
                videoMaker.FromImages("video.avi", "./", 12);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {

            }
        }

        [Test]
        public void FixImageNamesWhenImageSequenceIsWrong()
        {
            ImagesCapturer imagesCapturer = new ImagesCapturer();
            imagesCapturer.ScreenShot("0000000001.jpg", Point.Empty, Size.Empty);
            imagesCapturer.ScreenShot("0000000003.jpg", Point.Empty, Size.Empty);
            videoMaker.FromImages("video.avi", "./", 12);
            Assert.True(File.Exists("video.avi"));
            Assert.True(File.Exists("0000000001.jpg"));
            Assert.True(File.Exists("0000000002.jpg"));
            Assert.False(File.Exists("0000000003.jpg"));
        }

    }
}