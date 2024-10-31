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
        private Point clickPosition;
        private Point mousePosition;

        public FloatWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += new MouseButtonEventHandler(DragWindow);
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            clickPosition = e.GetPosition(this);
            this.DragMove();
            mousePosition = this.PointFromScreen(new Point(0, 0));
        }
    }
}
