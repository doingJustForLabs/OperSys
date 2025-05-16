using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab5
{
    public class WindowsAPI
    {
        [DllImport("user32.dll")]
        static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        static extern bool SetWindowText(IntPtr hWnd, string text);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);


        private static readonly ConcurrentDictionary<IntPtr, string> _windowCache = new ConcurrentDictionary<IntPtr, string>();
        private static readonly Timer _trackerTimer;
        public static event Action<IntPtr, string> OnTitleChanged;

        static WindowsAPI()
        {
            _trackerTimer = new Timer(CheckForChanges, null, 0, 300);
        }

        public static List<WindowInfo> GetAllWindows()
        {
            var windows = new List<WindowInfo>();
            EnumWindows((hWnd, lParam) =>
            {
                var title = GetWindowTitle(hWnd);
                _windowCache[hWnd] = title;

                windows.Add(new WindowInfo
                {
                    Handle = hWnd,
                    Title = title.ToString()
                });

                return true;
            }, IntPtr.Zero);

            return windows;
        }

        private static void CheckForChanges(object state)
        {
            foreach (var window in _windowCache)
            {
                var newTitle = GetWindowTitle(window.Key);
                if (newTitle != window.Value)
                {
                    _windowCache[window.Key] = newTitle;
                    OnTitleChanged?.Invoke(window.Key, newTitle);
                }
            }
        }

        public static void SetWindowPositionOrSize(IntPtr window, IntPtr hWndInsertAfter, int x, int y, int width, int height, uint uFlag)
        {
            SetWindowPos(window, IntPtr.Zero, x, y, width, height, uFlag);
        }

        public static void SetWindowTitle(IntPtr hWnd, string newTitle)
        {
            SetWindowText(hWnd, newTitle);
        }

        private static string GetWindowTitle(IntPtr hWnd)
        {
            var sb = new StringBuilder(255);
            return GetWindowText(hWnd, sb, 255) > 0 ? sb.ToString() : string.Empty;
        }

        public static void RefreshAllWindows()
        {
            EnumWindows(new EnumWindowsProc((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    var title = GetWindowTitle(hWnd);
                    _windowCache.AddOrUpdate(hWnd, title, (k, v) => title);
                }
                return true;
            }), IntPtr.Zero);
        }

        public static string GetClassName(IntPtr hWnd)
        {
            var sb = new StringBuilder(256);
            GetClassName(hWnd, sb, 256);
            return sb.ToString();
        }

        public static int GetProcessId(IntPtr hWnd)
        {
            GetWindowThreadProcessId(hWnd, out int processId);
            return processId;
        }

        public static string GetProcessPath(int processId)
        {
            try
            {
                var process = Process.GetProcessById(processId);
                return process.MainModule?.FileName ?? "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        public static (int X, int Y, int Width, int Height) GetWindowPositionAndSize(IntPtr hWnd)
        {
            GetWindowRect(hWnd, out RECT rect);
            return (rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left; public int Top; public int Right; public int Bottom;
    }
}
