#include <vector>

namespace Disque
{
	namespace Math
	{
		using namespace std;
		class Object
		{
		public:
			Object()
			{
			}

			virtual bool Equals(const Object* obj) const
			{
				return this == obj;
			}

			static bool Equals(const Object* a, const Object* b)
			{
				return a->Equals(b);
			}

			virtual Object* Clone() = 0;

			virtual void Dispose()
			{
			}

			~Object()
			{
				Dispose();
			}
		};
	}
}