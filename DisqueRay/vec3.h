#include "core.h"

#ifndef VECTOR3
#define VECTOR3

namespace Disque
{
	namespace Math
	{
		class matrix4;

		class vec3
		{
			flt cor[3];

		public:
			vec3(flt x, flt y, flt z)
			{
				cor[X] = x;
				cor[Y] = y;
				cor[Z] = z;
			}
			vec3(flt v)
			{
				cor[X] = cor[Y] = cor[Z] = v;
			}
			vec3()
			{
				cor[X] = cor[Y] = cor[Z] = 0;
			}

			flt& getX()
			{
				return cor[X];
			}
			void setX(const flt& x)
			{
				cor[X] = x;
			}

			flt& getY()
			{
				return cor[Y];
			}
			void setY(const flt& y)
			{
				cor[Y] = y;
			}

			flt& getZ()
			{
				return cor[Z];
			}
			void setZ(const flt& z)
			{
				cor[Z] = z;
			}

			flt& operator[] (const int& i)
			{
				return cor[i];
			}

			vec3& operator+ (const vec3& b)
			{
				return vec3(cor[X] + b.cor[X], cor[Y] + b.cor[Y], cor[Z] + b.cor[Z]);
			}

			vec3& operator- (const vec3& b)
			{
				return vec3(cor[X] - b.cor[X], cor[Y] - b.cor[Y], cor[Z] - b.cor[Z]);
			}

			vec3& operator* (const flt& s)
			{
				return vec3(cor[X] * s, cor[Y] * s, cor[Z] * s);
			}

			vec3& operator* (const vec3& b)
			{
				return vec3(cor[X] * b.cor[X], cor[Y] * b.cor[Y], cor[Z] * b.cor[Z]);
			}

			vec3& operator/ (const flt& s)
			{
				return vec3(cor[X] / s, cor[Y] / s, cor[Z] / s);
			}

			flt operator, (const vec3& b)
			{
				return cor[X] * b.cor[X] + cor[Y] * b.cor[Y] + cor[Z] * b.cor[Z];
			}

			vec3& operator^ (const vec3& b)
			{
				return vec3((cor[Y] * b.cor[Z]) - (b.cor[Y] * cor[Z]), (cor[Z] * b.cor[X]) - (b.cor[Z] * cor[X]), (cor[X] * b.cor[Y]) - (b.cor[X] * cor[Y]));
			}

			flt lengthSquared()
			{
				return pow(cor[X]) + pow(cor[Y]) + pow(cor[Z]);
			}

			flt length()
			{
				return sqrt(lengthSquared());
			}

			void normalize()
			{
				flt l = length();
				cor[X] /= l;
				cor[Y] /= l;
				cor[Z] /= l;
			}

		    static vec3& transform(vec3& v, matrix4& m);

			static vec3& transformPosition(vec3& v, matrix4& m);

			static vec3& transformNormal(vec3& v, matrix4& m);

			bool operator== (const vec3& b)
			{
				return equals(cor[X], b.cor[X]) && equals(cor[Y], b.cor[Y]) && equals(cor[Z], b.cor[Z]);
			}

			bool operator!= (const vec3& b)
			{
				return !(*this == b);
			}
		};
	}
}

#endif