using System.IO;
using NUnit.Framework;
using ShadowTester.Domain.Recorder;

namespace ShadowTester.IntegrationTests
{
    [TestFixture]
    public class RecordValidatorTests
    {
        RecordValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new RecordValidator();
        }

        [TearDown]
        public void TearDown()
        {
            if(Directory.Exists("path"))
            {
                if(File.Exists("path/file"))
                {
                    File.Delete("path/file");
                }
                Directory.Delete("path");
            }
        }

        [Test]
        public void ValidateConfig()
        {
            
            RecordConfiguration configuration = new RecordConfiguration()
                                                    {
                                                        Fps = 12, 
                                                        CapturesPath = "NOT_FOUND_DIR", 
                                                        Name = "record"
                                                    };

            bool result = validator.IsValid(configuration);

            Assert.True(result);
        }

        [Test]
        public void ValidateConfigWithFpsLessThanMinFps()
        {
            RecordConfiguration configuration = new RecordConfiguration()
            {
                Fps = 0,
                CapturesPath = "NOT_FOUND_DIR",
                Name = "record"
            };

            bool result = validator.IsValid(configuration);

            Assert.False(result);
        }

        [Test]
        public void ValidateConfigWithFpsGreaterThanMaxFps()
        {
            RecordConfiguration configuration = new RecordConfiguration()
            {
                Fps = 25,
                CapturesPath = "NOT_FOUND_DIR",
                Name = "record"
            };

            bool result = validator.IsValid(configuration);

            Assert.False(result);
        }

        [Test]
        public void ValidateConfigWithEmptyName()
        {
            RecordConfiguration configuration = new RecordConfiguration()
            {
                Fps = 12,
                CapturesPath = "NOT_FOUND_DIR",
                Name = ""
            };

            bool result = validator.IsValid(configuration);

            Assert.False(result);
        }

        [Test]
        public void ValidateConfigWithInexistentPath()
        {
            RecordConfiguration configuration = new RecordConfiguration()
            {
                Fps = 12,
                CapturesPath = "NOT_FOUND_DIR",
                Name = "record"
            };

            bool result = validator.IsValid(configuration);

            Assert.True(result);
        }

        [Test]
        public void ValidateConfigWithEmptyExistentPath()
        {
            Directory.CreateDirectory("path");
            RecordConfiguration configuration = new RecordConfiguration()
            {
                Fps = 12,
                CapturesPath = "path",
                Name = "record"
            };

            bool result = validator.IsValid(configuration);

            Assert.True(result);
        }

        [Test]
        public void ValidateConfigWithNotEmptyExistentPath()
        {
            Directory.CreateDirectory("path");
            using (File.Create("path/file"))
            {
            }
            RecordConfiguration configuration = new RecordConfiguration()
            {
                Fps = 12,
                CapturesPath = "path",
                Name = "record"
            };

            bool result = validator.IsValid(configuration);

            Assert.False(result);
        }
    }
}