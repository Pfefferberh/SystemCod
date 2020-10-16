using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace CopyFile
{
    public partial class MainWindow : Window
    {
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void From_Click(object sender, RoutedEventArgs e)
        {
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbFrom.Text = dialog.SelectedPath;
        }

        private void Where_Click(object sender, RoutedEventArgs e)
        {
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbWhere.Text = dialog.SelectedPath;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            string p1 = tbFrom.Text;
            string p2 = tbWhere.Text;
            Task tas = new Task(() => Open(p1, p2));

            tas.Start();
        }
        private void Open(string p1, string p2)
        {

            string start = p1;
            string end = p2;
            DirectoryInfo dir_inf = new DirectoryInfo(start);

            foreach (DirectoryInfo dir in dir_inf.GetDirectories())
            {
                if (Directory.Exists(end + @"\" + dir.Name) != true)
                    Directory.CreateDirectory(end + @"\" + dir.Name);
                Dispatcher.Invoke(() => { prBar.Maximum = dir.GetFiles().Length; });
                Dispatcher.Invoke(() => { prBar.Value++; });
                Open(dir.FullName, end + @"\" + dir.Name);
            }

            foreach (string file in Directory.GetFiles(start))
            {
                string filik = file.Substring(file.LastIndexOf(@"\"), file.Length - file.LastIndexOf(@"\"));
                File.Copy(file, end + @"\" + filik, true);
            }
        }
    }
}
