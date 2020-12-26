using System;
using System.Collections.Generic;
using System.IO;
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

namespace vfilename
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();
            }
            base.OnStateChanged(e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try { MonitorThread.MonitorThreadObject.Abort(); } catch {;}
            Application.Current.Shutdown();
        }
        ListWindow lw;
        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if(files.Length!=1)
                {
                    return;
                }
                if(!Directory.Exists(files[0]))
                {
                    return;
                }
                WindowState = WindowState.Minimized;
                lw = new ListWindow();
                lw.Topmost = true;
                lw.Left = 0;
                lw.Top = 0;
                lw.Width = 600;
                lw.Height = 600;                
                lw.WindowState = WindowState.Maximized;
                lw.Title = files[0];
                lw.InitList(files[0]);
                lw.Show();
            }
        }
    }

    public class ShowMessageCommand : ICommand
    {
        public void Execute(object parameter)
        {
            (App.Current.MainWindow as MainWindow).Topmost = true;
            (App.Current.MainWindow as MainWindow).Show();
            (App.Current.MainWindow as MainWindow).WindowState = System.Windows.WindowState.Normal;
            (App.Current.MainWindow as MainWindow).Focus();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}
