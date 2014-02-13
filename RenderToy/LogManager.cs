using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace RenderToy
{
    public class LogManager
    {
        public string CurrentLogUri;

        public readonly List<ImageData> Images = new List<ImageData>();

        public void Save()
        {
            string header = "<Log>\r\n";
            foreach (ImageData im in Images)
            {
                header += "<Entry Uri=\"" + im.Uri + "\" Samples=\"" + im.NumSamples + "\" RenderTime=\"" + im.RenderTime.ToString() + "\" RenderDate=\"" + im.RenderDate.ToShortDateString() + "\"/>\r\n";
            }
            header += "</Log>";
            File.WriteAllText(CurrentLogUri, header);
        }

        public void Open(string uri)
        {
            Images.Clear();
            if (!File.Exists(uri))
                File.WriteAllText(uri, "<Log></Log>");
            CurrentLogUri = uri;
            XDocument xdoc = XDocument.Load(uri);
            foreach (XElement xele in xdoc.Element("Log").Elements())
            {
                ImageData imd = new ImageData() { Uri = xele.Attribute("Uri").Value, NumSamples = int.Parse(xele.Attribute("Samples").Value), RenderTime = TimeSpan.Parse(xele.Attribute("RenderTime").Value), RenderDate = DateTime.Parse(xele.Attribute("RenderDate").Value) };
                Images.Add(imd);
            }
        }
    }
    public struct ImageData
    {
        public string Uri;
        public int NumSamples;
        public DateTime RenderDate;
        public TimeSpan RenderTime;
    }
}
