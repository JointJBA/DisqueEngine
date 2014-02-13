using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRenderToy
{
    public partial class Type : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string typ = "";
            if (Request.QueryString.Count > 0)
            {
                typ = Request.QueryString[0].ToLower();
                Literal l = new Literal();
                if (typ == "vector")
                {
                    l.Text = "<h4>Vector</h4><p>A vector should have the following format: 'X,Y,Z' or 'X, Y, Z'; either single spacing between commas or not or the raytracer will throw an exception. You can also put decimals in a vector: '0.1,0.2,2'. If X, Y, and Z have the same value, you can write them as a single number (ie. '1' is equal to '1,1,1', '2' is equal to '2,2,2'). Colors are also in vector format where X stands for R, Y stands for G, and Z stands for B, but the values must be between 0 and 1. For example: '1,0,0' is red, '0,1,0' is green, '0,0,1' is blue, '0.5,0.5,0.5' is grey, etc.</p>";
                }
                else if (typ == "boolean")
                {
                    l.Text = "<h4>Boolean</h4><p>A boolean can be have either a 'True' or a 'False'. Capitalization is not important; 'true' is acceptable, so is 'tRuE'.</p>";
                }
                else if (typ == "string")
                {
                    l.Text = "<h4>String</h4><p>A string can have an unlimited number of characters. Please avoid '<', '>', and '/', because they cause mixups with the rest of the xml document.</p>";
                }
                else if (typ == "float")
                {
                    l.Text = "<h4>Float</h4><p>A floating point number can be with or wihtout decimal. '1', '0.1', and '.1' are all acceptable.</p>";
                }
                else if (typ == "enum")
                {
                    l.Text = "<h4>Enum</h4><p>An enum must have one of the given choices. For example, if a sample can be 'Regular' or 'MultiJittered', the parser will only accept these two solutions. The value must be literally the same; 'rEgular' or 'regulat' is not acceptable.</p>";
                }
                else
                {
                    Response.Redirect(Server.MapPath("Default.aspx"));
                }
                form1.Controls.Add(l);
            }
        }
    }
}