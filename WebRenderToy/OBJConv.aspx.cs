using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Disque.Raytracer.Math;

namespace WebRenderToy
{
    public partial class OBJConv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void conv_Click(object sender, EventArgs e)
        {
            output.Text = new OBJReader(input.Text).ToRml();
        }
    }
}