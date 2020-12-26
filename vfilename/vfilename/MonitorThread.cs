using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Diagnostics;

namespace vfilename
{
    static class MonitorThread
    {
        public static FileNameWindow fnw = null;
        public static Thread MonitorThreadObject = null;
        public static IntPtr HandledHwnd = (IntPtr)0;
        public static void MonitorThreadFunction()
        {
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                fnw = new FileNameWindow();
            }));
            while (true)
            {
                if((GetActiveWindowTitle()=="压缩文件名和参数")&&(GetForegroundWindow()!=HandledHwnd))
                {
                    string RarExe = GetRarExe();
                    ConfigTxt.Write("rarexe", RarExe, "");
                    Thread.Sleep(1000);
                    HandledHwnd = GetForegroundWindow();
                    Rect r = new Rect();
                    GetWindowRect(GetForegroundWindow(), ref r);
                    int SpaceBetweenBorders = 15;
                    MoveWindow(GetForegroundWindow(), r.Left, SpaceBetweenBorders, r.Right - r.Left, r.Bottom - r.Top, true);
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {

                        fnw.Topmost = true;
                        fnw.Top = -10000;
                        fnw.Left = -10000;
                        fnw.UserNameTextBox.Text = ConfigTxt.Read("username", "用户名");
                        fnw.InitShow();
                        IntPtr fnwHwnd = (new WindowInteropHelper(fnw)).Handle;

                        MoveWindow(fnwHwnd,
                                   (r.Left + r.Right)/2 - ((r.Right - r.Left) * 3 / 2)/2, 
                                   SpaceBetweenBorders + (r.Bottom - r.Top) + SpaceBetweenBorders,
                                   (r.Right - r.Left) * 3 / 2,
                                   (r.Bottom - r.Top) / 2,
                                   true);

                    }));
                }
                if(!IsWindow(HandledHwnd))
                {
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        fnw.Close();
                    }));
                }
                Thread.Sleep(100);
            }
        }

        public static string GetRarExe()
        {
            var process = Process.GetProcessesByName("wInRaR").First();
            var fileNameBuilder = new StringBuilder(1024);
            uint bufferLength = (uint)fileNameBuilder.Capacity + 1;
            return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ?
                fileNameBuilder.ToString() :
                null;
        }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static string GetActiveWindowTitle()
        {
            const int nChars = 2560;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindow(IntPtr hWnd);

    }
}
