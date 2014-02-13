#include "MathHelper.h"

namespace Disque
{
	namespace Math
	{
		value class Vector3
		{
		public:
			T X, Y, Z;
			Vector3(T x, T y, T z) : X(x), Y(y), Z(z)
			{
			}

			Vector3(T v) : X(v), Y(v), Z(v)
			{
			}
			
			T LengthSquared()
			{
				return MathHelper::Pow(X, 2) + MathHelper::Pow(Y, 2) + MathHelper::Pow(Z, 2);
			}
			
			static T LengthSquared(Vector3 v)
			{
				return v.LengthSquared();
			}
			
			T Length()
			{
				return MathHelper::Sqrt(LengthSquared());
			}
			
			static T Length(Vector3 v)
			{
				return v.Length();
			}
						
			void Normalize()
			{
				T l = Length();
				X = X / l;
				Y = Y / l;
				Z = Z / l;
			}
			
			static Vector3 Normalize(Vector3 v)
			{
				v.Normalize();
				return v;
			}

			T Dot(Vector3 v)
			{
				return X * v.X + Y * v.Y + Z * v.Z;
			}
			
			static T Dot(Vector3 v1, Vector3 v2)
			{
				return v1.Dot(v2);
			}
			
			static T AbsDot(Vector3 v1, Vector3 v2)			
			{            
				return MathHelper::Abs(v1.Dot(v2));
			}
			
			Vector3 Cross(Vector3 v)
			{
				return Vector3((Y * v.Z) - (v.Y * Z), (Z * v.X) - (v.Z * X), (X * v.Y) - (v.X * Y));
			}
			
			static Vector3 Cross(Vector3 v1, Vector3 v2)
			{
				return v1.Cross(v2);
			}
			
			//static Vector3 Transform(Vector3 v, Matrix3 m)
			//{
			//	return Vector3(
			//		m[0, 0] * v.X + m[0, 1] * v.Y + m[0, 2] * v.Z,
			//		m[1, 0] * v.X + m[1, 1] * v.Y + m[1, 2] * v.Z,
			//		m[2, 0] * v.X + m[2, 1] * v.Y + m[2, 2] * v.Z);
			//}
			//
			//static Vector3 Transform(Vector3 v, Matrix4 m)
			//{
			//	return Vector3(
   //             m[0, 0] * v.X + m[0, 1] * v.Y + m[0, 2] * v.Z,
   //             m[1, 0] * v.X + m[1, 1] * v.Y + m[1, 2] * v.Z,
   //             m[2, 0] * v.X + m[2, 1] * v.Y + m[2, 2] * v.Z);
			//}
			//
			//static Vector3 TransformPosition(Vector3 v, Matrix4 m)
			//{
			//	return Vector3(
   //             m[0, 0] * v.X + m[0, 1] * v.Y + m[0, 2] * v.Z + m[0, 3],
   //             m[1, 0] * v.X + m[1, 1] * v.Y + m[1, 2] * v.Z + m[1, 3],
   //             m[2, 0] * v.X + m[2, 1] * v.Y + m[2, 2] * v.Z + m[2, 3]);
			//}
			//
			//static Vector3 TransformNormal(Vector3 v, Matrix4 m)
			//{
			//	return Vector3(
   //             m[0, 0] * v.X + m[1, 0] * v.Y + m[2, 0] * v.Z,
   //             m[0, 1] * v.X + m[1, 1] * v.Y + m[2, 1] * v.Z,
   //             m[0, 2] * v.X + m[1, 2] * v.Y + m[2, 2] * v.Z
			//	);
			//}
						
			static Vector3 operator -(Vector3 v)
			{
				return Vector3(-v.X, -v.Y, -v.Z);
			}
			
			static Vector3 operator +(Vector3 v1, Vector3 v2)
			{
				return Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
			}
			
			static Vector3 operator -(Vector3 v1, Vector3 v2)
			{
				return Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
			}
			
			static Vector3 operator *(Vector3 v, T s)
			{
				return Vector3(s * v.X, s * v.Y, s * v.Z);
			}
			
			static Vector3 operator *(T s, Vector3 v)
			{
				return v * s;
			}
			
			static Vector3 operator *(Vector3 value1, Vector3 value2)
			{
				Vector3 result;
				result.X = value1.X * value2.X;
				result.Y = value1.Y * value2.Y;
				result.Z = value1.Z * value2.Z;
				return result;
			}
			
			static Vector3 operator /(Vector3 v, T s)
			{
				return Vector3(v.X / s, v.Y / s, v.Z / s);
			}

			T Distance(Vector3 v)
			{
				
				return (Vector3(X, Y, Z) - v).Length();
			}
			
			static T Distance(Vector3 v1, Vector3 v2)
			{
				return v1.Distance(v2);
			}
			
			T DistanceSquared(Vector3 v)
			{
				return (Vector3(X, Y, Z) - v).LengthSquared();
			}
			
			static T DistanceSquared(Vector3 v1, Vector3 v2)
			{
				return v1.DistanceSquared(v2);
			}
			
			static bool operator ==(Vector3 v1, Vector3 v2)
			{
				return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
			}
			
			static bool operator !=(Vector3 v1, Vector3 v2)
			{
				return !(v1 == v2);
			}
		};
	};
};