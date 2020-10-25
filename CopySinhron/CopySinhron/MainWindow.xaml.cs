
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CopySinhron
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnFirstCatalog_Click(object sender, RoutedEventArgs e)
        {
            pbLoad.Value = 0;
            OpenDialog(lbFirstDir);
        }

        private void btnFinishCatalog_Click(object sender, RoutedEventArgs e) => OpenDialog(lbSecondDir);

        private void OpenDialog(TextBlock lb)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == true)
                lb.Text = fd.FileName.Substring(0,fd.FileName.LastIndexOf("\\")).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(CopyProces));
            Thread t2 = new Thread(new ParameterizedThreadStart(PbLoad));
            string[] catalogs = { lbFirstDir.Text.ToString(), lbSecondDir.Text.ToString() };
            DirectoryInfo directoryInfo = new DirectoryInfo(catalogs[0]);
            long size = DirSize(directoryInfo) / 100000;
            t1.Start(catalogs);
            t2.Start(size);
        }

        private void PbLoad(object size)
        {
            long tmpSize = Convert.ToInt32(size);
            Dispatcher.Invoke(() => { pbLoad.Minimum = 0; });
            Dispatcher.Invoke(() => { pbLoad.Maximum = tmpSize; });
            for (int i = 0; i <= tmpSize; i++)
                Dispatcher.Invoke(() => { pbLoad.Value = i; });
        }

        public static long DirSize(DirectoryInfo d)
        {
            long Size = 0;
            DirectoryInfo[] dis = d.GetDirectories();

            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
                Size += fi.Length;

            foreach (DirectoryInfo di in dis)
                Size += DirSize(di);

            return (Size);
        }


        private static void CopyProces(object catalogs)
        {
            string[] catalogsTMP = catalogs as string[];
            string start = catalogsTMP[0];
            string end = catalogsTMP[1];
            DirectoryInfo dir_inf = new DirectoryInfo(start);

            foreach (DirectoryInfo dir in dir_inf.GetDirectories())
            {
                if (Directory.Exists(end + @"\" + dir.Name) != true)
                    Directory.CreateDirectory(end + @"\" + dir.Name);

                string[] catalogsNext = { dir.FullName, end + @"\" + dir.Name };
                CopyProces(catalogsNext);
            }

            foreach (string file in Directory.GetFiles(start))
            {
                string filik = file.Substring(file.LastIndexOf(@"\"), file.Length - file.LastIndexOf(@"\"));
                File.Copy(file, end + @"\" + filik, true);
            }
        }
    }
}
