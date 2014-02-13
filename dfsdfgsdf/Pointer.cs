using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer
{
    public class Pointer<T> : IDisposable
    {
        int index;
        public Pointer(T data)
        {
            Memory.ConstantMemory.Add(data);
            index = Memory.ConstantMemory.Count - 1;
        }
        Pointer(int mindex)
        {
            index = mindex;
        }
        public static Pointer<T> operator ++(Pointer<T> p)
        {
            p.index++;
            return p;
        }
        public static Pointer<T> operator --(Pointer<T> p)
        {
            p.index--;
            return p;
        }
        public static Pointer<T> operator +(Pointer<T> p, int i)
        {
            p.index += i;
            return p;
        }
        public static Pointer<T> operator -(Pointer<T> p, int i)
        {
            p.index -= i;
            return p;
        }
        public Pointer<T> Decrement()
        {
            int i = index;
            index--;
            return new Pointer<T>(i);
        }
        public Pointer<T> Increment()
        {
            int i = index;
            index++;
            return new Pointer<T>(i);
        }
        public static T operator ~(Pointer<T> p)
        {
            return (T)Memory.ConstantMemory[p.index];
        }
        public static explicit operator Pointer<T>(T o)
        {
            return new Pointer<T>(o);
        }
        public static implicit operator int(Pointer<T> o)
        {
            return o.index;
        }
        public void Delete()
        {
            Delete(1);
        }
        public void Delete(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Memory.Remove(Memory.ConstantMemory[index + length]);
            }
        }
        public static Pointer<T> Allocate(T[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Memory.ConstantMemory.Add(data[i]);
            }
            return new Pointer<T>((Memory.ConstantMemory.Count) - data.Length);
        }
        public static Pointer<T> Allocate(int length)
        {
            for (int i = 0; i < length; i++)
                Memory.ConstantMemory.Add(null);
            return new Pointer<T>((Memory.ConstantMemory.Count) - length);
        }
        public T this[int i]
        {
            get
            {
                return (T)Memory.ConstantMemory[index + i];
            }
            set
            {
                Memory.Replace(Memory.ConstantMemory[index + i], value);
            }
        }
        public void Dispose()
        {
            Delete();
        }
    }
}
