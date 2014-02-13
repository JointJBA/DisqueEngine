using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Math
{
    public struct Matrix4x4
    {
        public static readonly Matrix4x4 Identity = new Matrix4x4(new float[4, 4]{
        {1,0,0,0},
        {0,1,0,0},
        {0,0,1,0},
        {0,0,0,1}});
        float[,] matrix;
        public Matrix4x4(float[,] m)
        {
            matrix = m;
        }
        public Matrix4x4(string m)
        {
            if (m.Length != 16)
                throw new Exception();
            string[] nums = m.Split(',');
            float[] fn = new float[16];
            for (int i = 0; i < 16; i++)
                fn[i] = float.Parse(nums[i]);
            matrix = new float[4, 4]{
            {fn[0],fn[1],fn[2],fn[3]},
            {fn[4],fn[5],fn[6],fn[7]},
            {fn[8],fn[9],fn[10],fn[11]},
            {fn[12],fn[13],fn[14],fn[15]}
            };
        }
        public void Inverse()
        {
            int[] indxc = new int[4], indxr = new int[4];
            int[] ipiv = { 0, 0, 0, 0 };
            float[,] minv = new float[4, 4];
            MathHelper.Copy<float>(out minv, matrix);
            for (int i = 0; i < 4; i++)
            {
                int irow = -1, icol = -1;
                float big = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (ipiv[j] != 1)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if (ipiv[k] == 0)
                            {
                                if (MathHelper.Abs(minv[j, k]) >= big)
                                {
                                    big = (MathHelper.Abs(minv[j, k]));
                                    irow = j;
                                    icol = k;
                                }
                            }
                            else if (ipiv[k] > 1)
                                throw new Exception("Singular matrix in MatrixInvert");
                        }
                    }
                }
                ++ipiv[icol];
                if (irow != icol)
                {
                    for (int k = 0; k < 4; ++k)
                        MathHelper.Swap<float>(ref minv[irow, k], ref minv[icol, k]);
                }
                indxr[i] = irow;
                indxc[i] = icol;
                if (minv[icol, icol] == 0)
                    throw new Exception("Singular matrix in MatrixInvert");
                float pivinv = 1.0f / minv[icol, icol];
                minv[icol, icol] = 1.0f;
                for (int j = 0; j < 4; j++)
                    minv[icol, j] *= pivinv;
                for (int j = 0; j < 4; j++)
                {
                    if (j != icol)
                    {
                        float save = minv[j, icol];
                        minv[j, icol] = 0;
                        for (int k = 0; k < 4; k++)
                            minv[j, k] -= minv[icol, k] * save;
                    }
                }
            }
        }
        public static bool operator ==(Matrix4x4 m1, Matrix4x4 m2)
        {
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    if (m1[x, y] != m2[x, y])
                        return false;
                }
            return true;
        }
        public static bool operator !=(Matrix4x4 m1, Matrix4x4 m2)
        {
            return !(m1 == m2);
        }
        public static Matrix4x4 Transpose(Matrix4x4 matrix)
        {
            return new Matrix4x4(new float[,] {
            {matrix[0,0], matrix[1,0], matrix[2,0], matrix[3,0]},
            {matrix[0,1], matrix[1,1], matrix[2,1], matrix[3,1]},
            {matrix[0,2], matrix[1,2], matrix[2,2], matrix[3,2]},
            {matrix[0,3], matrix[1,3], matrix[2,3], matrix[3,3]}});
        }
        public static Matrix4x4 Inverse(Matrix4x4 matrix)
        {
            matrix.Inverse();
            return matrix;
        }
        public static Matrix4x4 operator *(Matrix4x4 m1, Matrix4x4 m2)
        {
            Matrix4x4 r = new Matrix4x4();
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                    r[i, j] = m1[i, 0] * m2[0, j] +
                                m1[i, 1] * m2[1, j] +
                                m1[i, 2] * m2[2, j] +
                                m1[i, 3] * m2[3, j];
            return r;
        }
        public float this[int r, int c]
        {
            get
            {
                if (matrix == null) matrix = new float[4, 4];
                return matrix[r, c];
            }
            set
            {
                if (matrix == null) matrix = new float[4, 4];
                matrix[r, c] = value;
            }
        }
    }
}
