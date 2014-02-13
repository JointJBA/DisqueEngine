using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Disque.Xna.UI
{
    public class Canvas : Container
    {
        public static void SetTop(UIElement element, float value)
        {
            element.Top = element.Parent.Top + value;
        }
        public static void SetLeft(UIElement element, float value)
        {
            element.Left = element.Parent.Left + value;
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            if (this.Parent == null)
                spriteBatch.Begin();
            foreach (UIElement element in Children)
                element.Draw(spriteBatch);
            if (this.Parent == null)
                spriteBatch.End();
        }
    }
}
