using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer
{
    public static class Memory
    {
        public static List<object> ConstantMemory = new List<object>();
        public static void Remove(object o)
        {
            Replace(o, null);
        }
        public static void Replace(object o, object r)
        {
            int i = ConstantMemory.IndexOf(o);
            ConstantMemory.Remove(o);
            ConstantMemory.Insert(i, r);
        }
        public static void ClearMemory()
        {
            ConstantMemory.Clear();
            Collect();
        }
        public static void Collect()
        {
            GC.Collect();
        }
    }
}
