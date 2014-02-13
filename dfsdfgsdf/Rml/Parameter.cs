using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Rml
{
    public class Parameter
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public Element Parent { get; set; }
        public Parameter(Element parent, string name, string value)
        {
            Parent = parent;
            Name = name;
            Value = value;
        }
    }
}
