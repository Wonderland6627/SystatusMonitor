using SystatusMonitor.scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SystatusMonitor
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly HardwareInfoService _hardwareInfoService = new HardwareInfoService();
        private HardwareInfo _hardwareInfo;
        private DispatcherTimer _timer;

        public MainViewModel()
        {
            _hardwareInfo = _hardwareInfoService.GetHardwareInfo();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2); // 设置刷新间隔
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _hardwareInfo = _hardwareInfoService.GetHardwareInfo();
            // 通知属性更改
            OnPropertyChanged(nameof(CpuUsage));
            OnPropertyChanged(nameof(MemoryUsage));
            // ... 其他属性
        }

        public double CpuUsage => _hardwareInfo.CpuUsage;
        public double MemoryUsage => _hardwareInfo.MemoryUsage;
        // ... 其他属性

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
