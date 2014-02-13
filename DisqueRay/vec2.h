#include "core.h"

#ifndef VECTOR2
#define VECTOR2

namespace Disque
{
	namespace Math
	{
		class vec2
		{
			flt cor[2];

		public:
			vec2(flt x, flt y)
			{
				cor[X] = x;
				cor[Y] = y;
			}
			vec2(flt v)
			{
				cor[X] = cor[Y] = v;
			}
			vec2()
			{
				cor[X] = cor[Y] = 0;
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

			flt& operator[] (const int& i)
			{
				return cor[i];
			}

			vec2& operator+ (const vec2& b)
			{
				return vec2(cor[X] + b.cor[X], cor[Y] + b.cor[Y]);
			}

			vec2& operator- (const vec2& b)
			{
				return vec2(cor[X] - b.cor[X], cor[Y] - b.cor[Y]);
			}

			vec2& operator* (const flt& s)
			{
				return vec2(cor[X] * s, cor[Y] * s);
			}

			vec2& operator* (const vec2& b)
			{
				return vec2(cor[X] * b.cor[X], cor[Y] * b.cor[Y]);
			}

			vec2& operator/ (const flt& s)
			{
				return vec2(cor[X] / s, cor[Y] / s);
			}

			flt operator, (const vec2& b)
			{
				return cor[X] * b.cor[X] + cor[Y] * b.cor[Y];
			}

			flt lengthSquared()
			{
				return pow(cor[X]) + pow(cor[Y]);
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
			}

			bool operator== (const vec2& b)
			{
				return equals(cor[X], b.cor[X]) && equals(cor[Y], b.cor[Y]);
			}

			bool operator!= (const vec2& b)
			{
				return !(*this == b);
			}
		};
	}
}

#endif