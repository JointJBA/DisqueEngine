using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebRenderToy
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            if (Request.QueryString != null && Request.QueryString.Count > 0)
            {
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void sub_Click(object sender, EventArgs e)
        {
            writeReport();
            Response.Redirect("/Report.aspx?y=1");
        }

        void writeReport()
        {
            int c = new DirectoryInfo(Request.MapPath("~/Reports")).GetDirectories().Length;
            File.WriteAllText(Request.MapPath("~/Reports/" + c + "-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "-" + DateTime.Now.Millisecond + ".txt"), name.Text + "--" + email.Text + "--" + text.Text);
        }
    }
}