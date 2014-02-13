namespace Disque
{
	namespace CMath
	{
		template <typename T = float>
		public value class Vector3
		{
		public:
			T X, Y, Z;
			Vector3(T x, T y, T z)
			{
				X = x;
				Y = y;
				Z = z;
			}
			static Vector3<T> operator +(Vector3<T> v1, Vector3<T> v2)
			{
				return Vector3<T>(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
			}
			static Vector3<T> operator -(Vector3<T> v1, Vector3<T> v2)
			{
				return Vector3<T>(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
			}
		};
	};
};