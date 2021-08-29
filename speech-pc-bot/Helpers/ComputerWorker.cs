using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace speech_pc_bot.Helpers
{
    public enum Keys
    {
        Mute = 173,
        VolumeUp = 175,
        VolumeDown = 174
    }
    public static class ComputerWorker
    {
        [DllImport("user32")]
        public static extern void LockWorkStation();
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        // public static void Mute()
        // {
        //     keybd_event((byte)Keys.Mute, 0, 0, 0);
        // }

        // public static void DecreaseSound()
        // {
        //     keybd_event((byte)Keys.VolumeDown, 0, 0, 0);
        // }
        
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetConsoleWindow();
        public static void Mute()
        {
            SendMessageW(GetConsoleWindow(), WM_APPCOMMAND, GetConsoleWindow(),
                (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        public static void VolDown()
        {
            SendMessageW(GetConsoleWindow(), WM_APPCOMMAND, GetConsoleWindow(),
                (IntPtr)APPCOMMAND_VOLUME_DOWN);
        }

        public static void VolUp()
        {
            SendMessageW(GetConsoleWindow(), WM_APPCOMMAND, GetConsoleWindow(),
                (IntPtr)APPCOMMAND_VOLUME_UP);
        }

        public static void OpenBrowser()
        {
            System.Diagnostics.Process.Start("explorer.exe","https://google.com");
        }
        
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        const int WM_COMMAND = 0x111;
        const int MIN_ALL = 419;
        const int MIN_ALL_UNDO = 416;

        public static void HideWindows() {
            IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero); 
            System.Threading.Thread.Sleep(2000);
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL_UNDO, IntPtr.Zero);
        }
    }
}