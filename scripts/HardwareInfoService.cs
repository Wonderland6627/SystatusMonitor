using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystatusMonitor.scripts
{
    internal class HardwareInfoService
    {
        public HardwareInfo GetHardwareInfo()
        {
            var hardwareInfo = new HardwareInfo();

            // 获取CPU使用率
            var cpuInfo = new ManagementObjectSearcher("SELECT LoadPercentage FROM Win32_Processor").Get();
            foreach (var item in cpuInfo)
            {
                hardwareInfo.CpuUsage = (ushort)item["LoadPercentage"];
            }

            // 获取内存使用情况
            //var memInfo = new ManagementObjectSearcher("SELECT AvailableBytes FROM Win32_OperatingSystem").Get();
            //foreach (var item in memInfo)
            //{
            //    hardwareInfo.AvailableMemory = (long)item["AvailableBytes"];
            //    // 计算已使用内存
            //    hardwareInfo.MemoryUsage = 100 - (hardwareInfo.AvailableMemory / (1024.0 * 1024.0 * 1024.0) /
            //                                   (ulong)item["TotalVisibleMemorySize"] * 100);
            //}

            return hardwareInfo;
        }
    }
}
