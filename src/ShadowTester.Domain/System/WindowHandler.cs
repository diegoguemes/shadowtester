using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ShadowTester.Domain.System
{
    public class WindowHandler
    {
        public string GetForegroundProcess()
        {
            uint processId;
            IntPtr hWnd = User32.GetForegroundWindow();
            if (User32.GetWindowThreadProcessId(hWnd, out processId) != 0)
            {
                try
                {
                    Process process = Process.GetProcessById((int)processId);
                    return process.ProcessName;
                }
                catch (SystemException)
                {
                }
            }
            throw new ForegroundWindowNotFoundException();
        }

        public Rectangle GetForegroundWindowRectangle()
        {
            IntPtr hWnd = User32.GetForegroundWindow();
            RECT rectangle = new RECT();
            if (!User32.GetWindowRect(hWnd, ref rectangle))
            {
                throw new ForegroundWindowNotFoundException();
            }
            return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }

        public Size GetForegroundWindowSize()
        {
            return GetForegroundWindowRectangle().Size;
        }

        public Point GetForegroundWindowPosition()
        {
            Rectangle rectangle = GetForegroundWindowRectangle();
            return new Point(rectangle.Left, rectangle.Top);
        }

        private class User32
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();
            [DllImport("user32.dll")]
            public static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
            [DllImport("user32.dll")]
            public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rectangle);
        }

        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}