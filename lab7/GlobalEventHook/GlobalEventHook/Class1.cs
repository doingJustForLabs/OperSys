using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Threading;

namespace GlobalEventHook
{
    public class SystemEventHook : IDisposable
    {
        // Делегат для callback-функции
        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public enum HookType
        {
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14,
        }

        public event EventHandler<HookEventArgs> HookTriggered;

        private IntPtr _hookId = IntPtr.Zero;
        private HookProc _hookProc;
        private bool _disposed;

        private const int CornerSize = 50;
        private bool _isInCorner = false;

        //public HookType CurrentHookType { get; private set; }

        public bool IsActive
        {
            get { return _hookId != IntPtr.Zero; }
        }

        public SystemEventHook()
        {
            _hookProc = HookCallback;
        }

        public void StartHook(HookType hookType)
        {
            if (IsActive)
                throw new InvalidOperationException("Хук уже активен");

            //CurrentHookType = hookType;

            using (var process = Process.GetCurrentProcess())
            using (var module = process.MainModule)
            {
                _hookId = SetWindowsHookEx(
                    (int)hookType, 
                    _hookProc,
                    GetModuleHandle(module.ModuleName), 
                    0
                );
            }

            if (_hookId == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(),
                    $"Ошибка создания хука {hookType}. Ошибка: {Marshal.GetLastWin32Error()}");
            }
        }

        //public void StopHook()
        //{
        //    if (!IsActive) return;

        //    if (!UnhookWindowsHookEx(_hookId))
        //    {
        //        throw new Win32Exception(Marshal.GetLastWin32Error(),
        //            $"Ошибка при удалении хука. Ошибка: {Marshal.GetLastWin32Error()}");
        //    }

        //    _hookId = IntPtr.Zero;
        //}

        // Callback-функция
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && HookTriggered != null)
            {
                var args = new HookEventArgs(wParam, lParam);

                if (args.MousePosition.HasValue)
                {
                    bool nowInCorner = IsInTopRightCorner(args.MousePosition.Value);

                    if (nowInCorner && !_isInCorner)
                    {
                        Task.Run(() =>
                        {
                            MinimizeAllWindows();
                        });
                    }

                    _isInCorner = nowInCorner;
                }

                HookTriggered?.Invoke(this, args);
                if (args.Handled)
                    return (IntPtr)1;
            }
            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            if (_disposed) return;

            if (_hookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookId);
                _hookId = IntPtr.Zero;
            }
            _disposed = true;
        }

        private void MinimizeAllWindows()
        {
            // нажатие Win+D
            const byte WIN_KEY = 0x5B;
            const byte D_KEY = 0x44;
            const uint KEYEVENTF_KEYUP = 0x2;

            Thread.Sleep(1000);

            keybd_event(WIN_KEY, 0, 0, 0);
            keybd_event(D_KEY, 0, 0, 0);
            keybd_event(D_KEY, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(WIN_KEY, 0, KEYEVENTF_KEYUP, 0);
        }

        private bool IsInTopRightCorner(Point position)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            return position.X >= screenWidth && position.Y <= CornerSize;
        }
    }

    // Класс для передачи данных о событии
    public class HookEventArgs : EventArgs
    {
        public IntPtr wParam { get; }
        public IntPtr lParam { get; }
        public bool Handled { get; set; }
        public Keys? KeyCode { get; }
        public bool? IsKeyDown { get; }
        public Point? MousePosition { get; }
        public string MouseAction { get; }

        public HookEventArgs(IntPtr wParam, IntPtr lParam)
        {
            this.wParam = wParam;
            this.lParam = lParam;

            int msg = wParam.ToInt32();

            if (msg == 0x0100 || msg == 0x0101) // WM_KEYDOWN или WM_KEYUP
            {
                var kbStruct = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
                KeyCode = (Keys)kbStruct.vkCode;
                IsKeyDown = msg == 0x0100;
            }
            else
            {
                var mouseStruct = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
                MousePosition = mouseStruct.pt;
                MouseAction = GetMouseActionName(msg);
            }
        }

        private string GetMouseActionName(int message)
        {
            switch (message)
            {
                case 0x0200: return "Перемещение";
                case 0x0201: return "Левая кнопка вниз";
                case 0x0202: return "Левая кнопка вверх";
                case 0x0204: return "Правая кнопка вниз";
                case 0x0205: return "Правая кнопка вверх";
                case 0x0207: return "Средняя кнопка вниз";
                case 0x0208: return "Средняя кнопка вверх";
                case 0x020A: return "Колесо прокрутки";
                default: return $"Действие (код: 0x{message:X4})";
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
        public Point pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KBDLLHOOKSTRUCT
    {
        public uint vkCode;
        public uint scanCode;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
}
