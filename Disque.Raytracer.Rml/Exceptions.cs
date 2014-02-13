using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Rml
{
    public class ElementNotFound : Exception
    {
        public string Element;
        public ElementNotFound(string ele)
            : base(ele + " not found.")
        {
            Element = ele;
        }
    }

    public class IncorrectVectorFormat : Exception
    {
        public IncorrectVectorFormat()
            : base("Incorrect vector format. Make sure that there is no spacing between commas.")
        {
        }
    }

    public class PropertyNotFound : Exception
    {
        public string Element, Property;
        public PropertyNotFound(string ele, string property)
            : base(property + " not found in " + ele)
        {
            Element = ele;
            Property = property;
        }
    }

    public class NotCorrectValue : Exception
    {
        public string Element, Property;
        public NotCorrectValue(string ele, string property)
            : base("Not a proper value for " + property + " in " + ele)
        {
            Element = ele;
            Property = property;
        }
    }
}
