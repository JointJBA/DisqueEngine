using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Disque.Xna.Cameras;

namespace Disque.Xna
{
    public interface IDraw
    {
        void Update(GameTime gametime);
        void Draw(GameTime gametime, Camera camera);
        BoundingBox BoundingBox { get; }
    }
}
