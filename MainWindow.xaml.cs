using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;

namespace SystatusMonitorProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FloatWindow floatWindow;
        private NotifyIcon notifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            CreateFloatWindow();
            CreateNotifyIcon();
        }

        private void CreateFloatWindow()
        {
            if (floatWindow != null) { return; }
            floatWindow = new FloatWindow();
        }

        private void CreateNotifyIcon()
        {
            if (notifyIcon != null) { return; }
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = System.Drawing.SystemIcons.Application;
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("主页", null, (sender, e) => {
                ShowMainWindow();
            });
            contextMenuStrip.Items.Add("悬浮显示", null, (sender, e) => {
                ShowFloatWindow();
            });
            contextMenuStrip.Items.Add("退出", null, (sender, e) => {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
                System.Windows.Forms.Application.Exit();
            });
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Visible = true;
        }

        private void ShowFloatWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowFloatWindow();
        }

        private void ShowMainWindow()
        {
            if (floatWindow != null)
            {
                floatWindow.Hide();
            }
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        private void ShowFloatWindow()
        {
            floatWindow.Show();
            this.Hide();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
        }
    }
}
