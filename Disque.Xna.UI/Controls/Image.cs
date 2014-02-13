using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Disque.Xna.UI.Controls
{
    public class Image : UIElement
    {
        Texture2D texture;
        public Image(Texture2D text)
        {
            texture = text;
        }
        public Texture2D ImageSource
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)Left, (int)Top, (int)Width, (int)Height), Color.White);
        }
    }
}
