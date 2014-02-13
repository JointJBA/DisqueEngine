using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Disque.Xna.UI
{
    public class UIElement
    {
        public virtual float Height { get; set; }
        public virtual float Width { get; set; }
        internal virtual float Top { get; set; }
        internal virtual float Left { get; set; }
        internal Container Parent { get; set; }
        internal virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
