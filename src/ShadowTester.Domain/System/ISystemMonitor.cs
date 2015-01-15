namespace ShadowTester.Domain.System
{
    public interface ISystemMonitor
    {
        WindowData GetForegroundWindow();
        SystemInfo GetSystemInformation();
    }
}