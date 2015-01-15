using System;
using System.IO;
using System.Linq;

namespace ShadowTester.Domain.Storage
{
    public class StorageManager
    {
        private DirectoryInfo directory;

        public void CreateCapturesDirectory(string path)
        {
            directory = Directory.CreateDirectory(path);
        }

    }
}