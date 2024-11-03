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

namespace SystatusMonitor
{
    /// <summary>
    /// FloatWindow.xaml 的交互逻辑
    /// 环形进度条样式参考：https://www.cnblogs.com/beatfan/p/13624769.html
    /// </summary>
    public partial class FloatWindow : Window
    {
        private class CircleProgress
        {
            public int edgeLen;
            public int strokeThickness;

            public Path progressPath;

            public void SetPath(Path path)
            {
                progressPath = path;
                progressPath.Width = edgeLen;
                progressPath.Height = edgeLen;
                progressPath.StrokeThickness = strokeThickness;
            }
        }

        private CircleProgress cpuProgress;
        private CircleProgress gpuProgress;

        public FloatWindow()
        {
            InitializeComponent();
            SetInitPosition();

            this.MouseLeftButtonDown += new MouseButtonEventHandler(DragWindow);

            cpuProgress = new CircleProgress()
            {
                edgeLen = 100,
                strokeThickness = 10,
            };
            cpuProgress.SetPath(cpuPath);

            gpuProgress = new CircleProgress()
            {
                edgeLen = 70,
                strokeThickness = 10,
            };
            gpuProgress.SetPath(gpuPath);

            double progress = 0;
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += (object sender, EventArgs e) =>
            {
                progress+=0.01;
                SetProgressValue(progress, cpuProgress);
            };
            timer.Start();

            SetProgressValue(0.33, gpuProgress);
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

        /// <summary>
        /// value: [0, 1]
        /// </summary>
        private void SetProgressValue(double value, CircleProgress circleProgress)
        {
            double angle = value * 360;
            //起始点
            double leftStart = circleProgress.edgeLen / 2;
            double topStart = circleProgress.strokeThickness;
            //结束点
            double endLeft = 0;
            double endTop = 0;
            double radius = leftStart - topStart; //环形半径
            bool isLagreCircle = false; //是否优势弧，即大于180度的弧形

            lbValue.Content = (value * 100).ToString("0") + "%";

            //小于90度
            if (angle <= 90) //第一象限
            {
                double ra = (90 - angle) * Math.PI / 180;
                endLeft = leftStart + Math.Cos(ra) * radius;
                endTop = topStart + radius - Math.Sin(ra) * radius;
            }
            else if (angle <= 180) //第二象限
            {
                double ra = (angle - 90) * Math.PI / 180;
                endLeft = leftStart + Math.Cos(ra) * radius;
                endTop = topStart + radius + Math.Sin(ra) * radius;
            }
            else if (angle <= 270) //第三象限
            {
                isLagreCircle = true;
                double ra = (angle - 180) * Math.PI / 180;
                endLeft = leftStart - Math.Sin(ra) * radius;
                endTop = topStart + radius + Math.Cos(ra) * radius;
            }
            else if (angle < 360) //第四象限
            {
                isLagreCircle = true;
                double ra = (angle - 270) * Math.PI / 180;
                endLeft = leftStart - Math.Cos(ra) * radius;
                endTop = topStart + radius - Math.Sin(ra) * radius;
            }
            else
            {
                isLagreCircle = true;
                endLeft = leftStart - 0.001; //不与起点在同一点，避免重叠绘制出非环形
                endTop = topStart;
            }

            Point arcEndPt = new Point(endLeft, endTop);
            Size arcSize = new Size(radius, radius);
            ArcSegment arcsegment = new ArcSegment(arcEndPt, arcSize, 0, isLagreCircle, SweepDirection.Clockwise, true);
            PathSegmentCollection pathsegmentCollection = new PathSegmentCollection
            {
                arcsegment
            };
            PathFigure pathFigure = new PathFigure
            {
                StartPoint = new Point(leftStart, topStart),
                Segments = pathsegmentCollection
            };
            PathFigureCollection pathFigureCollection = new PathFigureCollection
            {
                pathFigure
            };
            PathGeometry pathGeometry = new PathGeometry
            {
                Figures = pathFigureCollection
            };
            circleProgress.progressPath.Data = pathGeometry;
        }
    }
}