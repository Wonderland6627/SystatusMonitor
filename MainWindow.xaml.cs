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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SystatusMonitorProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the button's content
            string buttonContent = (sender as Button)?.Content.ToString();

            // Clear the content panel
            contentPanel.Children.Clear();

            // Display the selected content
            switch (buttonContent)
            {
                case "General":
                    DisplayGeneralInfo();
                    break;
                case "Monitoring":
                    DisplayMonitoringInfo();
                    break;
                case "Memory Performance":
                    DisplayMemoryPerformance();
                    break;
                case "Storage Capacity":
                    DisplayStorageCapacity();
                    break;
                case "Battery State":
                    DisplayBatteryState();
                    break;
                case "Network Connection":
                    DisplayNetworkConnection();
                    break;
                case "Experimental feature":
                    DisplayExperimentalFeature();
                    break;
                case "System Info Bar(β)":
                    DisplaySystemInfoBar();
                    break;
            }
        }

        private void DisplayGeneralInfo()
        {
            // Add general information to the content panel
            contentPanel.Children.Add(new TextBlock { Text = "General Information", FontSize = 24, Margin = new Thickness(20) });
        }

        private void DisplayMonitoringInfo()
        {
            // Add monitoring information to the content panel
            contentPanel.Children.Add(new TextBlock { Text = "Monitoring Information", FontSize = 24, Margin = new Thickness(20) });
        }

        private void DisplayMemoryPerformance()
        {
            // Add memory performance information to the content panel
            contentPanel.Children.Add(new TextBlock { Text = "Memory Performance", FontSize = 24, Margin = new Thickness(20) });
        }

        private void DisplayStorageCapacity()
        {
            // Add storage capacity information to the content panel
            contentPanel.Children.Add(new TextBlock { Text = "Storage Capacity", FontSize = 24, Margin = new Thickness(20) });
        }

        private void DisplayBatteryState()
        {
            // Add battery state information to the content panel
            contentPanel.Children.Add(new TextBlock { Text = "Battery State", FontSize = 24, Margin = new Thickness(20) });
        }

        private void DisplayNetworkConnection()
        {
            // Add network connection information to the content panel
            contentPanel.Children.Add(new TextBlock { Text = "Network Connection", FontSize = 24, Margin = new Thickness(20) });
        }

        private void DisplayExperimentalFeature()
        {
            // Add experimental feature information to the content panel
            contentPanel.Children.Add(new TextBlock { Text = "Experimental Feature", FontSize = 24, Margin = new Thickness(20) });
        }

        private void DisplaySystemInfoBar()
        {
            // Add system info bar information to the content panel
            contentPanel.Children.Add(new TextBlock { Text = "System Info Bar(β)", FontSize = 24, Margin = new Thickness(20) });
        }
    }
}
