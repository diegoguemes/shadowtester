namespace ShadowTester.Domain.System
{
    public class SystemInfo
    {
        public string Processor { get; set; }
        public string HardDisk { get; set; }
        public string Os { get; set; }
        public string Ram { get; set; }
        public string User { get; set; }

        public override string ToString()
        {
            return "Processor: " + Processor + "\n" +
                   "Hard Disk: " + HardDisk + "\n" +
                   "RAM: " + Ram + "\n" +
                   "OS: " + Os + "\n" +
                   "User: " + User;
        }
    }
}