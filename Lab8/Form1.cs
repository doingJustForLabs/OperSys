using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace Lab8
{
    public partial class Form1 : Form
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private IntPtr _hookID = IntPtr.Zero;
        private delegate IntPtr KeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private readonly KeyboardProc _hookProc;

        private readonly string _soundsDir = "Sounds";
        private WaveOutEvent _mainOut;

        private readonly Dictionary<Keys, string> _keyMappings = new Dictionary<Keys, string>();
        private readonly Dictionary<string, WaveFileReader> _sampleReaders = new Dictionary<string, WaveFileReader>();
        private readonly HashSet<Keys> _pressedKeys = new HashSet<Keys>();

        public Form1()
        {
            InitializeComponent();
            _hookProc = KeyboardHookCallback;
            SetupKeyMappings();
            LoadAllSamples();
            InitializeMainOutput();
            SetKeyboardHook();
        }

        private void SetupKeyMappings()
        {
            _keyMappings.Add(Keys.Q, "kick.wav");
            _keyMappings.Add(Keys.W, "clap.wav");
            _keyMappings.Add(Keys.E, "hihat.wav");
        }

        private void LoadAllSamples()
        {
            foreach (var pair in _keyMappings)
            {
                string path = Path.Combine(_soundsDir, pair.Value);
                if (File.Exists(path))
                {
                    try
                    {
                        _sampleReaders[pair.Value] = new WaveFileReader(path);
                    }
                    catch
                    {
                        MessageBox.Show($"Не получилось загрузить: {pair.Value}");
                    }
                }
            }
        }

        private void InitializeMainOutput()
        {
            string mainLoop = Path.Combine(_soundsDir, "melody.wav");

            if (File.Exists(mainLoop))
            {
                try
                {
                    _mainOut = new WaveOutEvent();
                    var mainReader = new AudioFileReader(mainLoop);
                    _mainOut.Init(mainReader);
                    _mainOut.Play();

                    // По окончании запускаем еще раз
                    _mainOut.PlaybackStopped += (s, e) =>
                    {
                        mainReader.Position = 0;
                        _mainOut.Play();
                    };
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            } else
            {
                MessageBox.Show("Melody not found");
            }
        }

        private void SetKeyboardHook()
        {
            _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, GetModuleHandle(null), 0);
        }

        private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                if (wParam == (IntPtr)WM_KEYDOWN) // Нажатие клавиши
                {
                    // Если клавиша еще не была нажата (нет в множестве)
                    if (!_pressedKeys.Contains(key) && _keyMappings.ContainsKey(key))
                    {
                        _pressedKeys.Add(key); // Добавляем в множество нажатых
                        string soundFile = _keyMappings[key];
                        BeginInvoke((Action)(() => PlaySound(soundFile)));
                    }
                }
                else if (wParam == (IntPtr)WM_KEYUP) // Отпускание клавиши
                {
                    _pressedKeys.Remove(key); // Удаляем из множества нажатых
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void PlaySound(string soundFile)
        {
            if (_sampleReaders.TryGetValue(soundFile, out var reader))
            {
                try
                {
                    reader.Position = 0;
                    var quickOut = new WaveOutEvent();
                    quickOut.Init(reader);
                    quickOut.Play();
                    quickOut.PlaybackStopped += (s, e) =>
                    {
                        quickOut.Dispose();
                    };
                }
                catch
                {
                    // Игнорируем ошибки воспроизведения
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _mainOut?.Stop();
            _mainOut?.Dispose();
            foreach (var reader in _sampleReaders.Values)
            {
                reader?.Dispose();
            }
            UnhookWindowsHookEx(_hookID);
            base.OnFormClosing(e);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}