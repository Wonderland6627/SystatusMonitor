using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystatusMonitorProject.ViewModels
{
    internal class HardwareInfo
    {
        public double CpuUsage { get; set; }
        public double MemoryUsage { get; set; }
        public long AvailableMemory { get; set; }
        // 其他硬件信息属性...
    }
}
