using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Some_Task
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> filesDir = new ObservableCollection<string>();
        ObservableCollection<string> filesProc = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            DirectoryInfo directory = new DirectoryInfo(@"D:\User\Desktop\STEP\SystemProgram\Some Task\Some Task\bin\Debug");
            lbDebugList.ItemsSource = filesDir;
            lbProcesList.ItemsSource = filesProc;
            foreach (var item in directory.GetFiles())
                if (item.Extension == ".exe")
                    filesDir.Add(item.Name);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = lbDebugList.SelectedItem.ToString();
                Process proc = new Process();
                proc.StartInfo = startInfo;
                proc.Start();

                foreach (var item in Process.GetProcesses())
                    if (item.ProcessName == proc.ProcessName)
                    {
                        filesProc.Add(item.ProcessName);
                        filesDir.Remove(startInfo.FileName);
                        break;
                    }
            }
            catch
            {

            }

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in Process.GetProcesses())
                    if (item.ProcessName == lbProcesList.SelectedItem.ToString())
                    {
                        filesProc.Remove(item.ProcessName);
                        filesDir.Add(item.ProcessName + ".exe");
                        item.Kill();
                        break;
                    }
            }
            catch
            {

            }
        }
    }
}
