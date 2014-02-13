using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Textures
{
    public abstract class Texture<T>
    {
        public abstract T Evaluate(DifferentialGeometry dg);
    }
}
