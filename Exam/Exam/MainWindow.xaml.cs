using Microsoft.Win32;
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

namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RegistryKey key = Registry.CurrentUser;


        public MainWindow()
        {
            InitializeComponent();
            foreach (var k in key.GetSubKeyNames())
            {
                TreeViewItem item = new TreeViewItem();
                item.Tag = k;
                item.Header = k;
                item.Items.Add("*");
                tvRegistry.Items.Add(item);
            }
        }
        string tmpKey = " ";
        RegistryKey dir;
        private void tvRegistry_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.OriginalSource;
            item.Items.Clear();

            if (key.GetSubKeyNames().Contains(item.Tag.ToString()))
            {
                dir = key.OpenSubKey(item.Tag.ToString(), true);
                tmpKey = item.Tag.ToString();
                lvRegistry.Items.Add(key.Name);

            }
            else
            {
                RegistryKey drive = key.OpenSubKey(tmpKey, true);
                dir = drive.OpenSubKey(item.Tag.ToString(), true);
                lvRegistry.Items.Add(drive.Name);

                tmpKey = " ";
            }
            try
            {
                foreach (var subDir in dir.GetSubKeyNames())
                {
                    TreeViewItem newItem = new TreeViewItem();
                    newItem.Tag = subDir;
                    newItem.Header = subDir.ToString();
                    newItem.Items.Add("*");
                    item.Items.Add(newItem);
                }
            }
            catch
            { }
        }

        private void tvRegistry_Selected(object sender, RoutedEventArgs e)
        {


        }
    }
}
