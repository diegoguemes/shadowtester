using System;
using ShadowTester.Domain;
using ShadowTester.Domain.Recorder;

namespace ShadowTester.Presentation.CommandLine.Commands
{
    public class ConsoleCommandFactory
    {
        public static ConsoleCommand CreateCommand(string inputCommand, RecordConfiguration recordConfiguration)
        {
            switch (inputCommand)
            {
                case ConsoleHelper.ADD_PROCESS_ACTION:
                    return new AddProcessCommand(recordConfiguration);
                case ConsoleHelper.REMOVE_PROCESS_ACTION:
                    return new RemoveProcessCommand(recordConfiguration);
                case ConsoleHelper.PRINT_CURRENT_PROCESSES_ACTION:
                    return new CurrentProcessesCommand(recordConfiguration);
                case ConsoleHelper.STOP_RECORDING_ACTION:
                    return new StopRecordingCommand(Factory.ProcessRecorder, Factory.VideoMaker, recordConfiguration);
            }
            throw new InvalidOperationException("Invalid Console Command");
        }
    }
}