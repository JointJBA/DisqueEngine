using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Disque.Math;

namespace Disque.Raytracer.Rml
{
    public class FunctionParser
    {
        public static Dictionary<string, Function> Functions = new Dictionary<string, Function>();
        public delegate string Function(List<Parameter> parameters);
        internal static void createFunctions()
        {
            Functions.Add("rand", new Function(delegate(List<Parameter> l)
            {
                if (l.Count == 0)
                    return MathHelper.RandomInt().ToString();
                else if (l.Count == 1)
                    return MathHelper.RandomInt(int.Parse(l[0].Value)).ToString();
                else
                    return MathHelper.RandomInt(int.Parse(l[0].Value), int.Parse(l[1].Value)).ToString();
            }));
            Functions.Add("randf", new Function(delegate(List<Parameter> l)
            {
                if (l.Count == 0)
                    return MathHelper.RandomFloat().ToString();
                else
                    return MathHelper.RandomFloat(float.Parse(l[0].Value), float.Parse(l[1].Value)).ToString();
            }));
            Functions.Add("add", new Function(delegate(List<Parameter> l)
            {
                return (float.Parse(l[0].Value) + float.Parse(l[1].Value)).ToString();
            }));
            Functions.Add("subt", new Function(delegate(List<Parameter> l)
            {
                return (float.Parse(l[0].Value) - float.Parse(l[1].Value)).ToString();
            }));
            Functions.Add("mult", new Function(delegate(List<Parameter> l)
            {
                return (float.Parse(l[0].Value) * float.Parse(l[1].Value)).ToString();
            }));
            Functions.Add("div", new Function(delegate(List<Parameter> l)
            {
                return (float.Parse(l[0].Value) / float.Parse(l[1].Value)).ToString();
            }));
            Functions.Add("pow", new Function(delegate(List<Parameter> l)
            {
                return MathHelper.Pow(float.Parse(l[0].Value), float.Parse(l[1].Value)).ToString();
            }));
            Functions.Add("sin", new Function(delegate(List<Parameter> l)
            {
                return MathHelper.Sin(MathHelper.ToRadians(float.Parse(l[0].Value))).ToString();
            }));
            Functions.Add("cos", new Function(delegate(List<Parameter> l)
            {
                return MathHelper.Cos(MathHelper.ToRadians(float.Parse(l[0].Value))).ToString();
            }));
            Functions.Add("tan", new Function(delegate(List<Parameter> l)
            {
                return MathHelper.Tan(MathHelper.ToRadians(float.Parse(l[0].Value))).ToString();
            }));
            Functions.Add("max", new Function(delegate(List<Parameter> l)
            {
                return MathHelper.Max(float.Parse(l[0].Value), float.Parse(l[1].Value)).ToString();
            }));
            Functions.Add("min", new Function(delegate(List<Parameter> l)
            {
                return MathHelper.Min(float.Parse(l[0].Value), float.Parse(l[1].Value)).ToString();
            }));
            Functions.Add("rotatex", new Function(delegate(List<Parameter> l)
            {
                return toStringMatrix(Matrix4.CreateRotation(Vector3.Right, MathHelper.ToRadians(float.Parse(l[0].Value))));
            }));
            Functions.Add("rotatey", new Function(delegate(List<Parameter> l)
            {
                return toStringMatrix(Matrix4.CreateRotation(Vector3.Up, MathHelper.ToRadians(float.Parse(l[0].Value))));
            }));
            Functions.Add("rotatez", new Function(delegate(List<Parameter> l)
            {
                return toStringMatrix(Matrix4.CreateRotation(Vector3.Forward, MathHelper.ToRadians(float.Parse(l[0].Value))));
            }));
            Functions.Add("trans", new Function(delegate(List<Parameter> l)
            {
                return toStringMatrix(Matrix4.CreateTranslation(getV(l).ToVector3()));
            }));
            Functions.Add("scale", new Function(delegate(List<Parameter> l)
            {
                return toStringMatrix(Matrix4.CreateScale(getV(l).ToVector3()));
            }));
        }

        static string toStringMatrix(Matrix4 m)
        {
            string s = "";
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    s += m[r, c] + ",";
                }
            }
            s = s.Remove(s.Length - 1);
            return s;
        }

        public static string ParseFunctions(string txt)
        {
            string rtxt = txt.Replace(", ", ",");
            Functions.Clear();
            createFunctions();
            while (rtxt.Contains("f:"))
            {
                parse(ref rtxt);
            }
            return rtxt;
        }

        static void parse(ref string rtxt)
        {
            List<string> functs = GetSubStrings(rtxt, "{f:", "}").ToList<string>();
            foreach (string function in functs)
            {
                KeyValuePair<string, List<Parameter>> kvp = getFunction(function);
                string name = "{f:" + kvp.Key + "(";
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    name += kvp.Value[i].Value + ",";
                }
                name = (kvp.Value.Count > 0 ? name.Remove(name.Length - 1) : name) + ")}";
                rtxt = rtxt.Replace(name, Functions[kvp.Key](kvp.Value));
            }
        }

        private static IEnumerable<string> GetSubStrings(string input, string start, string end)
        {
            Regex r = new Regex(Regex.Escape(start) + "(.*?)" + Regex.Escape(end));
            MatchCollection matches = r.Matches(input);
            foreach (Match match in matches)
                yield return match.Groups[1].Value;
        }

        public struct Parameter
        {
            public string Value;
        }

        static KeyValuePair<string, List<Parameter>> getFunction(string f)
        {
            KeyValuePair<string, List<Parameter>> kvp;
            string v = f.Remove(f.IndexOf('('));
            string p = f.Remove(0, f.IndexOf('(') + 1).Replace(")", "");
            List<Parameter> list = new List<Parameter>();
            if (p.Contains(","))
            {
                string[] pmts = p.Split(',');
                for (int i = 0; i < pmts.Length; i++)
                {
                    list.Add(new Parameter() { Value = pmts[i] });
                }
            }
            else if (p != "")
            {
                list.Add(new Parameter() { Value = p });
            }
            kvp = new KeyValuePair<string, List<Parameter>>(v, list);
            return kvp;
        }

        static string getV(List<Parameter> o)
        {
            if (o.Count > 1)
                return o[0].Value + "," + o[1].Value + "," + o[2].Value;
            return o[0].Value;
        }
    }
}
