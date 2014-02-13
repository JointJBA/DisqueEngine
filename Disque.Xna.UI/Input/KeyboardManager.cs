using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Disque.Xna.UI.Input
{
    public static class KeyboardManager
    {
        public static event KeyboardEventHandler KeysDown;
        public static event KeyboardEventHandler KeysUp;
        static bool prevstate = false;
        static bool down = false;
        static Keys[] prevkeys;
        public static void Update()
        {
            prevstate = down;
            KeyboardState ks = Keyboard.GetState();
            Keys[] keys = ks.GetPressedKeys();
            if (!down)
            {
                if (keys.Length > 0)
                {
                    down = true;
                    prevkeys = keys;
                    if (KeysDown != null)
                        KeysDown(null, new KeyboardEventArgs(keys));
                }
            }
            if (keys.Length < 1)
            {
                down = false;
            }
            if (prevstate && !down)
            {
                if (KeysUp != null)
                    KeysUp(null, new KeyboardEventArgs(prevkeys));
            }
        }
    }
}
