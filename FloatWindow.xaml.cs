using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SystatusMonitorProject
{
    /// <summary>
    /// FloatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FloatWindow : Window
    {
        public FloatWindow()
        {
            InitializeComponent();
            SetInitPosition();

            this.MouseLeftButtonDown += new MouseButtonEventHandler(DragWindow);
        }

        private void SetInitPosition()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            this.Left = screenWidth - this.Width - 24;
            this.Top = screenHeight - this.Height - 48 - 24;
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
