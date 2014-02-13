using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Rml
{
    public class RElement
    {
        public string Name, Value;

        public RElement Parent;

        bool hasElements = false, hasAttributes = false;

        public readonly List<RElement> Elements = new List<RElement>();

        public readonly RAttributesCollection Attributes = new RAttributesCollection();

        public RElement(string name)
        {
            Name = name;
            Attributes.Parent = this;
        }

        public bool HasElements
        {
            get
            {
                return hasElements;
            }
            set
            {
                hasElements = value;
            }
        }

        public bool HasAttributes
        {
            get
            {
                return hasAttributes;
            }
            set
            {
                hasAttributes = value;
            }
        }

        public bool HasElement(string name)
        {
            foreach (RElement ele in Elements)
                if (ele.Name == name) return true;
            return false;
        }

        public bool HasAttribute(string name)
        {
            return Attributes.ContainsKey(name);
        }

        public RElement GetElement(string p)
        {
            foreach (RElement ele in Elements)
                if (ele.Name == p)
                    return ele;
            throw new ElementNotFound("No Element with the matching name " + p + " found.");
        }
    }

    public class RAttribute
    {
        public string Name, Value;

        public RElement Parent;
    }

    public class RAttributesCollection
    {
        public RElement Parent;

        readonly Dictionary<string, RAttribute> Attributes = new Dictionary<string, RAttribute>();

        public void Add(string a, RAttribute att)
        {
            Attributes.Add(a, att);
        }

        public bool ContainsKey(string k)
        {
            return Attributes.ContainsKey(k);
        }

        public RAttribute this[string a]
        {
            get
            {
                if (Attributes.ContainsKey(a))
                    return Attributes[a];
                else
                {
                    throw new Exception("Attribute " + a + " not found in " + Parent.Name);
                }
            }
            set
            {
                Attributes[a] = value;
            }
        }
    }
}
