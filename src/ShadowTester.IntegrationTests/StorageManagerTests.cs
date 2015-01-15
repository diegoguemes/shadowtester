using System;
using System.IO;
using NUnit.Framework;
using ShadowTester.Domain;
using ShadowTester.Domain.Storage;

namespace ShadowTester.IntegrationTests
{
    [TestFixture]
    public class StorageManagerTests
    {
        [TearDown]
        public void TearDown()
        {
            File.Delete("dir/file.txt");
            Directory.Delete("dir");
        }

        [Test]
        public void CapturesDirectoryIsCreated()
        {
            StorageManager storageManager = new StorageManager();

            storageManager.CreateCapturesDirectory("dir");

            DirectoryInfo directoryInfo = new DirectoryInfo("dir");
            if(!directoryInfo.Exists)
            {
                Assert.Fail("Directory should have been created");
            }
        }
    }
}