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
                        /*fnw.Top = 0 + (r.Bottom - r.Top);
                        fnw.Left = r.Left;
                        fnw.Width = r.Right - r.Left;
                        fnw.Height = (r.Bottom - r.Top)/2;*/
                        
                        fnw.Topmost = true;
                        fnw.Top = -10000;
                        fnw.Left = -10000;
                        fnw.UserNameTextBox.Text = ConfigTxt.Read("username", "用户名");
                        fnw.InitShow();
                        IntPtr fnwHwnd = (new WindowInteropHelper(fnw)).Handle;

                        /*
                        int rarTopBorder = 0;
                        int rarBottomBorder = 0;
                        int rarLeftBorder = 0;
                        int rarRightBorder = 0;
                        int fnwTopBorder = 0;
                        int fnwBottomBorder = 0;
                        int fnwLeftBorder = 0;
                        int fnwRightBorder = 0;
                        RECT fnwR1 = GetWindowRectangle(fnwHwnd);
                        Rect fnwR2 = new Rect();
                        GetWindowRect(fnwHwnd, ref fnwR2);
                        RECT rarR1 = GetWindowRectangle(HandledHwnd);
                        Rect rarR2 = new Rect();
                        GetWindowRect(HandledHwnd, ref rarR2);
                        if ((Math.Abs(fnwR1.Right) + Math.Abs(fnwR1.Left) >= 100) && (Math.Abs(rarR1.Right) + Math.Abs(rarR1.Left) >= 100))
                        {
                            rarTopBorder = Math.Abs(((int)rarR2.Top) - ((int)rarR1.Top));
                            rarBottomBorder = Math.Abs(((int)rarR2.Bottom) - ((int)rarR1.Bottom));
                            rarLeftBorder = Math.Abs(((int)rarR2.Left) - ((int)rarR1.Left));
                            rarRightBorder = Math.Abs(((int)rarR2.Right) - ((int)rarR1.Right));
                            fnwTopBorder = Math.Abs(((int)fnwR2.Top) - ((int)fnwR1.Top));
                            fnwBottomBorder = Math.Abs(((int)fnwR2.Bottom) - ((int)fnwR1.Bottom));
                            fnwLeftBorder = Math.Abs(((int)fnwR2.Left) - ((int)fnwR1.Left));
                            fnwRightBorder = Math.Abs(((int)fnwR2.Right) - ((int)fnwR1.Right));
                            MoveWindow(...);
                        }
                        else
                        {
                            rarTopBorder = 0;
                            rarBottomBorder = 0;
                            rarLeftBorder = 0;
                            rarRightBorder = 0;
                            fnwTopBorder = 0;
                            fnwBottomBorder = 0;
                            fnwLeftBorder = 0;
                            fnwRightBorder = 0;
                            MoveWindow(...);
                        }
                        */

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

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);

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

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
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

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        
        /*
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        [Flags]
        private enum DwmWindowAttribute : uint
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_LAST
        }

        public static RECT GetWindowRectangle(IntPtr hWnd)
        {
            RECT rect = new RECT();
            int size = Marshal.SizeOf(typeof(RECT));
            DwmGetWindowAttribute(hWnd, (int)DwmWindowAttribute.DWMWA_EXTENDED_FRAME_BOUNDS, out rect, size);

            return rect;
        }
        */
        
        /*
        [DllImport(@"dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out Rect pvAttribute, int cbAttribute);

        public static bool GetWindowActualRect(IntPtr handle, out Rect rect)
        {
            const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;
            int result = DwmGetWindowAttribute(handle, DWMWA_EXTENDED_FRAME_BOUNDS, out rect, Marshal.SizeOf(typeof(Rect)));

            return result >= 0;
        }
        */
    }
}
