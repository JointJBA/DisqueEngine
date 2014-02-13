using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Disque.Math;
using DVector = Disque.Math.Vector3;
using XVector = Microsoft.Xna.Framework.Vector3;

namespace Disque.Xna
{
    public static class Extensions
    {
        public static XVector ToVector3(this DVector vector)
        {
            return new XVector(vector.X, vector.Y, vector.Z);
        }
        public static DVector ToVector3(this XVector vector)
        {
            return new DVector(vector.X, vector.Y, vector.Z);
        }
        public static Matrix ToMatrix(this Matrix4 matrix)
        {
            Matrix m = new Matrix(
                matrix[0], matrix[1], matrix[2], matrix[3],
                matrix[4], matrix[5], matrix[6], matrix[7],
                matrix[8], matrix[9], matrix[10], matrix[11],
                0, 0, 0, 1);
            return Matrix.Transpose(m);
        }
    }
}
