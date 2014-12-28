using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;

namespace MetricsNet
{
    public static class Instrumentation
    {
        /// <summary>
        /// Get the CPU Logical Cores usages on the machine.
        /// Do not need to read at a specific interval, it is read from WMI
        /// </summary>
        /// <returns>
        /// Returns a <tt>IList</tt> object with all CPU cores and their values
        /// </returns>
        public static IList GetCpuUsageByCores()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor WHERE Name<>\"_Total\"");
            var cpuTimes = searcher.Get()
                                    .Cast<ManagementObject>()
                                    .Select(mo => new
                                        {
                                            Name = mo["Name"],
                                            Usage = mo["PercentProcessorTime"]
                                        }
                )
                                    .ToList();

            return cpuTimes;
        }

        /// <summary>
        /// Used to get the total cpu usage in the machine
        /// </summary>
        /// <returns>
        /// Returns a <tt>float</tt> value
        /// </returns>
        public static float GetTotalCpuUsage()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor WHERE Name=\"_Total\"");
            var cpuTimes = searcher.Get()
                                   .Cast<ManagementObject>()
                                   .Select(mo => new
                                   {
                                       Usage = mo["PercentProcessorTime"]
                                   }
                                         )
                                   .FirstOrDefault();

            if (cpuTimes != null)
            {
                return float.Parse(cpuTimes.Usage.ToString());
            }
            return -1;
        }

        /// <summary>
        /// Read and returns the number of logical cores on the machine running
        /// </summary>
        /// <returns>
        /// Return an <tt>int</tt> value
        /// </returns>
        public static int GetNumberOfLogicalProcessors()
        {
            try
            {
                return Environment.ProcessorCount;
            }
            catch
            {
                return 0;  
            }
        }

        /// <summary>
        /// Returns the number of available memory in MiB
        /// </summary>
        /// <returns></returns>
        public static Int64 GetPhysicalAvailableMemory()
        {
            return PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
        }

        /// <summary>
        /// Returns the total of memory used in MiB
        /// </summary>
        /// <returns></returns>
        public static Int64 GetPhysicalUsedMemory()
        {
            return PerformanceInfo.GetTotalMemoryInMiB();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetOSName()
        {
            return OSInfo.Name + " " + OSInfo.Edition;
        }

        /// <summary>
        /// Returns the available physical memory in percentage
        /// </summary>
        /// <returns></returns>
        public static decimal GetPhysicalAvailableMemoryInPercentage()
        {
            var phav = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
            var tot = PerformanceInfo.GetTotalMemoryInMiB();
            return Math.Round(((decimal)phav / (decimal)tot * 100), 2);
        }

        /// <summary>
        /// Returns the total physical memory used in percentage
        /// </summary>
        /// <returns></returns>
        public static decimal GetPhysicalUsedMemoryInPercentage()
        {
            var phav = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
            var tot = PerformanceInfo.GetTotalMemoryInMiB();
            var percentFree = ((decimal)phav / (decimal)tot) * 100;
            return Math.Round((100 - percentFree), 2);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Returns an <tt>int</tt> value
        /// </returns>
        public static int GetNumberOfThreads()
        {
            return Process.GetCurrentProcess().Threads.Count;
        }

        /// <summary>
        /// The function determines whether the current operating system is a 
        /// 64-bit operating system.
        /// </summary>
        /// <returns>
        /// The function returns true if the operating system is 64-bit; 
        /// otherwise, it returns false.
        /// </returns>
        public static bool Is64BitOperatingSystem()
        {
            if (IntPtr.Size == 8)  // 64-bit programs run only on Win64
            {
                return true;
            }
            // Detect whether the current process is a 32-bit process 
            // running on a 64-bit system.
            bool flag;
            return ((DoesWin32MethodExist("kernel32.dll", "IsWow64Process") &&
                     IsWow64Process(GetCurrentProcess(), out flag)) && flag);
        }

        /// <summary>
        /// The function determins whether a method exists in the export 
        /// table of a certain module.
        /// </summary>
        /// <param name="moduleName">The name of the module</param>
        /// <param name="methodName">The name of the method</param>
        /// <returns>
        /// The function returns true if the method specified by methodName 
        /// exists in the export table of the module specified by moduleName.
        /// </returns>
        static bool DoesWin32MethodExist(string moduleName, string methodName)
        {
            var moduleHandle = GetModuleHandle(moduleName);
            if (moduleHandle == IntPtr.Zero)
            {
                return false;
            }
            return (GetProcAddress(moduleHandle, methodName) != IntPtr.Zero);
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule,[MarshalAs(UnmanagedType.LPStr)]string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWow64Process(IntPtr hProcess, out bool wow64Process);
    }
}
