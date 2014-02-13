using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.Core
{
    public interface IMovable
    {
        Vector3 Position { get; set; }
        Vector3 Acceleration { get; set; }
        Vector3 Velocity { get; set; }
        Vector3 ForceAccumulation { get; set; }
        void ClearAccumulation();
        void AddForce(Vector3 force);
    }
}
