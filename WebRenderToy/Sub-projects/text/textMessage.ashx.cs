using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Net;
using System.IO;

namespace WebRenderToy.Sub_projects.text
{
    /// <summary>
    /// Summary description for textMessage
    /// </summary>
    public class textMessage : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.QueryString.Count > 0)
            {
                try
                {
                    string cmnd = context.Request.QueryString[3].Split(new string[] { " " }, StringSplitOptions.None)[0].ToLower();
                    string srch = context.Request.QueryString[3].Substring(context.Request.QueryString[3].IndexOf(" ") + 1).ToLower();
                    if (cmnd == "movie")
                    {
                        string name = check(srch);
                        context.Response.Write(getmRatings(name));
                    }
                    else if (cmnd == "book")
                    {
                        context.Response.Write(getbRatings(srch, context));
                    }
                }
                catch (Exception e)
                {
                    context.Response.Write("Incorrect format.");
                }
            }
        }

        string get(string uri)
        {
            Stream stream;
            StreamReader reader;
            String response = null;
            WebClient webClient = new WebClient();

            using (webClient)
            {
                try
                {
                    // open and read from the supplied URI
                    stream = webClient.OpenRead(uri);
                    reader = new StreamReader(stream);
                    response = reader.ReadToEnd();
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse)
                    {
                        // Add you own error handling as required
                        switch (((HttpWebResponse)ex.Response).StatusCode)
                        {
                            case HttpStatusCode.NotFound:
                            case HttpStatusCode.Unauthorized:
                                response = null;
                                break;

                            default:
                                throw ex;
                        }
                    }
                }
            }

            return response;
        }

        public string check(string s)
        {
            XDocument xdoc = XDocument.Parse(get("http://www.google.com/complete/search?output=toolbar&q=" + s));
            string d = "", f = "";
            int c = 0;
            foreach (XElement xele in xdoc.Element("toplevel").Elements())
            {
                if (c == 0)
                {
                    f = d;
                    c++;
                }
                d = xele.Element("suggestion").Attribute("data").Value;
                if (d == s)
                {
                    return s;
                }
            }
            return f;
        }

        public string getmRatings(string mov)
        {
            XDocument xdoc = XDocument.Parse(get("http://mymovieapi.com/?q=" + mov + "&type=xml"));
            string a = "N/A", b = "N/A", auth = "";
            try
            {
                foreach (XElement xele in xdoc.Element("IMDBDocumentList").Element("item").Elements())
                {
                    if (xele.Name == "rated")
                    {
                        a = xele.Value;
                    }
                    if (xele.Name == "rating")
                    {
                        b = xele.Value + "/10.0";
                    }
                    if (xele.Name == "title")
                    {
                        auth = getMovieName(xele);
                    }
                }

                return auth.Length > 0 ? auth + ": " + a + ", " + b : a + ", " + b;
            }
            catch (Exception e)
            {
                return "No matching movie found.";
            }
        }
        public string getMovieName(XElement x)
        {
            return x.Value;
        }
        public string getbRatings(string b, HttpContext ctx)
        {
            string bname = b.Split(':')[0];
            string auth = b.Split(':')[1];

            string data = get("https://www.googleapis.com/books/v1/volumes?q=" + bname.Replace(" ", "+") + "+inauthor:" + auth.Replace(" ", "+"));
            if (data.Contains("averageRating"))
            {
                return getTitle(data) + ": " + data.Substring(data.IndexOf("\"averageRating\":") + ("\"averageRating\":").Length).Split(new string[] {","}, StringSplitOptions.None)[0].Replace(" ", "") + "/5.0";
            }
            else
            {
                return "N/A";
            }
        }

        public string getTitle(string data)
        {
            return data.Substring(data.IndexOf("\"title\":") + ("\"title\":").Length).Split(new string[] { "\"," }, StringSplitOptions.None)[0].Replace("\"", "");
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}