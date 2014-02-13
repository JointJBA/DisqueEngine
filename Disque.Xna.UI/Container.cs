using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Disque.Xna.UI
{
    public class Container : UIElement
    {
        UIElementCollection collection = new UIElementCollection();
        public UIElementCollection Children
        {
            get
            {
                return collection;
            }
            set
            {
                collection = value;
            }
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
