using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace ClassAndDDL
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<string> Lib = new ObservableCollection<string>();
        ObservableCollection<string> Metod = new ObservableCollection<string>();
        public string Assembly { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            lbLib.ItemsSource = Lib;
            lbClassMetog.ItemsSource = Metod;
        }
        private void Load()
        {
            AppDomain domain = AppDomain.CurrentDomain;

            Assembly[] assemblies = domain.GetAssemblies();
            foreach (Assembly asm in assemblies)
            Metod.Add(asm.GetName().Name);
                
            
        }

        private void btnFBD_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                
                DirectoryInfo d = new DirectoryInfo(dialog.SelectedPath);
                FileInfo[] Files = d.GetFiles("*.cs");
                foreach (var item in Files)
                    Lib.Add(item.Name);

            }
        }

        private void lbLib_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load();
        }
    }
}

