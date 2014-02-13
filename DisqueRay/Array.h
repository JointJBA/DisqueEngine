#include "Object.h"
#include <initializer_list>

using namespace std;

namespace Disque
{
	namespace Math
	{
		template <typename T>
		class A : Object
		{
			T* arr;
			int len;
		public:
			A(int l) : len(l), Object()
			{
				arr = new T[l];
			}

			A(initializer_list<T>& l) : len(l.size()), Object()
			{
				int i = 0;
				for(T t : l)
				{
					arr[0] = t;
					i++;
				}
			}

			int Length() const
			{
				return len;
			}

			T Max()
			{
				T max = arr[0];
				for(int i = 1; i < len; i++)
				{
					if(arr[i] > max)
						max = arr[i];
				}
				return max;
			}

			T Min()
			{
				T min = arr[0];
				for(int i = 1; i < len; i++)
				{
					if(arr[i] < min)
						min = arr[i];
				}
				return min;
			}

			virtual bool Equals(const Object* obj)
			{
				A<T>* ref = (A<T>*)obj;
				if(ref->Length() == len)
				{
					for(int i = 0; i < len; i++)
					{
						if(arr[i] != (*ref)[i])
							return false;
					}
				}
				return true;
			}

			virtual Object* Clone()
			{
				A<T> ar(len);
				for(int i = 0; i < len; i++)
				{
					ar[i] = arr[i];
				}
				return &ar;
			}

			T& operator [] (int index)
			{
				return arr[index];
			}

			virtual void Dispose()
			{
				delete[] arr;
			}
		};
	}
}