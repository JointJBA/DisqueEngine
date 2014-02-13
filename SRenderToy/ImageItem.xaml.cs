using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace SRenderToy
{
    public partial class ImageItem : UserControl
    {
        public ImageItem()
        {
            InitializeComponent();
        }

        public void SetImage(string name)
        {
            string uri = Application.Current.Host.Source.AbsoluteUri;
            uri = uri.Remove(uri.IndexOf("/ClientBin"));
            UriBuilder ub = new UriBuilder(uri + "/ImageRend.ashx");
            ub.Query = "image=" + name + "&typ=down&typ2=y";
            mainImage.Source = new BitmapImage(ub.Uri);
            text.Text = "Name: " + name;
        }

        public void Select()
        {
            text.Visibility = System.Windows.Visibility.Visible;
            Height = 180;
        }

        public void UnSelect()
        {
            text.Visibility = System.Windows.Visibility.Collapsed;
            Height = 150;
        }
    }
}
