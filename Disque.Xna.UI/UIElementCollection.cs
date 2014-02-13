using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Disque.Xna.UI
{
    public class UIElementCollection : IEnumerable<UIElement>, IEnumerator<UIElement>
    {
        List<UIElement> elements = new List<UIElement>();
        public void Add(UIElement element)
        {
            elements.Add(element);
        }
        public void Remove(UIElement element)
        {
            elements.Remove(element);
        }
        int pos = 0;
        public UIElement Current
        {
            get { return elements[pos]; }
        }
        public void Dispose()
        {
        }
        object IEnumerator.Current
        {
            get { return elements[pos]; }
        }
        public bool MoveNext()
        {
            pos++;
            return pos < elements.Count;
        }
        public void Reset()
        {
            pos = 0;
        }
        public UIElement this[int i]
        {
            get
            {
                return elements[i];
            }
            set
            {
                elements[i] = value;
            }
        }
        public IEnumerator<UIElement> GetEnumerator()
        {
            return (IEnumerator<UIElement>)this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
