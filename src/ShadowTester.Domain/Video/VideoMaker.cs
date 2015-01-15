using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ShadowTester.Domain.Video
{
    public class VideoMaker
    {
        private string ffmpegExecutable;
        private VideoMakerValidator validator;
        private ImagesSequenceRenamer renamer;

        public VideoMaker(VideoMakerValidator validator, ImagesSequenceRenamer renamer)
        {
            this.validator = validator;
            this.renamer = renamer;
            // TODO: Configure ffpeg path in other place
            ffmpegExecutable = AppDomain.CurrentDomain.BaseDirectory + @"\ffmpeg";
        }

        public bool FromImages(string filename, string imagesPath, int fps)
        {
            validator.ValidatePath(imagesPath);
            renamer.RenameImages(imagesPath);
            RunFfmpegProcess(filename, imagesPath, fps);
            return File.Exists(filename);
        }

        private void RunFfmpegProcess(string filename, string imagesPath, int fps)
        {
            Process process = new Process();
            process.StartInfo.FileName = ffmpegExecutable;
            process.StartInfo.Arguments = "-r " + fps + " -i " + imagesPath + "/%10d.jpg -y " + filename;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
            process.Close();
        }


    }
}