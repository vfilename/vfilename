using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace vfilename
{
    public static class ChildWindows
    {
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

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
        public static extern bool GetComboBoxInfo(IntPtr hWnd, ref COMBOBOXINFO pcbi);

        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COMBOBOXINFO
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public int stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }

        static IntPtr FoundComboHwnd = (IntPtr)0;
        static IntPtr FoundButtonHwnd = (IntPtr)0;

        

        public static bool EnumChildWindowsCallback(IntPtr hWnd, IntPtr lParam)
        {
            
            StringBuilder className = new StringBuilder(256);
            GetClassName(hWnd, className, className.Capacity);
            if(className.ToString()== "ComboBox")
            {
                if(FoundComboHwnd==(IntPtr)0)
                {
                    //MessageBox.Show("ComboBox");
                    FoundComboHwnd = hWnd;
                }
            }

            String text = WindowMessageClass.GetControlText(hWnd);
            if(text == "确定")
            {
                if(FoundButtonHwnd==(IntPtr)0)
                {
                    //MessageBox.Show("确定");
                    FoundButtonHwnd = hWnd;
                }
            }
            
            return true;
        }

        

        

        public static void Go(String s)
        {

            FoundComboHwnd = (IntPtr)0;
            FoundButtonHwnd = (IntPtr)0;
            EnumChildWindows(MonitorThread.HandledHwnd, EnumChildWindowsCallback, (IntPtr)0);

            COMBOBOXINFO info;
            info = new COMBOBOXINFO();
            info.cbSize = Marshal.SizeOf(info);
            GetComboBoxInfo(FoundComboHwnd, ref info);

            String x = WindowMessageClass.GetControlText(info.hwndEdit);
            x = Path.GetFileNameWithoutExtension(x) + s + Path.GetExtension(x);
            WindowMessageClass.SetControlText(info.hwndEdit, x);

            WindowMessageClass.ClickControl(FoundButtonHwnd);
        }
    }

    public static class WindowMessageClass
    {

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode)]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(int hWnd, int Msg, int wparam, int lparam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;

        public static string GetControlText(IntPtr hWnd)
        {

            // Get the size of the string required to hold the window title (including trailing null.) 
            Int32 titleSize = SendMessage((int)hWnd, WM_GETTEXTLENGTH, 0, 0).ToInt32();

            // If titleSize is 0, there is no title so return an empty string (or null)
            if (titleSize == 0)
                return String.Empty;

            StringBuilder title = new StringBuilder((titleSize + 1)*3); // new StringBuilder(titleSize + 1);

            SendMessage(hWnd, (int)WM_GETTEXT, title.Capacity, title);

            return title.ToString();
        }

        public static void SetControlText(IntPtr hWnd, string s)
        {
            SendMessage(hWnd, WM_SETTEXT, IntPtr.Zero, s);
        } 

        const uint WM_SETTEXT = 0x000C;

        const int BM_CLICK = 0x00F5;
        public static void ClickControl(IntPtr hWnd)
        {
            SendMessage(hWnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
