using System;
using ShadowTester.Domain.Captures;
using ShadowTester.Domain.Recorder;

namespace ShadowTester.Presentation.CommandLine.Commands
{
    public class CurrentProcessesCommand : ConsoleCommand
    {
        private RecordConfiguration configuration;

        public CurrentProcessesCommand(RecordConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public override void Execute()
        {
            Console.WriteLine(String.Join("\n", configuration.ExpectedProcesses));
        }
    }
}