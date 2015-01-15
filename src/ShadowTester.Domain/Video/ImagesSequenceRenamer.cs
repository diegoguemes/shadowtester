using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShadowTester.Domain.Video
{
    public class ImagesSequenceRenamer
    {
        private int FORMAT_LENGTH = 10;
        public void RenameImages(string imagesPath)
        {
            int imageNumber = 1;
            foreach (string imageFile in GetImageFiles(imagesPath))
            {
                File.Move(imageFile, GetNewImageFileName(imageFile, imageNumber));
                ++imageNumber;
            }
        }

        private IEnumerable<string> GetImageFiles(string imagesPath)
        {
            return Directory.EnumerateFiles(imagesPath).Where(f => f.EndsWith(".jpg"));
        }

        private string GetNewImageFileName(string oldImageFile, int imageNumber)
        {
            return oldImageFile.Replace(Path.GetFileNameWithoutExtension(oldImageFile), NumericNameFormat(imageNumber, FORMAT_LENGTH));
        }

        private string NumericNameFormat(int number, int length)
        {
            return number.ToString().PadLeft(length, Convert.ToChar("0"));
        }
    }
}