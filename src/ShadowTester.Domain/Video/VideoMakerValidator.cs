using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShadowTester.Domain.Video
{
    public class VideoMakerValidator
    {
        public void ValidatePath(string imagesPath)
        {
            if (!ThereAreImages(GetImageFiles(imagesPath)))
            {
                throw new InvalidOperationException("There are no images to generate the video.");
            }
            if (!ImageNamesHasSameLength(GetImageFiles(imagesPath)))
            {
                throw new InvalidOperationException("The images have invalid names.");
            }
        }

        private IEnumerable<string> GetImageFiles(string imagesPath)
        {
            return Directory.EnumerateFiles(imagesPath).Where(f => f.EndsWith(".jpg"));
        }

        private bool ThereAreImages(IEnumerable<string> imageFiles)
        {
            return imageFiles.Count() > 0;
        }

        private bool ImageNamesHasSameLength(IEnumerable<string> imageFiles)
        {
            return imageFiles.All(f => f.Length == imageFiles.First().Length);
        }
    }
}