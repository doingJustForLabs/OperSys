using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace lab3
{
    public static class WinAPI
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CopyFile(
            string lpExistingFileName,
            string lpNewFileName,
            bool bFailIfExists
            );


        // Константы для SHFileOperation
        public const uint FO_DELETE = 0x0003;
        public const uint FO_MOVE = 0x0001; // Операция перемещения
        public const ushort FOF_ALLOWUNDO = 0x0040; // Разрешить отмену (восстановление)
        public const ushort FOF_NOCONFIRMATION = 0x0010; // Не запрашивать подтверждение

        // Структура для SHFileOperation
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            public uint wFunc;
            public string pFrom;
            public string pTo;
            public ushort fFlags;
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        // Импорт функции SHFileOperation
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
    }
}
