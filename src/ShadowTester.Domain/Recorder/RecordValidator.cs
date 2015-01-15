using System;
using System.Collections.Generic;
using System.IO;

namespace ShadowTester.Domain.Recorder
{
    public class RecordValidator
    {
        private const int MIN_FPS = 1;
        private const int MAX_FPS = 24;

        private IList<string> GetErrors(RecordConfiguration configuration)
        {
            IList<string> errors = new List<string>();
            if(!ValidateFps(configuration.Fps))
            {
                errors.Add(string.Format("FPS must be a number between {0} and {1}", MIN_FPS, MAX_FPS));
            }
            if (!ValidateName(configuration.Name))
            {
                errors.Add(string.Format("Name cannot be empty"));
            }
            if(!ValidatePath(configuration.CapturesPath))
            {
                errors.Add(string.Format("Path must be an inexistent directory or an empty existent directory"));
            }
            return errors;
        }

        public void Validate(RecordConfiguration configuration)
        {
            if(!IsValid(configuration))
            {
                throw new ArgumentException(string.Join("\n", GetErrors(configuration)));
            }
        }

        public bool IsValid(RecordConfiguration configuration)
        {
            return GetErrors(configuration).Count == 0;
        }

        private bool ValidatePath(string path)
        {
            return !Directory.Exists(path) || IsEmptyPath(path);
        }

        private bool IsEmptyPath(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory.GetFiles().Length == 0 && directory.GetDirectories().Length == 0;
        }

        private bool ValidateFps(int fps)
        {
            return fps >= MIN_FPS && fps <= MAX_FPS;
        }

        // TODO: validate filename
        private bool ValidateName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
    }
}