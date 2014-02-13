using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;

namespace Disque.Xna.UI.Input
{
    public delegate void KeyboardEventHandler(object sender, KeyboardEventArgs e);
    public class KeyboardEventArgs : EventArgs
    {
        Keys[] pressedkeys;
        public KeyboardEventArgs(Keys[] pressedkeys)
        {
            this.pressedkeys = pressedkeys;
        }
        public Keys[] PressedKeys
        {
            get
            {
                return pressedkeys;
            }
        }
    }
}
