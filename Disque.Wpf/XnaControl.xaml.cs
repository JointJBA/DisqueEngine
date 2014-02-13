using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace Disque.Wpf
{
    /// <summary>
    /// Interaction logic for XnaControl.xaml
    /// </summary>
    public partial class XnaControl : System.Windows.Controls.UserControl
    {
        public XnaControl()
        {
            InitializeComponent();
            GamePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(GamePanel_MouseMove);
            GamePanel.MouseDown += new MouseEventHandler(GamePanel_MouseDown);
            GamePanel.MouseUp += new MouseEventHandler(GamePanel_MouseUp);
        }

        void forms_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show(e.ToString());
        }
        void GamePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MouseRightButtonUp != null)
                    MouseRightButtonUp(this, e);
            }
            else if (e.Button == MouseButtons.Left)
                if (MouseLeftButtonUp != null)
                    MouseLeftButtonUp(this, e);
        }
        void GamePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MouseRightButtonDown != null)
                    MouseRightButtonDown(this, e);
            }
            else if (e.Button == MouseButtons.Left)
                if (MouseLeftButtonDown != null)
                    MouseLeftButtonDown(this, e);
        }
        void GamePanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (MouseMove != null)
                MouseMove(this, e);
        }
        public IntPtr Handle
        {
            get
            {
                return GamePanel.Handle;
            }
        }
        public new event MouseEventHandler MouseMove;
        public new event MouseEventHandler MouseLeftButtonDown;
        public new event MouseEventHandler MouseRightButtonDown;
        public new event MouseEventHandler MouseLeftButtonUp;
        public new event MouseEventHandler MouseRightButtonUp;
    }
}
