using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Disque.Xna.Primatives
{
    public interface IPrimitive : IDraw
    {
        Matrix Transformation { get; set; }
    }
}
