#include <math.h>
#include "Array.h"
#define T float

using namespace System;
using namespace std;

namespace Disque
{
	namespace Math
	{
		public ref class MathHelper
		{
		public:
			static Random^ r = gcnew Random(DateTime::Now.Millisecond);
			static const T PI = 3.141592653589793f;
			static const T TwoPI = 6.2831853071795864769f;
			static const T PIOn180 = 0.0174532925199432957f;
			static const T InvPI = 0.3183098861837906715f;
			static const T InvTwoPI = 0.15915494309189533577f;
			static const T Epsilon = 0.0001f;
			static const T BigEpsilon = 0.001f;
			static const T SleepEpilson = 0.3f;
			static const T PI4div3 = 4.18879f;
			static const T InvRandMax = 1.0f / 0x7fff;
			
			static T Sqrt(T n)
			{
				return sqrt(n);
			}

			static T Pow(T a, T b)
			{
				return pow(a, b);
			}
			
			static T Abs(T n)
			{
				return Sqrt(Pow(n, 2));
			}
			
			static T Exp(T value)
			{
				return exp(value);
			}

			static T Cbrt(T n)
			{
				T div = 1.0f / 3.0f;
				if (n < 0)
					return -Pow(-n, div);
				return Pow(n, div);
			}
			
			static T Cos(T radian)
			{
				return cos(radian);
			}
			
			static T Acos(T radian)
			{
				return acos(radian);
			}
			
			static T Sin(T radian)
			{
				return sin(radian);
			}
			
			static T Asin(T radian)
			{
				return asin(radian);
			}

			static T Tan(T radian)
			{
				return tan(radian);
			}

			static T Atan(T radian)
			{
				return atan(radian);
			}

			static T Atan2(T x, T y)
			{
				return atan2(x, y);
			}

			static T ToRadians(T degrees)
			{
				return degrees * (PI / 180);
			}

			static T ToDegrees(T radians)
			{
				return radians * (180 / PI);
			}

			static T Min(T a, T b)
			{
				if (a < b)
					return a;
				return b;
			}

			static T Max(T a, T b)
			{
				if (a > b)
					return a;
				return b;
			}

			static int Min(int a, int b)
			{
				if (a < b)
					return a;
				return b;
			}

			static int Max(int a, int b)
			{
				if (a > b)
					return a;
				return b;
			}

			static T Lerp(T t, T v1, T v2)
			{
				return (1.0f - t) * v1 + t * v2;
			}

			static T Clamp(T val, T low, T high)
			{
				if (val < low) return low;
				else if (val > high) return high;
				else return val;
			}

			static int RandomInt()
			{
				return r->Next(0x7fff);
			}

			static int RandomInt(int max)
			{
				return r->Next(max);
			}

			static int RandomInt(int l, int h)
			{
				return (r->Next(l, h));
			}

			static T RandomT()
			{
				return (T)RandomInt() * InvRandMax;
			}

			static T RandomT(T l, T h)
			{
				T ans = RandomT() * (h - (l)) + (l);
				return ans;
			}

			static void SetRandomSeed(int seed)
			{
				r = gcnew Random(seed);
			}
		
			static bool IsZero(T x)
			{
				T EQN_EPS = 1e-90f;
				return (x) > -EQN_EPS && (x) < EQN_EPS;
			}

			static int Floor(T p)
			{
				return floor(p);
			}

			static T Mod(T a, T b)
			{
				int n = (int)(a / b);

				a -= n * b;
				if (a < 0.0)
					a += b;

				return (a);
			}

			static T SmoothStep(T a, T b, T x)
			{
				if (x < a)
					return (0.0f);
				if (x >= b)
					return (1.0f);
				T y = (x - a) / (b - a);
				return (y * y * (3.0f - 2.0f * y));
			}

			static T SmoothPulse(T e0, T e1, T e2, T e3, T x)
			{
				return (SmoothStep(e0, e1, x) - SmoothStep(e2, e3, x));
			}

			static T SmoothPulseTrain(T e0, T e1, T e2, T e3, T period, T x)
			{
				return (SmoothPulse(e0, e1, e2, e3, Mod(x, period)));
			}

			static T MixT(T a, T b, T f)
			{
				return ((1.0f - f) * a + f * b);
			}

			static String^ Print()
			{
				A<int> arr(4);
				arr[0] = 10;
				arr[1] = 5;
				arr[2] = 56;
				arr[3] = 30;
				String^ s = L"";
				for(int i = 0; i < arr.Length(); i++)
				{
					s += L" " + arr[i];
				}
				return s;
			}
		};
	}
}
