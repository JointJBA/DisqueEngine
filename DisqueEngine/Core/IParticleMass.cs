using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.Core
{
    public interface IParticleMass
    {
        float Mass { get; set; }
        float InverseMass { get; }
        float Drag { get; set; }
    }
}
