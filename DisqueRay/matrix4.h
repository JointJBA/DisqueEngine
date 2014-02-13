#include "core.h"
#include <assert.h>

#ifndef MATRIX4
#define MATRIX4

namespace Disque
{
	namespace Math
	{
		class matrix4
		{
			flt data[4][4];
			
		public:

			matrix4()
			{
				data[0][0] = data[1][1] = data[2][2] = data[3][3] = 1;
			}

			flt detriment()
			{
				return
         data[3][0] * data[2][1] * data[1][2] * data[0][3] - data[2][0] * data[3][1] * data[1][2] * data[0][3] -
         data[3][0] * data[1][1] * data[2][2] * data[0][3] + data[1][0] * data[3][1] * data[2][2] * data[0][3] +
         data[2][0] * data[1][1] * data[3][2] * data[0][3] - data[1][0] * data[2][1] * data[3][2] * data[0][3] -
         data[3][0] * data[2][1] * data[0][2] * data[1][3] + data[2][0] * data[3][1] * data[0][2] * data[1][3] +
         data[3][0] * data[0][1] * data[2][2] * data[1][3] - data[0][0] * data[3][1] * data[2][2] * data[1][3] -
         data[2][0] * data[0][1] * data[3][2] * data[1][3] + data[0][0] * data[2][1] * data[3][2] * data[1][3] +
         data[3][0] * data[1][1] * data[0][2] * data[2][3] - data[1][0] * data[3][1] * data[0][2] * data[2][3] -
         data[3][0] * data[0][1] * data[1][2] * data[2][3] + data[0][0] * data[3][1] * data[1][2] * data[2][3] +
         data[1][0] * data[0][1] * data[3][2] * data[2][3] - data[0][0] * data[1][1] * data[3][2] * data[2][3] -
         data[2][0] * data[1][1] * data[0][2] * data[3][3] + data[1][0] * data[2][1] * data[0][2] * data[3][3] +
         data[2][0] * data[0][1] * data[1][2] * data[3][3] - data[0][0] * data[2][1] * data[1][2] * data[3][3] -
         data[1][0] * data[0][1] * data[2][2] * data[3][3] + data[0][0] * data[1][1] * data[2][2] * data[3][3];
			}

			flt* operator[](int r)
			{
				return data[r];
			}

			matrix4& inverse()
			{
				flt dete = detriment();
				assert(dete != 0.0);
				matrix4 result;
				result[0][0] = data[1][2] * data[2][3] * data[3][1] - data[1][3] * data[2][2] * data[3][1] + data[1][3] * data[2][1] * data[3][2] - data[1][1] * data[2][3] * data[3][2] - data[1][2] * data[2][1] * data[3][3] + data[1][1] * data[2][2] * data[3][3];
				result[0][1] = data[0][3] * data[2][2] * data[3][1] - data[0][2] * data[2][3] * data[3][1] - data[0][3] * data[2][1] * data[3][2] + data[0][1] * data[2][3] * data[3][2] + data[0][2] * data[2][1] * data[3][3] - data[0][1] * data[2][2] * data[3][3];
				result[0][2] = data[0][2] * data[1][3] * data[3][1] - data[0][3] * data[1][2] * data[3][1] + data[0][3] * data[1][1] * data[3][2] - data[0][1] * data[1][3] * data[3][2] - data[0][2] * data[1][1] * data[3][3] + data[0][1] * data[1][2] * data[3][3];
				result[0][3] = data[0][3] * data[1][2] * data[2][1] - data[0][2] * data[1][3] * data[2][1] - data[0][3] * data[1][1] * data[2][2] + data[0][1] * data[1][3] * data[2][2] + data[0][2] * data[1][1] * data[2][3] - data[0][1] * data[1][2] * data[2][3];
				result[1][0] = data[1][3] * data[2][2] * data[3][0] - data[1][2] * data[2][3] * data[3][0] - data[1][3] * data[2][0] * data[3][2] + data[1][0] * data[2][3] * data[3][2] + data[1][2] * data[2][0] * data[3][3] - data[1][0] * data[2][2] * data[3][3];
				result[1][1] = data[0][2] * data[2][3] * data[3][0] - data[0][3] * data[2][2] * data[3][0] + data[0][3] * data[2][0] * data[3][2] - data[0][0] * data[2][3] * data[3][2] - data[0][2] * data[2][0] * data[3][3] + data[0][0] * data[2][2] * data[3][3];
				result[1][2] = data[0][3] * data[1][2] * data[3][0] - data[0][2] * data[1][3] * data[3][0] - data[0][3] * data[1][0] * data[3][2] + data[0][0] * data[1][3] * data[3][2] + data[0][2] * data[1][0] * data[3][3] - data[0][0] * data[1][2] * data[3][3];
				result[1][3] = data[0][2] * data[1][3] * data[2][0] - data[0][3] * data[1][2] * data[2][0] + data[0][3] * data[1][0] * data[2][2] - data[0][0] * data[1][3] * data[2][2] - data[0][2] * data[1][0] * data[2][3] + data[0][0] * data[1][2] * data[2][3];
				result[2][0] = data[1][1] * data[2][3] * data[3][0] - data[1][3] * data[2][1] * data[3][0] + data[1][3] * data[2][0] * data[3][1] - data[1][0] * data[2][3] * data[3][1] - data[1][1] * data[2][0] * data[3][3] + data[1][0] * data[2][1] * data[3][3];
				result[2][1] = data[0][3] * data[2][1] * data[3][0] - data[0][1] * data[2][3] * data[3][0] - data[0][3] * data[2][0] * data[3][1] + data[0][0] * data[2][3] * data[3][1] + data[0][1] * data[2][0] * data[3][3] - data[0][0] * data[2][1] * data[3][3];
				result[2][2] = data[0][1] * data[1][3] * data[3][0] - data[0][3] * data[1][1] * data[3][0] + data[0][3] * data[1][0] * data[3][1] - data[0][0] * data[1][3] * data[3][1] - data[0][1] * data[1][0] * data[3][3] + data[0][0] * data[1][1] * data[3][3];
				result[2][3] = data[0][3] * data[1][1] * data[2][0] - data[0][1] * data[1][3] * data[2][0] - data[0][3] * data[1][0] * data[2][1] + data[0][0] * data[1][3] * data[2][1] + data[0][1] * data[1][0] * data[2][3] - data[0][0] * data[1][1] * data[2][3];
				result[3][0] = data[1][2] * data[2][1] * data[3][0] - data[1][1] * data[2][2] * data[3][0] - data[1][2] * data[2][0] * data[3][1] + data[1][0] * data[2][2] * data[3][1] + data[1][1] * data[2][0] * data[3][2] - data[1][0] * data[2][1] * data[3][2];
				result[3][1] = data[0][1] * data[2][2] * data[3][0] - data[0][2] * data[2][1] * data[3][0] + data[0][2] * data[2][0] * data[3][1] - data[0][0] * data[2][2] * data[3][1] - data[0][1] * data[2][0] * data[3][2] + data[0][0] * data[2][1] * data[3][2];
				result[3][2] = data[0][2] * data[1][1] * data[3][0] - data[0][1] * data[1][2] * data[3][0] - data[0][2] * data[1][0] * data[3][1] + data[0][0] * data[1][2] * data[3][1] + data[0][1] * data[1][0] * data[3][2] - data[0][0] * data[1][1] * data[3][2];
				result[3][3] = data[0][1] * data[1][2] * data[2][0] - data[0][2] * data[1][1] * data[2][0] + data[0][2] * data[1][0] * data[2][1] - data[0][0] * data[1][2] * data[2][1] - data[0][1] * data[1][0] * data[2][2] + data[0][0] * data[1][1] * data[2][2];
				return result;
			}

			matrix4& transpose()
			{
				matrix4 result;
				for (int r = 0; r < 4; r++)
					for (int c = 0; c < 4; c++)
						result[c][r] = data[r][ c];
				return result;
			}

		    static matrix4 createRotation(vec3& axis, const flt& radian)
			{
				flt x = axis.getX();
				flt y = axis.getY();
				flt z = axis.getZ();
				flt num = sin(radian);
				flt num2 = cos(radian);
				flt num3 = x * x;
				flt num4 = y * y;
				flt num5 = z * z;
				flt num6 = x * y;
				flt num7 = x * z;
				flt num8 = y * z;
				matrix4 result;
				result[0][0] = num3 + num2 * (1.0 - num3);
				result[0][1] = num6 - num2 * num6 + num * z;
				result[0][2] = num7 - num2 * num7 - num * y;
				result[0][3] = 0;
				result[1][0] = num6 - num2 * num6 - num * z;
				result[1][1] = num4 + num2 * (1.0 - num4);
				result[1][2] = num8 - num2 * num8 + num * x;
				result[1][3] = 0;
				result[2][0] = num7 - num2 * num7 + num * y;
				result[2][1] = num8 - num2 * num8 - num * x;
				result[2][2] = num5 + num2 * (1.0 - num5);
				result[2][3] = 0;
				return result;
			}

			static matrix4 createScale(vec3& v)
			{
				matrix4 res;
				res[0][0] = v.getX();
				res[1][1] = v.getY();
				res[2][2] = v.getZ();
				return res;
			}

			static matrix4 createTranslation(vec3& v)
			{
				matrix4 res;
				res[0][3] = v.getX();
				res[1][3] = v.getY();
				res[2][3] = v.getZ();
				return res;
			}

		};

		vec3& vec3::transform(vec3& v, matrix4& m)
		{
			return vec3(
				m[0][0] * v.cor[X] + m[0][1] * v.cor[Y] + m[0][2] * v.cor[Z],
				m[1][0] * v.cor[X] + m[1][1] * v.cor[Y] + m[1][2] * v.cor[Z],
				m[2][0] * v.cor[X] + m[2][1] * v.cor[Y] + m[2][2] * v.cor[Z]);
		}

		vec3& vec3::transformPosition(vec3& v, matrix4& m)
		{
			return vec3(
				m[0][0] * v.cor[X] + m[0][1] * v.cor[Y] + m[0][2] * v.cor[Z] + m[0][3],
				m[1][0] * v.cor[X] + m[1][1] * v.cor[Y] + m[1][2] * v.cor[Z] + m[1][3],
				m[2][0] * v.cor[X] + m[2][1] * v.cor[Y] + m[2][2] * v.cor[Z] + m[2][3]);
		}

		vec3& vec3::transformNormal(vec3& v, matrix4& m)
		{
			return vec3(
				m[0][0] * v.cor[X] + m[1][0] * v.cor[Y] + m[2][0] * v.cor[Z],
				m[0][1] * v.cor[X] + m[1][1] * v.cor[Y] + m[2][1] * v.cor[Z],
				m[0][2] * v.cor[X] + m[1][2] * v.cor[Y] + m[2][2] * v.cor[Z]
			);
		}

	}
}

#endif