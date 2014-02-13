using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Disque
{
    public delegate void ListModifiedEventHandker<T>(object sender, ListModifiedEventArgs<T> e);
    public class SpecialList<T> : IEnumerable<T>
    {
        ArrayList list = new ArrayList();
        public void Add(T item)
        {
            list.Add(item);
            if (ListModified != null)
                ListModified(this, new ListModifiedEventArgs<T>() { ItemModified = item, Removed = false });
        }
        public void Remove(T item)
        {
            if (list.Contains(item))
            {
                list.Remove(item);
                if (ListModified != null)
                    ListModified(this, new ListModifiedEventArgs<T>() { ItemModified = item, Removed = true });
            }
        }
        public T this[int index]
        {
            get
            {
                return (T)list[index];
            }
            set
            {
                list[index] = value;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in list)
            {
                yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (T item in list)
            {
                yield return item;
            }
        }
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        public event ListModifiedEventHandker<T> ListModified;
    }
    public class ListModifiedEventArgs<T> : EventArgs
    {
        public T ItemModified { get; set; }
        public bool Removed { get; set; }
    }
}
