using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace SystatusMonitor
{
    public class MainViewModel : IDisposable
    {
        private double cpuUsage;
        private PerformanceCounter cpuCounter;

        public double CpuUsage => cpuUsage; 

        public MainViewModel()
        {
            cpuCounter = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
        }

        public void Tick()
        {
            cpuUsage = cpuCounter.NextValue();
        }

        public void Dispose()
        {
            
        }
    }
}
