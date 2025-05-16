//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace lab7_main
//{
//    public static class HookEventParser
//    {
//        public static string ParseEvent(SystemEventHook.HookType hookType, IntPtr wParam, IntPtr lParam)
//        {
//            return hookType switch
//            {
//                SystemEventHook.HookType.WH_KEYBOARD_LL => ParseKeyboardEvent(wParam, lParam),
//                SystemEventHook.HookType.WH_MOUSE_LL => ParseMouseEvent(wParam, lParam),
//                _ => $"System hook event: {hookType} (wParam: {wParam}, lParam: {lParam})"
//            };
//        }

//        private static string ParseKeyboardEvent(IntPtr wParam, IntPtr lParam)
//        {
//            int vkCode = Marshal.ReadInt32(lParam);
//            bool isKeyDown = wParam == (IntPtr)0x0100; // WM_KEYDOWN
//            string keyName = ((Keys)vkCode).ToString();

//            return $"Keyboard: {(isKeyDown ? "Pressed" : "Released")} {keyName} (0x{vkCode:X})";
//        }

//        private static string ParseMouseEvent(IntPtr wParam, IntPtr lParam)
//        {
//            var mouseData = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
//            string action = GetMouseActionName(wParam.ToInt32());

//            return $"Mouse: {action} at ({mouseData.pt.X}, {mouseData.pt.Y})";
//        }

//        private static string GetMouseActionName(int message)
//        {
//            return message switch
//            {
//                0x0200 => "Move",
//                0x0201 => "Left button down",
//                0x0202 => "Left button up",
//                0x0204 => "Right button down",
//                0x0205 => "Right button up",
//                0x0207 => "Middle button down",
//                0x0208 => "Middle button up",
//                0x020A => "Mouse wheel",
//                0x020B => "Horizontal wheel",
//                _ => $"Unknown action (0x{message:X4})"
//            };
//        }

//        [StructLayout(LayoutKind.Sequential)]
//        private struct MSLLHOOKSTRUCT
//        {
//            public Point pt;
//            public uint mouseData;
//            public uint flags;
//            public uint time;
//            public IntPtr dwExtraInfo;
//        }
//    }
//}
