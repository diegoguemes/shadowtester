using System;

namespace ShadowTester.Presentation.CommandLine
{
    public static class ConsoleHelper
    {
        public const string ADD_PROCESS_ACTION = "1";
        public const string REMOVE_PROCESS_ACTION = "2";
        public const string PRINT_CURRENT_PROCESSES_ACTION = "3";
        public const string STOP_RECORDING_ACTION = "4";
        private const string YES = "Y";
        private const string NO = "N";

        public static string GetInputFromConsole()
        {
            return Console.ReadLine().Trim();
        }

        public static string GetInputFromConsole(string defaultValue)
        {
            string input = Console.ReadLine().Trim();
            return String.IsNullOrWhiteSpace(input) 
                ? 
                    defaultValue 
                : 
                    input;
        }

        public static string GetFromConsole(string message, string error, Predicate<string> checkInput)
        {
            string input;
            Console.Write(message);
            input = GetInputFromConsole();
            while (!checkInput(input))
            {
                Console.Write(error + " " + message);
                input = GetInputFromConsole();
            }
            return input;
        }

        public static string GetFromConsole(string message, string error, Predicate<string> checkInput, string defaultValue)
        {
            Console.Write(message);
            string input = GetInputFromConsole(defaultValue);
            while (!checkInput(input))
            {
                Console.Write(error + " " + message);
                input = GetInputFromConsole(defaultValue);
            }
            return input;
        }

        public static string GetProcessFromConsole()
        {
            return GetFromConsole("Process name (not empty): ", "Invalid process name.", i => !String.IsNullOrWhiteSpace(i));
        }

        public static bool GetBooleanInputFromConsole()
        {
            string input = GetFromConsole(
                String.Format("The file already exists. Do you want overwrite the file ({0}/{1}) [{2}]: ", YES, NO, NO),
                "Invalid option.",
                CheckBooleanOption,
                NO
            );
            return BooleanStringToBool(input);
        }

        public static bool CheckBooleanOption(string option)
        {
            return option.ToUpperInvariant() == YES || option.ToUpperInvariant() == NO;
        }

        public static string GetMenuOption()
        {
            return GetFromConsole("Choose an option (introduce a number): ", "Invalid option.", CheckMenuOption);
        }

        public static bool CheckMenuOption(string option)
        {
            return option == ADD_PROCESS_ACTION ||
                   option == REMOVE_PROCESS_ACTION ||
                   option == PRINT_CURRENT_PROCESSES_ACTION ||
                   option == STOP_RECORDING_ACTION;
        }

        public static bool BooleanStringToBool(string booleanStr)
        {
            return booleanStr.ToUpperInvariant() == YES;
        }
    }
}