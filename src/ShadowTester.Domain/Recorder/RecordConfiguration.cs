using System.Collections.Generic;
using System.Linq;

namespace ShadowTester.Domain.Recorder
{
    public class RecordConfiguration
    {
        private const int MILLISECONDS_PER_SECOND = 1000;
        private List<string> expectedProcess;

        public int Fps { get; set; }
        public string CapturesPath { get; set; }
        public string Name { get; set; }

        public IList<string> ExpectedProcesses
        {
            get
            {
                return expectedProcess.AsReadOnly();
            }
        }

        public void InitializeProcesses(IEnumerable<string> processes)
        {
            expectedProcess = processes.ToList();
        }

        public void AddProcess(string process)
        {
            expectedProcess.Add(process);
        }

        public bool RemoveProcess(string process)
        {
            return expectedProcess.Remove(process);
        }

        public double Period
        {
            get
            {
                return MILLISECONDS_PER_SECOND / Fps;
            }
        }
    }
}