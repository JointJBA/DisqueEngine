using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Rml
{
    public class Element
    {
        List<Element> elements = new List<Element>();
        List<Parameter> parameters = new List<Parameter>();
        public Element Parent { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsParent
        {
            get
            {
                return Parent != null;
            }
        }
        public List<Element> Elements
        {
            get { return elements; }
            set { elements = value; }
        }
        public List<Parameter> Parameters
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;
            }
        }
        public Element(Element parent, string name, string value)
        {
            Name = name;
            Value = value;
            Parent = parent;
        }
        public IEnumerable<Parameter> GetParameters(string name)
        {
            var pmtrs = from x in Parameters where x.Name == name select x;
            return pmtrs;
        }
        public Parameter GetParameter(string name)
        {
            return GetParameters(name).First<Parameter>();
        }
        public IEnumerable<Element> GetElements(string name)
        {
            var elmnts = from x in Elements where x.Name == name select x;
            return elmnts;
        }
    }
}
