namespace ShadowTester.Domain.System
{
    public class SystemMonitor : ISystemMonitor
    {
        public WindowData GetForegroundWindow()
        {
            WindowHandler windowHandler = new WindowHandler();
            return new WindowData()
            {
                Position = windowHandler.GetForegroundWindowPosition(),
                Process = windowHandler.GetForegroundProcess(),
                Size = windowHandler.GetForegroundWindowSize()
            };
        }

        public SystemInfo GetSystemInformation()
        {
            ManagementHandler managementHandler = new ManagementHandler();
            return new SystemInfo()
                       {
                           Processor = managementHandler.GetProcessor(),
                           HardDisk = managementHandler.GetHardDisk(),
                           Os = managementHandler.GetOs(),
                           Ram = managementHandler.GetRam(),
                           User = managementHandler.GetUser()
                       };
        }
    }
}