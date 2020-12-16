using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace vfilename
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SetProcessDPIAware();

            if (FindWindow(null, "V File Name") != ((IntPtr)0))
            {
                MessageBox.Show("这个应用程序已经在运行了。");
                Application.Current.Shutdown();
            }
            else
            {
                MainWindow wnd = new MainWindow();
                wnd.Topmost = true;
                wnd.Show();
                MonitorThread.MonitorThreadObject = new Thread(MonitorThread.MonitorThreadFunction);
                MonitorThread.MonitorThreadObject.Start();
            }
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }

    
}
