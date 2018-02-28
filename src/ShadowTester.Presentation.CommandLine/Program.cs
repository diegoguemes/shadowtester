using System;
using CommandLine;
using ShadowTester.Domain;
using ShadowTester.Domain.Recorder;
using ShadowTester.Domain.Storage;
using ShadowTester.Presentation.CommandLine.Commands;
using System.IO;

namespace ShadowTester.Presentation.CommandLine
{
    class Program
    {
        private static StorageManager storageManager;
        private static RecordConfiguration recordConfiguration;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                string sessionName = "shadowtesting-session-" + DateTime.Now.ToString("yyyyMMdd-hhmm");

                string directory = Path.Combine(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "shadowtesting"),
                    sessionName);

                args = new string[] {
                    "--name=" + sessionName,
                    "--path=" + directory,
                    "--fps=2",
                    "--processes=plastic,gluon,bplastic,bgluon"
                };
            }

            if (ConfigureApplication(args))
            {
                RunApplication();
            }
        }

        private static bool ConfigureApplication(string[] args)
        {
            CommandLineParser parser = new CommandLineParser();
            CommandLineOptions options = new CommandLineOptions();
            if(parser.ParseArguments(args, options))
            {
                recordConfiguration = CreateConfigFromOptions(options);
                RecordValidator recordValidator = new RecordValidator();
                try
                {
                    recordValidator.Validate(recordConfiguration);
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                storageManager = new StorageManager();
                Factory.ProcessRecorder.Configure(recordConfiguration);
                return true;
            }
            Console.WriteLine(options.GetHelp());
            return false;
        }

        private static RecordConfiguration CreateConfigFromOptions(CommandLineOptions options)
        {
            RecordConfiguration configuration = new RecordConfiguration
                                                    {
                                                        Name = options.Name,
                                                        CapturesPath = options.CapturesPath,
                                                        Fps = options.Fps
                                                    };
            configuration.InitializeProcesses(options.processes);
            return configuration;
        }

        private static void RunApplication()
        {
            try
            {
                storageManager.CreateCapturesDirectory(recordConfiguration.CapturesPath);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Factory.ProcessRecorder.Start();

            Console.WriteLine("Recording on directory {0}...", recordConfiguration.CapturesPath);
            Console.WriteLine();

            Console.WriteLine("Press ENTER to finish and create the video");
            Console.ReadLine();

            Console.WriteLine("Session video will be created at {0}",
                recordConfiguration.CapturesPath);

            ConsoleCommand command = ConsoleCommandFactory.CreateCommand(
                ConsoleHelper.STOP_RECORDING_ACTION, recordConfiguration);
            command.Execute();

            Console.WriteLine("Press ENTER to quit");
            Console.ReadLine();

            OpenFileWith("explorer.exe", recordConfiguration.CapturesPath, "/root,");
        }

        static void OpenFileWith(string exePath, string path, string arguments)
        {
            if (path == null)
                return;

            using (System.Diagnostics.Process process = new System.Diagnostics.Process())
            {
                process.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
                if (exePath != null)
                {
                    process.StartInfo.FileName = exePath;
                    //Pre-post insert quotes for fileNames with spaces.
                    process.StartInfo.Arguments = string.Format("{0}\"{1}\"", arguments, path);
                }
                else
                {
                    process.StartInfo.FileName = path;
                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
                }

                if (!path.Equals(process.StartInfo.WorkingDirectory))
                {
                    process.Start();
                }
            }
        }
    }
}
