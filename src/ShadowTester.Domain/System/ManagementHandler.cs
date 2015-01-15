using System;
using System.Linq;
using System.Management;
using System.Security.Principal;
using ShadowTester.Util;

namespace ShadowTester.Domain.System
{
    public class ManagementHandler
    {
        private const string WIN32_PROCESSOR = "Win32_Processor";
        private const string WIN32_DISKDRIVE = "Win32_DiskDrive";
        private const string WIN32_OPERATIVESYSTEM = "Win32_OperatingSystem";
        private const string WIN32_PHYSICALMEMORY = "Win32_PhysicalMemory";

        public string GetUser()
        {
            return WindowsIdentity.GetCurrent().Name;
        }

        public string GetProcessor()
        {
            foreach (ManagementObject managementObject in GetManagementObjectCollection(WIN32_PROCESSOR))
            {
                return managementObject.Properties["Name"].Value.ToString();
            }
            return String.Empty;
        }

        private ManagementObjectCollection GetManagementObjectCollection(string queryObject)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0}", queryObject));
            return searcher.Get();
        }

        public string GetHardDisk()
        {
            foreach (ManagementObject managementObject in GetManagementObjectCollection(WIN32_DISKDRIVE))
            {
                return DataStorageUnitHelper.BytesToGbs(long.Parse(managementObject.Properties["Size"].Value.ToString())) + " GB";
            }
            return String.Empty;
        }

        public string GetOs()
        {
            foreach (ManagementObject managementObject in GetManagementObjectCollection(WIN32_OPERATIVESYSTEM))
            {
                string[] osParts = managementObject.Properties["Name"].Value.ToString().Split(("|").ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string os = osParts[0];
                return os;
            }
            return String.Empty;
        }

        public string GetRam()
        {
            long capacity = 
                GetManagementObjectCollection(WIN32_PHYSICALMEMORY)
                    .Cast<ManagementObject>()
                    .Sum(managementObject => long.Parse(managementObject.Properties["Capacity"].Value.ToString()));
            return DataStorageUnitHelper.BytesToGbs(capacity) + " GB";
        }
    }
}