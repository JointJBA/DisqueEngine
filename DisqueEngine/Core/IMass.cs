using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Core
{
    public interface IMass
    {
        float Mass { get; set; }
        float InverseMass { get; }
        float LinearDrag { get; set; }
        float AngularDrag { get; set; }
    }
}
