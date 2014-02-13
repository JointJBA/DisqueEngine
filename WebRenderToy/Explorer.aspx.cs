using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebRenderToy
{
    public partial class Explorer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tree.Nodes.Clear();
            DirectoryInfo root = new DirectoryInfo(Server.MapPath("~/"));
            tree.SelectedNodeChanged += new EventHandler(tree_SelectedNodeChanged);
            TreeNode node = new TreeNode("Root");
            tree.Nodes.Add(node);
            getDirectories(root, node);
            tree.CollapseAll();
        }

        void tree_SelectedNodeChanged(object sender, EventArgs e)
        {
            file.InnerText = File.ReadAllText(tree.SelectedNode.ToolTip);
        }

        void getDirectories(DirectoryInfo d, TreeNode tnode)
        {
            foreach (DirectoryInfo dinfo in d.GetDirectories())
            {
                TreeNode t = new TreeNode(dinfo.Name);
                tnode.ChildNodes.Add(t);
                getDirectories(dinfo, t);
            }
            getFiles(d, tnode);
        }

        void getFiles(DirectoryInfo d, TreeNode t)
        {
            foreach (FileInfo f in d.GetFiles())
            {
                TreeNode tn = new TreeNode(f.Name);
                tn.ToolTip = f.FullName;
                t.ChildNodes.Add(tn);
            }
        }
    }
}