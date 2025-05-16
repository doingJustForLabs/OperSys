using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class WindowInfo
    {
        public IntPtr Handle { get; set; }
        public string Title { get; set; }

        public bool IsVisible => WindowsAPI.IsWindowVisible(Handle);
        public string ClassName => WindowsAPI.GetClassName(Handle);
        public int ProcessId => WindowsAPI.GetProcessId(Handle);
        public string ProcessPath => WindowsAPI.GetProcessPath(ProcessId);
        public string ProcessName => Path.GetFileName(ProcessPath);

        public (int X, int Y, int Width, int Height) PositionAndSize =>
            WindowsAPI.GetWindowPositionAndSize(Handle);

        public override string ToString() => Title;

        public string GetFullInfo()
        {
            var pos = PositionAndSize;
            return $"Заголовок: {Title}\n" +
               $"Класс окна: {ClassName}\n" +
               $"Видимость: {(IsVisible ? "Видимо" : "Скрыто")}\n" +
               $"PID: {ProcessId}\n" +
               $"Процесс: {ProcessName}\n" +
               $"Путь: {ProcessPath}\n" +
               $"Положение: X={pos.X}, Y={pos.Y}\n" +
               $"Размер: {pos.Width}x{pos.Height}";
        }
    }

    //public class DetailedWindowInfo
    //{
    //    public bool IsVisible { get; set; }
    //    public string ClassName { get; set; }
    //    public int ProcessId { get; set; }
    //    public string ProcessPath { get; set; }
    //    public string ProcessName { get; set; }
    //}
}
