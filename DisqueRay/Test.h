#include "core.h"

namespace Disque
{
	namespace Math
	{
		using namespace System;
		using namespace System::Diagnostics;
		public ref class Test
		{
		public:
			static void DoWork()
			{
				Stopwatch^ watch = gcnew Stopwatch;
				watch->Start();
				vec3 pi(PI), v(1);
				for(int i = 0; i < 1000000; i++)
				{
					v = v * pi;
				}
				watch->Stop();
				Console::WriteLine(watch->ElapsedMilliseconds);
			}
		};
	}
}