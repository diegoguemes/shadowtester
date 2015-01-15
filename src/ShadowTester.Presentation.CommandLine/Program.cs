using System;
using CommandLine;
using ShadowTester.Domain;
using ShadowTester.Domain.Recorder;
using ShadowTester.Domain.Storage;
using ShadowTester.Presentation.CommandLine.Commands;

namespace ShadowTester.Presentation.CommandLine
{
    class Program
    {
        private static StorageManager storageManager;
        private static RecordConfiguration recordConfiguration;

        static void Main(string[] args)
        {
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
            string option;
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
            Console.WriteLine(GetHeader());
            Console.WriteLine("Recording...");
            Console.WriteLine(GetSeparator());
            do
            {
                Console.WriteLine("Actions:");
                Console.WriteLine(GetMenu());
                option = ConsoleHelper.GetMenuOption();
                Console.WriteLine(GetSeparator());
                ConsoleCommand command = ConsoleCommandFactory.CreateCommand(option, recordConfiguration);
                command.Execute();
                Console.WriteLine(GetSeparator());
            } while (option != ConsoleHelper.STOP_RECORDING_ACTION);
        }

        private static string GetSeparator()
        {
            return string.Empty;
        }

        private static string GetHeader()
        {
            return "+-------------------------------------------+\n" +
                   "|    SHADOWTESTER COMMAND LINE INTERFACE    |\n" +
                   "+-------------------------------------------+";
        }

        private static string GetMenu()
        {
            return "1. Add Process\n" +
                   "2. Remove Process\n" +
                   "3. Current Processes\n" +
                   "4. Stop Recording";
        }
    }
}
