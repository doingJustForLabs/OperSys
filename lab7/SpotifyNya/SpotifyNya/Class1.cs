using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace SpotifyNya
{
    public class SpotifyControl : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private const byte VK_VOLUME_UP = 0xAF;
        private const byte VK_VOLUME_DOWN = 0xAE;
        private const byte VK_MEDIA_PLAY_PAUSE = 0x20;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hmod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hwnd);

        private IntPtr _hookId = IntPtr.Zero;
        private readonly LowLevelKeyboardProc _proc;
        private readonly Keys _hotkey;

        public SpotifyControl(Keys hotkey)
        {
            _hotkey = hotkey;
            _proc = HookCallBack;
        }

        public void Start()
        {
            _hookId = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(null), 0);
        }

        private IntPtr HookCallBack(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                var key = (Keys)Marshal.ReadInt32(lParam);
                if (key == _hotkey)
                {
                    Task.Run(() =>
                    {
                        OpenSpotify();
                        //return CallNextHookEx(_hookId, nCode, wParam, lParam);
                    });

                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public void OpenSpotify()
        {
            try
            {
                var spotifyProcess = Process.Start(new ProcessStartInfo
                {
                    FileName = "spotify:",
                    UseShellExecute = true
                });

                if (spotifyProcess != null)
                {
                    SetForegroundWindow(spotifyProcess.MainWindowHandle);
                    Thread.Sleep(4500);

                    SmartVolumeControl();
                    Thread.Sleep(100);

                    PressMediaKey(VK_MEDIA_PLAY_PAUSE);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void PressMediaKey(byte keyCode)
        {
            keybd_event(keyCode, 0, 0, 0); // Нажатие
            keybd_event(keyCode, 0, 0x0002, 0); // Отпускание
        }

        //public static void SetVolume()
        //{
        //    for (int i = 0; i < 2; i++)
        //    {
        //        keybd_event(VK_VOLUME_UP, 0, 0, 0);
        //        keybd_event(VK_VOLUME_UP, 0, 0x0002, 0); // KEYEVENTF_KEYUP
        //        keybd_event(VK_VOLUME_DOWN, 0, 0, 0);
        //        keybd_event(VK_VOLUME_DOWN, 0, 0x0002, 0);
        //        Thread.Sleep(10);
        //    }
        //}

        public void Dispose()
        {
            if(_hookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookId);
            }
        }

        private void SmartVolumeControl()
        {
            var device = new MMDeviceEnumerator()
                .GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            if (device.AudioEndpointVolume.Mute)
            {
                device.AudioEndpointVolume.Mute = false;
                device.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
            }
            else if (device.AudioEndpointVolume.MasterVolumeLevelScalar < 0.4f)
            {
                device.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
            }
        }
    }
}
