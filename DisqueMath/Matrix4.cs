using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Math
{
    public struct Matrix4
    {
        float[,] data;
        public Matrix4(float[,] _data)
        {
            data = _data;
        }
        public float this[int x, int y]
        {
            get
            {
                return data[x, y];
            }
            set
            {
                data[x, y] = value;
            }
        }
        public static readonly Matrix4 Identity = new Matrix4(new float[4, 4] 
        {
        {1,0,0,0},
        {0,1,0,0},
        {0,0,1,0},
        {0,0,0,1}
        });
        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            float[,] m = new float[4, 4];
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                {
                    float sum = 0.0f;
                    for (int j = 0; j < 4; j++)
                        sum += a[x, j] * b[j, y];

                    m[x, y] = sum;
                }
            return new Matrix4(m);
        }
        public static Matrix4 operator /(Matrix4 a, float d)
        {
            float[,] m = new float[4, 4];
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                {
                    m[x, y] = a[x, y] / d;
                }
            return new Matrix4(m);
        }
        public static Matrix4 CreateFromTranslation(Vector3 t)
        {
            Matrix4 m = new Matrix4(new float[4, 4] 
            {
              {1,0,0,t.X},
              {0,1,0,t.Y},
              {0,0,1,t.Z},
              {0,0,0,1}
            });
            return m;
        }
        public static Matrix4 CreateFromScale(Vector3 s)
        {
            return new Matrix4(new float[4, 4] 
        {
        {s.X,0,0,0},
        {0,s.Y,0,0},
        {0,0,s.Z,0},
        {0,0,0,1}
        });
        }
    }
}
