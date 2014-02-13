using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebRenderToy
{
    public partial class Element : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ele = "", att = "";
            if (Request.QueryString != null && Request.QueryString[0] != null)
            {
                ele = Request.QueryString[0];
                if (Request.QueryString.Count > 1)
                    att = Request.QueryString[1];
                if (att == "")
                {
                    string[] p = ele.Split(new char[] { '.' });
                    XDocument xdoc = XDocument.Load(Request.MapPath("~/Scripts/ElementsDesc.xml"));
                    foreach (XElement xele in xdoc.Element("Elements").Elements())
                    {
                        if (xele.Attribute("Name").Value == p[0])
                            foreach (XElement xel in xele.Elements())
                            {
                                if (xel.Attribute("Name").Value == p[1])
                                {
                                    createElementTable(xel);
                                }
                            }
                    }
                }
                else
                {
                    string[] p = ele.Split(new char[] { '.' });
                    XDocument xdoc = XDocument.Load(Request.MapPath("~/Scripts/ElementsDesc.xml"));
                    foreach (XElement xele in xdoc.Element("Elements").Elements())
                    {
                        if (xele.Attribute("Name").Value == p[0])
                            foreach (XElement xel in xele.Elements())
                            {
                                if (xel.Attribute("Name").Value == p[1])
                                {
                                    foreach (XElement el in xel.Elements())
                                    {
                                        if (el.Attribute("Name").Value == att)
                                        {
                                            createAttributeTable(el);
                                        }
                                    }
                                }
                            }
                    }
                }
            }
        }

        void createElementTable(XElement xele)
        {
            Literal l = new Literal();
            l.Text = "<h4>" + xele.Attribute("Name").Value + "</h4>";
            form1.Controls.Add(l);
            Table table = new Table();
            table.Width = new Unit("800px");
            TableRow t = new TableRow();
            t.Cells.Add(new TableHeaderCell() { Text = "Attribute", CssClass = "tempTable", Width = 100 });
            t.Cells.Add(new TableHeaderCell() { Text = "Type", CssClass = "tempTable"});
            t.Cells.Add(new TableHeaderCell() { Text = "Description", CssClass = "tempTable" });
            table.Rows.Add(t);
            foreach (XElement ele in xele.Elements())
            {
                if (ele.Name != "RefGrp" && ele.Name != "RefEle")
                {
                    TableRow tr = new TableRow();
                    if (ele.HasElements)
                        tr.Cells.Add(new TableCell() { Text = "<a href='Element.aspx?e=" + Request.QueryString[0] + "&a=" + ele.Attribute("Name").Value + "'>" + ele.Attribute("Name").Value + "</a>", CssClass = "tempTable" });
                    else
                        tr.Cells.Add(new TableCell() { Text = ele.Attribute("Name").Value, CssClass = "tempTable" });
                    tr.Cells.Add(new TableCell() { Text = "<a href='Type.aspx?t=" + ele.Attribute("Type").Value + "'>" + ele.Attribute("Type").Value + "</a>", CssClass = "tempTable"});
                    tr.Cells.Add(new TableCell() { Text = ele.Attribute("Description").Value });
                    table.Rows.Add(tr);
                }
            }
            form1.Controls.Add(table);
        }

        void createAttributeTable(XElement xele)
        {
            Literal l = new Literal();
            l.Text = "<h4>" + xele.Attribute("Name").Value + "</h4>";
            form1.Controls.Add(l);
            Table table = new Table();
            table.Width = new Unit("800px");
            TableRow t = new TableRow();
            t.Cells.Add(new TableHeaderCell() { Text = "Value", CssClass = "tempTable", Width = 100 });
            t.Cells.Add(new TableHeaderCell() { Text = "Description", CssClass = "tempTable" });
            table.Rows.Add(t);
            foreach (XElement ele in xele.Elements())
            {
                TableRow tr = new TableRow();
                tr.Cells.Add(new TableCell() { Text = ele.Value, CssClass = "tempTable" });
                tr.Cells.Add(new TableCell() { Text = ele.Attribute("Description").Value });
                table.Rows.Add(tr);
            }
            form1.Controls.Add(table);
        }
    }
}