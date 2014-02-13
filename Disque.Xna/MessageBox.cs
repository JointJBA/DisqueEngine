using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Disque.Xna
{
    public static class MessageBox
    {
        public static void Show(string message)
        {
            Native.MessageBox(new IntPtr(0), message, "Alert", 0);
        }
    }
    public static class Native
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);
    }
}
