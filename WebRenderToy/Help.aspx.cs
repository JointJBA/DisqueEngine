using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.UI.HtmlControls;

namespace WebRenderToy
{
    public partial class Help : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            createTables();
        }

        void createTables()
        {
            XDocument xdoc = XDocument.Load(Request.MapPath("~/Scripts/ElementsDesc.xml"));
            foreach (XElement xele in xdoc.Element("Elements").Elements())
            {
                Literal l = new Literal();
                l.Text = "<h4>" + xele.Attribute("Name").Value + "</h4>";
                tablePanel.Controls.Add(l);
                Table table = new Table();
                table.Width = new Unit("800px");
                TableRow t = new TableRow();
                t.Cells.Add(new TableHeaderCell() { Text = "Element", CssClass = "tempTable", Width= 200});
                t.Cells.Add(new TableHeaderCell() { Text = "Description", CssClass = "tempTable" });
                table.Rows.Add(t);
                foreach (XElement ele in xele.Elements())
                {
                    TableRow tr = new TableRow();
                    if (ele.HasElements)
                        tr.Cells.Add(new TableCell() { Text = "<a href='Element.aspx?e=" + xele.Attribute("Name").Value + "." + ele.Attribute("Name").Value + "' target='_blank' >" + ele.Attribute("Name").Value + "</a>", CssClass = "tempTable" });
                    else
                        tr.Cells.Add(new TableCell() { Text = ele.Attribute("Name").Value, CssClass = "tempTable" });
                    tr.Cells.Add(new TableCell() { Text = ele.Attribute("Description").Value });
                    table.Rows.Add(tr);
                }
                tablePanel.Controls.Add(table);
            }
        }
    }
}