using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Disque.Raytracer.Rml;
using Disque.Raytracer.Worlds;
using System.IO;
using System.Text;
using Disque;
using System.Net;
using System.Diagnostics;
using Disque.Math;

namespace WebRenderToy
{
    /// <summary>
    /// Summary description for HHelper
    /// </summary>
    public class HHelper : IHttpHandler
    {
        public static readonly Dictionary<string, Session> Sessions = new Dictionary<string, Session>();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.QueryString.Count > 0)
            {
                string cmnd = context.Request["cmnd"];
                if (cmnd == "compile")
                {
                    string id = context.Request["id"];
                    string txt = context.Request.Form["file"];
                    try
                    {
                        if (Sessions[id].World != null)
                        {
                            Sessions[id].World.Remove();
                        }
                        Sessions[id].Parser.Parse(txt);
                        Sessions[id].World = Sessions[id].Parser.Compile();
                        context.Response.Write("s");
                    }
                    catch (Exception e)
                    {
                        context.Response.Write(e.Message + "\n" + e.StackTrace);
                    }
                }
                else if (cmnd == "getUniqueSessionID")
                {
                    string s = getUniqueSessionID();
                    Sessions.Add(s, new Session());
                    context.Response.Write(s);
                }
                else if (cmnd == "getSessionsCount")
                {
                    context.Response.Write(Sessions.Count);
                }
                else if (cmnd == "getSessions")
                {
                    string sessions = "";
                    foreach (string s in Sessions.Keys)
                    {
                        sessions += "\n" + s;
                    }
                    context.Response.Write(Sessions.Count + sessions);
                }
                else if (cmnd == "progress")
                {
                    string id = context.Request["id"];
                    float n = Sessions[id].World.Screen.Pixels.Count, d = Sessions[id].World.ViewPlane.HRes * Sessions[id].World.ViewPlane.VRes;
                    int prog = (int)((n / d) * 100.0f);
                    if (prog == 100)
                        Sessions[id].Watch.Stop();
                    context.Response.Write(prog.ToString());
                }
                else if (cmnd == "getTime")
                {
                    string id = context.Request["id"];
                    context.Response.Write(Sessions[id].Watch.Elapsed.ToString());
                }
                else if (cmnd == "pixels")
                {
                    string id = context.Request["id"];
                    float n = Sessions[id].World.Screen.Pixels.Count, d = Sessions[id].World.ViewPlane.HRes * Sessions[id].World.ViewPlane.VRes;
                    context.Response.Write(n + "/" + d);
                }
                else if (cmnd == "render")
                {
                    string id = context.Request["id"];
                    context.Response.Write("s");
                    Sessions[id].World.RenderScene();
                    Sessions[id].Watch.Restart();
                }
                else if (cmnd == "getImage")
                {
                    context.Response.ContentType = "image/png";
                    string id = context.Request["id"];
                    string type = context.Request["type"];
                    if (type == "render")
                        writeImage(Sessions[id].World, context);
                    else
                    {
                        string name = context.Request["name"];
                        Extensions.CreateFromImage(Sessions[id].Parser.Images[name]).WriteTo(context.Response.OutputStream);
                    }
                }
                else if (cmnd == "checkName")
                {
                    string id = context.Request["id"];
                    string name = context.Request["name"];
                    if (Sessions[id].Parser.Images.ContainsKey(name))
                        context.Response.Write("t");
                    else
                        context.Response.Write("f");
                }
                else if (cmnd == "addImage")
                {
                    string id = context.Request["id"];
                    string type = context.Request["type"];
                    string name = context.Request["name"];
                    if (type == "url")
                    {
                        try
                        {
                            Sessions[id].Parser.Images.Add(name, Extensions.CreateFromBitmap(BitmapFromWeb(context.Request.Form["file"])));
                            context.Response.Write("s");
                        }
                        catch (Exception e)
                        {
                            context.Response.Write(e.Message + "\n" + e.StackTrace);
                        }
                    }
                }
                else if (cmnd == "getOP")
                {
                    context.Response.Write(Environment.OSVersion);
                }
            }
        }

        static Random r = new Random();

        string getUniqueSessionID()
        {
            string session = "";
            do
            {
                session = "";
                for (int i = 0; i < r.Next(2, 30); i++)
                {
                    session += randomChar();
                }
            }
            while (Sessions.ContainsKey(session));
            return session;
        }

        static string randomChar()
        {
            return ((char)r.Next(65, 122)).ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        string getText(Stream s)
        {
            StringBuilder sb = new StringBuilder(); 
            int streamLength; 
            int streamRead; 
            streamLength = Convert.ToInt32(s.Length);
            Byte[] streamArray = new Byte[streamLength];
            streamRead = s.Read(streamArray, 0, streamLength);
            for (int i = 0; i < streamLength; i++)
            {
                sb.Append(Convert.ToChar(streamArray[i]));
            }
            return sb.ToString();
        }

        void writeImage(World world, HttpContext context)
        {
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(world.ViewPlane.HRes, world.ViewPlane.VRes);
            for (int i = 0; i < world.Screen.Pixels.Count; i++)
            {
                Pixel p = world.Screen.Pixels[i];
                Vector3 c = max_to_one(new Vector3(p.Color.R, p.Color.G, p.Color.B));
                bm.SetPixel((int)p.X, (int)p.Y, System.Drawing.Color.FromArgb((int)(c.X * 255.0f), (int)(c.Y * 255.0f), (int)(c.Z * 255.0f)));
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + context.Request.QueryString[0] + ".png");
                ms.WriteTo(context.Response.OutputStream);
            }
        }

        static Vector3 max_to_one(Vector3 c)
        {
            float max_value = Disque.MathHelper.Max(c.X, Disque.MathHelper.Max(c.Y, c.Z));

            if (max_value > 1.0)
                return (c / max_value);
            else
                return (c);
        }

        System.Drawing.Bitmap BitmapFromWeb(string URL)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(URL);
            myRequest.Method = "GET";
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
            myResponse.Close();
            return bmp;
        }
    }

    public class Session
    {
        public RmlParser Parser;
        public World World;
        public Stopwatch Watch = new Stopwatch();

        public Session()
        {
            Parser = new RmlParser();
        }
    }
}