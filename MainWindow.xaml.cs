using System;
using System.Windows;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Resources;
using System.ComponentModel;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;
using System.Media;

namespace SystatusMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        private Timer updateTimer;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            CreateNotifyIcon();
            CreateFloatWindow();
            CreateTimer();

            //this.Hide();
        }

        public void CreateTimer()
        {
            updateTimer = new Timer();
            updateTimer.Interval = 2000;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (viewModel == null)
            {
                return;
            }

            viewModel.Tick();
            UpdateMainWindow();
            UpdateFloatWindow();
        }

        private void UpdateMainWindow()
        {
            cpuUsageTxt.Text = $"{viewModel.CpuUsage.ToString("f2")}%";
        }

        //protected override void OnStateChanged(EventArgs e)
        //{
        //    base.OnStateChanged(e);
        //    if (WindowState == WindowState.Minimized)
        //    {
        //        this.Hide();
        //    }
        //}

        private void Exit(object sender, EventArgs e)
        {
            updateTimer.Stop();
            floatWindow.Hide();
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            viewModel.Dispose();
            System.Windows.Forms.Application.Exit();
            System.Windows.Application.Current.Shutdown();
        }
    }

    /// <summary>
    /// 处理NotifyIcon相关
    /// </summary>
    partial class MainWindow
    {
        private NotifyIcon notifyIcon;
        private ToolStripMenuItem runnerMenuItem;
        private ToolStripMenuItem startupMenuItem;

        private string runnerName = "cat";
        private string iconResDirPath = "pack://application:,,,/SystatusMonitor;component/resources";
        private Icon[] icons;
        private int currentIconIndex = 0;
        private Timer iconAnimTimer = new Timer();

        private void CreateNotifyIcon()
        {
            runnerMenuItem = new ToolStripMenuItem("Runner", null, new ToolStripMenuItem[]
            {
                new ToolStripMenuItem("Cat", null, SetRunner)
                {
                    Checked = runnerName.Equals("cat"),
                },
                new ToolStripMenuItem("Kunkun", null, SetRunner)
                {
                    Checked = runnerName.Equals("kunkun"),
                },
            });

            ContextMenuStrip contextMenuStrip = new ContextMenuStrip(new Container());
            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                runnerMenuItem,

                new ToolStripSeparator(),

                new ToolStripMenuItem("Exit", null, Exit)
            });

            notifyIcon = new NotifyIcon();
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Visible = true;

            LoadIcons();
            SetIconAimation();
        }

        private void SetRunner(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            UpdateCheckedState(menuItem, runnerMenuItem);
            runnerName = menuItem.Text.ToLower();
            LoadIcons();
        }

        private void UpdateCheckedState(ToolStripMenuItem sender, ToolStripMenuItem menuItem)
        {
            foreach (ToolStripMenuItem item in menuItem.DropDownItems)
            {
                item.Checked = false;
            }
            sender.Checked = true;
        }

        private void LoadIcons()
        {
            int capacity = 5;
            string lowerRunnerName = runnerName.ToLower();
            switch (lowerRunnerName)
            {
                case "cat":
                    capacity = 5; break;
                case "kunkun":
                    capacity = 17; break;
                default:
                    icons = new Icon[1]
                    {
                        SystemIcons.Application,
                    };
                    notifyIcon.Icon = icons[0];
                    return;
            }

            List<Icon> list = new List<Icon>(capacity);
            for (int index = 0; index < capacity; index++)
            {
                string iconPath = $"{iconResDirPath}/{lowerRunnerName}/{lowerRunnerName}_{index}.ico";
                Stream iconStream = System.Windows.Application.GetResourceStream(new Uri(iconPath)).Stream;
                Icon icon = new Icon(iconStream);
                list.Add(icon);
            }
            icons = list.ToArray();
            notifyIcon.Icon = icons[0];
        }

        private void SetIconAimation()
        {
            iconAnimTimer.Interval = 100;
            iconAnimTimer.Tick += new EventHandler(IconAnimationTick);
            iconAnimTimer.Start();
        }

        private void IconAnimationTick(object sender, EventArgs e)
        {
            if (icons.Length <= currentIconIndex)
            {
                currentIconIndex = 0;
            }
            notifyIcon.Icon = icons[currentIconIndex];
            currentIconIndex = (currentIconIndex + 1) % icons.Length;
        }
    }

    /// <summary>
    /// 处理FloatWindow相关
    /// </summary>
    partial class MainWindow
    {
        private FloatWindow floatWindow;

        private void CreateFloatWindow()
        {
            floatWindow = new FloatWindow();
            floatWindow.Show();
        }

        private void UpdateFloatWindow()
        {
            if (viewModel == null)
            {
                return;
            }
            if (floatWindow == null || !floatWindow.IsVisible)
            {
                return;
            }

            floatWindow.UpdateGrid(cpuUsage: viewModel.CpuUsage);
        }
    }
}
