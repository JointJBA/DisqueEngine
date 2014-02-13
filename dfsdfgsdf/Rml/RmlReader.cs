using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Disque.Raytracer.Rml
{
    public class RmlReader
    {
        Element root;
        string _data;
        public RmlReader(string data)
        {
            _data = data;
            parse();
        }
        public Element Root
        {
            get
            {
                return root;
            }
        }
        void parse()
        {
            XDocument xdoc = XDocument.Parse(_data);
            root = new Element(null, "Root", "");
            getcollections(xdoc.Element("Root"), root);
        }
        void getcollections(XElement x, Element parent)
        {
            foreach (XElement xele in x.Elements())
            {
                if (xele.Name.LocalName == "Group")
                {
                    Element p = new Element(parent, xele.Name.LocalName, "");
                    if (xele.HasAttributes)
                    {
                        p.Parameters.Add(new Parameter(p, "Name", xele.FirstAttribute.Value));
                    }
                    parent.Elements.Add(p);
                    getcollections(xele, p);
                }
                else
                {
                    getelement(xele, parent);
                }
            }
        }
        void getelement(XElement xe, Element group)
        {
            Element el = new Element(group, xe.Name.LocalName, "");
            foreach (XAttribute xa in xe.Attributes())
            {
                el.Parameters.Add(new Parameter(el, xa.Name.LocalName, xa.Value));
            }
            group.Elements.Add(el);
        }
        public IEnumerable<Element> GetElements(string name)
        {
            var elmnts = from x in Root.Elements where x.Name == name select x;
            return elmnts;
        }
    }
}
